using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace BatteryDashBoard
{

    public enum PayloadDataType
    {   //RFC 6455 기반
        Unknown = -1,
        Continuation = 0,
        Text = 1,
        Binary = 2,
        ConnectionClose = 8,
        Ping = 9,
        Pong = 10
    }


    class WebSocketControllerEx
    {
        //웹 소켓의 상태 객체
        public WebSocketState State { get; private set; } = WebSocketState.None;

        private readonly TcpClient targetClient;
        private readonly NetworkStream messageStream;
        private readonly byte[] dataBuffer = new byte[1024];

        public WebSocketControllerEx(TcpClient tcpClient)
        {
            State = WebSocketState.Connecting;  //완전한 WebSocket 연결이 아니므로 연결 중 표시

            targetClient = tcpClient;
            messageStream = targetClient.GetStream();
            messageStream.BeginRead(dataBuffer, 0, dataBuffer.Length, OnReadData, null);
        }

        public byte[] ConvertToByteArray(IList<ArraySegment<byte>> list)
        {
            var bytes = new byte[list.Sum(asb => asb.Count)];
            int pos = 0;

            foreach (var asb in list)
            {
                Buffer.BlockCopy(asb.Array, asb.Offset, bytes, pos, asb.Count);
                pos += asb.Count;
            }

            return bytes;
        }

        private void OnReadData(IAsyncResult ar)
        {


            int size = messageStream.EndRead(ar);   //데이터 수신 종료
            byte[] httpRequestRaw = new byte[1024];    //HTTP request 
            string httpRequest = Encoding.UTF8.GetString(httpRequestRaw, 0, size);


            //GET 요청인지 여부 확인
            if (Regex.IsMatch(httpRequest, "^GET", RegexOptions.IgnoreCase))
            {
                HandshakeToClient(size);        // 연결 요청에 대한 응답
                State = WebSocketState.Open;    // 응답이 성공하여 연결 중으로 상태 전환
            }
            else
            {
                // 메시지 수신에 대한 처리, 반환 값은 연결 종료 여부
                if (ProcessClientRequest(size) == false) { return; }
            }

            //데이터 수신 재시작
            messageStream.BeginRead(dataBuffer, 0, dataBuffer.Length, OnReadData, null);

        }

        private bool ProcessClientRequest(int dataSize)
        {

            bool fin = (dataBuffer[0] & 0b10000000) != 0;   // 혹시 false일 경우 다음 데이터와 이어주는 처리를 해야 함
            bool mask = (dataBuffer[1] & 0b10000000) != 0;  // 클라이언트에서 받는 경우 무조건 true
            PayloadDataType opcode = (PayloadDataType)(dataBuffer[0] & 0b00001111); // enum으로 변환

            int msglen = dataBuffer[1] - 128; // Mask bit가 무조건 1라는 가정하에 수행
            int offset = 2;     //데이터 시작점
            if (msglen == 126)  //길이 126 이상의 경우
            {
#if false
                msglen = BitConverter.ToInt16(new byte[] { dataBuffer[3], dataBuffer[2] }, Int32);
#else

                msglen = BitConverter.ToInt16(new byte[] { dataBuffer[3], dataBuffer[2] }, 0);
              //  byte[] bytes = BitConverter.GetBytes(dataBuffer);

             //   Console.WriteLine("byte array: " + BitConverter.ToInt32(msglen);

#endif
                offset = 4;
            }
            else if (msglen == 127)
            {
                // 이 부분은 구현 안 함. 나중에 필요한 경우 구현
                Console.WriteLine("Error: over int16 size");
                return true;
            }

            if (mask)
            {
                byte[] decoded = new byte[msglen];  //데이터
                                                    //...(생략)

                Console.WriteLine(Encoding.UTF8.GetString(decoded));    //데이터 출력
                switch (opcode)
                {
                    case PayloadDataType.Text:
                        SendData(Encoding.UTF8.GetBytes("Success"), PayloadDataType.Text);
                        break;
                    case PayloadDataType.Binary:
                        //Binary는 아무 동작 없음
                        break;
                    default:
                        Console.WriteLine("Unknown Data Type");
                        break;
                }
            }
            else
            {
                // 마스킹 체크 실패
                Console.WriteLine("Error: Mask bit not valid");
            }

            return true;
        }


        public void SendData(byte[] data, PayloadDataType opcode)
        {
            byte[] sendData;
            BitArray firstByte = new BitArray(new bool[] {
                    // opcode
                    opcode == PayloadDataType.Text || opcode == PayloadDataType.Ping,
                    opcode == PayloadDataType.Binary || opcode == PayloadDataType.Pong,
                    false,
                    opcode == PayloadDataType.ConnectionClose || opcode == PayloadDataType.Ping || opcode == PayloadDataType.Pong,
                    false,  //RSV3
                    false,  //RSV2
                    false,  //RSV1
                    true,   //Fin
                });
            //위 코드는 아래 설명 참조

            if (data.Length < 126)
            {
                sendData = new byte[data.Length + 2];
                firstByte.CopyTo(sendData, 0);
                sendData[1] = (byte)data.Length;    //서버에서는 Mask 비트가 0이어야 함
                data.CopyTo(sendData, 2);
            }
            else
            {
                // 수신과 마찬가지로 32,767이상의 길이(int16 범위 이상)의 데이터에 대응하지 못함
                sendData = new byte[data.Length + 4];
                firstByte.CopyTo(sendData, 0);
                sendData[1] = 126;
                byte[] lengthData = BitConverter.GetBytes((ushort)data.Length);
                Array.Copy(lengthData, 0, sendData, 2, 2);
                data.CopyTo(sendData, 4);
            }

            messageStream.Write(sendData, 0, sendData.Length);  //클라이언트에 전송
        }

        public void HandshakeToClient(int dataSize)
        {
            string raw = Encoding.UTF8.GetString(dataBuffer);

            string swk = Regex.Match(raw, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
            string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
            byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
            string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

            // HTTP/1.1은 연속된 CR, LF를 라인의 끝을 의미하는 마커로 정의
            byte[] response = Encoding.UTF8.GetBytes(
                "HTTP/1.1 101 Switching Protocols\r\n" +
                "Connection: Upgrade\r\n" +
                "Upgrade: websocket\r\n" +
                "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

            //요청 승인 응답 전송
            messageStream.Write(response, 0, response.Length);
        }

    }


    public class webServer
    {
        public string _addr = "";
        public int _port = 0;

        TcpListener _listner = null;
        TcpClient _client = null;

        NetworkStream _clientStream = null;
        byte[] _readBuffer = null;

        NetworkStream _webstream = null;
        private static webServer instance = null;// new webServer();
        static int _wport = 0;
        public webServer (string addr , int port)
        {
            //주소 저장
            _addr = addr;
            _port = port;

        }


        public static webServer getInstance()
        {
            if (null == instance)
            {
                int wport = _wport;// _port;
                instance = new webServer("127.0.0.1", wport);
                //instance.start();
            }

            return instance;
        }


        public string uID = "";
        
        private webServer() {
            this.uID = ""; 
        }


        public void ThreadStart(int Port)
        {
#if false
            Thread t1 = new Thread(new ThreadStart(webSocketStart(Port)));
            t1.Start();
#else

            int port = Port;

            System.Threading.Thread th = new Thread(new ParameterizedThreadStart(webSocketStart));
            th.Start(port);

#endif
        }


        private void webSocketStart(object  ObjecPort)
        {
            //ws://127.0.0.1/
            int port = (int)ObjecPort;
            string ip = "127.0.0.1";
            int nPort = 80;
            nPort = port;
            _port = port;
            ip = _addr;

             string path = "/Testwebsocket"; ;// "/1"
                                              //   resMessage.Length
            string wsIPadress = ip + path;//
             //string path = "/Testwebsocket"; ;// "/1"

          var server = new TcpListener(IPAddress.Parse(ip), nPort);

            server.Start();
            Console.WriteLine("Server has started on {0}:{1}, Waiting for a connection...", ip, nPort);

            //_client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");

#if false
            TcpClient client = server.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
#else

            _client = server.AcceptTcpClient();
            _webstream = _client.GetStream();

            TcpClient client = _client;
            NetworkStream stream = _webstream;
#endif

            // enter to an infinite cycle to be able to handle every change in stream
            while (true)
            {
                while (!stream.DataAvailable) ;
                while (client.Available < 3) ; // match against "get"

                byte[] bytes = new byte[client.Available];
                stream.Read(bytes, 0, client.Available);
                string sMessage = Encoding.UTF8.GetString(bytes);

                if (Regex.IsMatch(sMessage, "^GET", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("=====Handshaking from client=====\n{0}", sMessage);

                    // 1. Obtain the value of the "Sec-WebSocket-Key" request header without any leading or trailing whitespace
                    // 2. Concatenate it with "258EAFA5-E914-47DA-95CA-C5AB0DC85B11" (a special GUID specified by RFC 6455)
                    // 3. Compute SHA-1 and Base64 hash of the new value
                    // 4. Write the hash back as the value of "Sec-WebSocket-Accept" response header in an HTTP response
                    string swk = Regex.Match(sMessage, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
                    string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                    byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
                    string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

                    // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
                    byte[] response = Encoding.UTF8.GetBytes(
                        "HTTP/1.1 101 Switching Protocols\r\n" +
                        "Connection: Upgrade\r\n" +
                        "Upgrade: websocket\r\n" +
                        "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

                    stream.Write(response, 0, response.Length);
                }
                else
                {
                    bool fin = (bytes[0] & 0b10000000) != 0,
                        mask = (bytes[1] & 0b10000000) != 0; // must be true, "All messages from the client to the server have this bit set"

                    int opcode = bytes[0] & 0b00001111, // expecting 1 - text message
                        msglen = bytes[1] - 128, // & 0111 1111
                        offset = 2;

                    if (msglen == 126)
                    {
                        // was ToUInt16(bytes, offset) but the result is incorrect
                        msglen = BitConverter.ToUInt16(new byte[] { bytes[3], bytes[2] }, 0);
                        offset = 4;
                    }
                    else if (msglen == 127)
                    {
                        Console.WriteLine("TODO: msglen == 127, needs qword to store msglen");
                        // i don't really know the byte order, please edit this
                        // msglen = BitConverter.ToUInt64(new byte[] { bytes[5], bytes[4], bytes[3], bytes[2], bytes[9], bytes[8], bytes[7], bytes[6] }, 0);
                        // offset = 10;
                    }

                    if (msglen == 0)
                        Console.WriteLine("msglen == 0");
                    else if (mask)
                    {
#if true
                        byte[] decoded = new byte[msglen];
                        byte[] masks = new byte[4] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] };
                        offset += 4;

                        for (int i = 0; i < msglen; ++i)
                            decoded[i] = (byte)(bytes[offset + i] ^ masks[i % 4]);

                        string text = Encoding.UTF8.GetString(decoded);
                        Console.WriteLine("recive:{0}", text);
#else
                        //--------------------------------------------------------------
                        byte[] decoded = new byte[msglen];
                        byte[] masks = new byte[4] { bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3] };
                        offset += 4;

                        for (int i = 0; i < msglen; ++i)
                            decoded[i] = (byte)(bytes[offset + i] ^ masks[i % 4]);

                        string text = Encoding.UTF8.GetString(decoded);

                        send(text);
#endif
                    }
                    else
                        Console.WriteLine("mask bit not set");

                    Console.WriteLine();
                }
            }
        }



        public bool start()
        {
            bool bResult = true;
            if (null == _listner)
            {
                _listner = new TcpListener(IPAddress.Parse(_addr), _port);
            }

            if (null != _listner)
            {
                try
                {
                    _listner.Start();
                    Console.WriteLine("웹소켓 서버 오픈...");
#if false
                    //_listner.BeginAcceptTcpClient(OnServerConnect, null);
#else
                    _listner.BeginAcceptTcpClient(OnAcceptClient, null);
#endif
                    Console.WriteLine("클라이언트와의 접속 대기...");

                }
                catch(Exception ex)
                {

                    bResult = false;
                    Console.WriteLine(ex.ToString());
                }
            }

            return bResult;


        }


        private void OnAcceptClient(IAsyncResult ar)
        {
            TcpClient client = _listner.EndAcceptTcpClient(ar);
            WebSocketControllerEx webSocketController = new WebSocketControllerEx(client);
            //다음 클라이언트를 대기
            _listner.BeginAcceptTcpClient(OnAcceptClient, null);
        }


        void OnServerConnect1(IAsyncResult ar)
        {
            _client = _listner.EndAcceptTcpClient(ar);
            Console.WriteLine("클라이언트 접속함.");

            _listner.BeginAcceptTcpClient(OnServerConnect1, null);

             _readBuffer = new byte[1024];



#if flase
            // 현재의 클라이언트로 부터 데이터를 받아 옵니다.
            _clientStream = _client.GetStream();
            _clientStream.BeginRead(_readBuffer, 0, _readBuffer.Length, onAcceptReader, null);

#else
            _listner.BeginAcceptTcpClient(OnAcceptClient, null);
#endif

        }

        void onAcceptReader(IAsyncResult ar)
        {

            // 받은 데이터의 길이를 확인합니다.
            int receiveLength = _clientStream.EndRead(ar);


            // 받은 데이터가 없는 경우는 접속이 끊어진 경우 입니다.
            if (receiveLength <= 0)
            {
                Console.WriteLine("접속이 끊어졌습니다.");
                return;
            }


            // 받은 메시지를 출력합니다.
            string newMessage = Encoding.UTF8.GetString(_readBuffer, 0, receiveLength);
            Console.WriteLine(
                string.Format("받은 메시지:{0}\n", newMessage)
            );


            // 첫 3문자가 GET으로 시작하지 않는 경우, 잘못된 접속이므로 종료합니다.
            if (!Regex.IsMatch(newMessage, "^GET"))
            {
                Console.WriteLine("잘못된 접속 입니다.");
                return;
            }


            bool bWSS =  true;

            string resMessage = "Server OK";
            if (bWSS)
            {
                // 클라이언트로 응답을 돌려 줍니다.
                const string eol = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker


                // 보낼 메시지.
                 resMessage = "HTTP/1.1 101 Switching Protocols" + eol
                    + "Connection: Upgrade" + eol
                    + "Upgrade: websocket" + eol
                    + "Sec-WebSocket-Accept: " + Convert.ToBase64String(
                        System.Security.Cryptography.SHA1.Create().ComputeHash(
                            Encoding.UTF8.GetBytes(
                                new Regex("Sec-WebSocket-Key: (.*)").Match(newMessage).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"
                            )
                        )
                    ) + eol
                    + eol
                    ;
            }


            // 보낸 메시지를 출력해 봅니다.
            Console.WriteLine(
                string.Format("보낸 메시지:{0}\n", resMessage)
            );



            // 메시지를 보내 줍니다.
            Byte[] response = Encoding.UTF8.GetBytes(resMessage);
            _clientStream.Write(response, 0, response.Length);

        }

        public bool send(string newMessage)
        {

            // 클라이언트로 응답을 돌려 줍니다.
            const string eol = "\r\n"; // HTTP/1.1 defines the sequence CR LF as the end-of-line marker


            bool bWSS =  false;
            string resMessage = newMessage;
            int offset = 2;     //데이터 시작점

            // 메시지를 보내 줍니다.
            //if (null != _clientStream)
            if (null != _webstream)
            {
#if false
                string sHost = String.Format("{0}:{1}",_addr,_port);
                int nversion = 13;

                byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(resMessage));
                string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

                if (bWSS)
                {

                    // 보낼 메시지.
                    resMessage = "GET/HTTP/1.1" + eol
                       + "Connection: Upgrade,Keep-Alive" + eol
                       + "Upgrade: websocket" + eol
                       + "Host:" + sHost + eol
                       + "Sec-WebSocket-Version:" +  Convert.ToString(nversion) + eol 
                       + "Sec-WebSocket-Key:"+ swkaSha1Base64 + eol
                       + eol
                       + eol
                     ;
                 }


                // 보낸 메시지를 출력해 봅니다.
                Console.WriteLine(
                    string.Format("보낸 메시지:{0}\r\n", resMessage)
                );


                Byte[] response = Encoding.UTF8.GetBytes(resMessage);
                //_clientStream.Write(response, 0, response.Length);

                _webstream.Write(response, 0, response.Length);
#else


                Console.WriteLine("recive:{0}", resMessage);

                byte[] bytes = new byte[resMessage.Length];

                byte[] StrByte = Encoding.UTF8.GetBytes(resMessage);

                //>> 보내기-1
                _webstream.Write(StringToByte(resMessage), 0, resMessage.Length);
             //   _webstream.Write(StrByte, 0, StrByte.Length);


                //>> 보내기2
                if (Regex.IsMatch(resMessage, "^GET", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("=====Handshaking from client=====\n{0}", resMessage);

                    // 1. Obtain the value of the "Sec-WebSocket-Key" request header without any leading or trailing whitespace
                    // 2. Concatenate it with "258EAFA5-E914-47DA-95CA-C5AB0DC85B11" (a special GUID specified by RFC 6455)
                    // 3. Compute SHA-1 and Base64 hash of the new value
                    // 4. Write the hash back as the value of "Sec-WebSocket-Accept" response header in an HTTP response
                    string swk = Regex.Match(resMessage, "Sec-WebSocket-Key: (.*)").Groups[1].Value.Trim();
                    string swka = swk + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
                    byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(swka));
                    string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);

                    // HTTP/1.1 defines the sequence CR LF as the end-of-line marker
                    byte[] response2 = Encoding.UTF8.GetBytes(
                        "HTTP/1.1 101 Switching Protocols\r\n" +
                        "Connection: Upgrade\r\n" +
                        "Upgrade: websocket\r\n" +
                        "Sec-WebSocket-Accept: " + swkaSha1Base64 + "\r\n\r\n");

                    _webstream.Write(response2, 0, response2.Length);
                }

                if (bWSS)
                {

                    string sHost = String.Format("{0}:{1}", _addr, _port);
                    int nversion = 13;

                    byte[] swkaSha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(resMessage));
                    string swkaSha1Base64 = Convert.ToBase64String(swkaSha1);


                    // 보낼 메시지.
                    resMessage = "GET/HTTP/1.1" + eol
                       + "Connection: Upgrade,Keep-Alive" + eol
                       + "Upgrade: websocket" + eol
                       + "Host:" + sHost + eol
                       + "Sec-WebSocket-Version:" + Convert.ToString(nversion) + eol
                       + "Sec-WebSocket-Key:" + swkaSha1Base64 + eol
                       + eol
                       + eol
                     ;
                }

                Byte[] response = Encoding.UTF8.GetBytes(resMessage);

                _webstream.Write(response, 0, response.Length);



#endif
            }

            return true;
        }

        // 바이트 배열을 String으로 변환
        private string ByteToString(byte[] strByte) { 
            string str = Encoding.Default.GetString(strByte); 
            return str;
        } 


        // String을 바이트 배열로 변환
         private byte[] StringToByte(string str) {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }


        void SendHttpTest()
        {
            // TCP/IP Socket 객체 생성
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 호스트를 IP로 변경
            IPHostEntry hostEntry = Dns.GetHostEntry("httpbin.org");
            IPAddress ip = hostEntry.AddressList[0];

            // HTTP 서버 접속
            var httpEndPoint = new IPEndPoint(ip, 80);
            sock.Connect(httpEndPoint);

            // HTTP header should end with double newline (\r\n\r\n)
            string http = @"GET http://httpbin.org/get HTTP/1.1
Host: httpbin.org
Connection: keep-alive
Cache-Control: max-age=0
User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/67.0.3396.99 Safari/537.36
Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8
Accept-Encoding: gzip, deflate
Accept-Language: en-US,en;q=0.9
 
";

            // Send
            var sendBuff = Encoding.ASCII.GetBytes(http);
            sock.Send(sendBuff, SocketFlags.None);

            // Receive
            byte[] recvBuff = new byte[sock.ReceiveBufferSize];
            int nCount = sock.Receive(recvBuff);

            // 파일 저장
            string result = Encoding.ASCII.GetString(recvBuff, 0, nCount);
            File.WriteAllText("test.out", result);

            sock.Close();
        }


    }
}
