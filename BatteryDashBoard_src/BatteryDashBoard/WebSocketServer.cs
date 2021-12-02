using BatteryDashBoard;
using BatteryDashBoard.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
//using System.Net.NetworkInformation;


public enum WebSocketState
{
    None = 0,
    Connecting = 1,
    Open = 2,
    CloseSent = 3,
    CloseReceived = 4,
    Closed = 5,
    Aborted = 6
}

public enum PayloadDataType
{
    Unknown = -1,
    Continuation = 0,
    Text = 1,
    Binary = 2,
    ConnectionClose = 8,
    Ping = 9,
    Pong = 10
}


public class ConnectionManager
{
    private readonly TcpListener tcpListener=null;

    public WebSocketController _webSocketController=null;
    private static ConnectionManager _instance =null;
    private static int _wport = 80;

    private static string _address="";
    public ConnectionManager(string IPaddress, int port)
    {
        string address =  IPaddress;
        if ("" == address || "0.0.0.0" == address)
        {
            address = Get_MyIP();

        }

        _address = address;
        _wport = port;

        tcpListener = new TcpListener(IPAddress.Parse(address), port);
        tcpListener.Start();
        //비동기 Listening 시작
        tcpListener.BeginAcceptTcpClient(OnAcceptClient, null);// 중요
    }

    //연결된 IP 값
    public string getConnectIP()
    {
        return _address; //대기중인 - IP
    }

    public int getConnectPort()
    {
        return _wport; // 대기중 열린 -Port
    }

    private void OnAcceptClient(IAsyncResult ar)
    {
        TcpClient client = tcpListener.EndAcceptTcpClient(ar);
        _webSocketController = new WebSocketController(client);
        //다음 클라이언트를 대기
        tcpListener.BeginAcceptTcpClient(OnAcceptClient, null);
    }

    private void  aaa()
    {
        int wport = _wport;
        string wsIPadress = "127.0.0.1";
        dynamic dobj = jsonParsingWebSocketValue("webSocketServer");

        if (null != dobj)
        {
            string strIP = (string)dobj["ip"];
            int nPort = (int)dobj["port"];
            string strPath = (string)dobj["path"];
            wsIPadress = strIP;
            if (nPort <= 80) nPort = 80;

            wport = nPort;
        }

        return;// "aaa",90;
    }

    public static dynamic jsonParsingWebSocketValue(string key)
    {

        CJsonParser cjson = new CJsonParser();
        dynamic dobj = cjson.getObject(key);
        return dobj;
    }


    //>> 사용하지 않음. 추후 사용 할지 몰라 남겨 뒀다.
    public static ConnectionManager getInstance()
    {
        if (null == _instance)
        {
            int wport = _wport;

            string address = "127.0.0.1";
            dynamic dobj = jsonParsingWebSocketValue("webSocketServer");

            if (null != dobj)
            {
                try
                {
                    string strIP = (string)dobj["ip"];
                    int nPort = (int)dobj["port"];
                    string strPath = (string)dobj["path"];
                    address = strIP;
                    if (nPort <= 80) nPort = 80;

                    wport = nPort;
                    _wport = nPort;
                }catch(Exception ex)
                {
                    address = "127.0.0.1";
                    wport = 80;
                    Console.WriteLine(ex.Message);
                }
            }


            _instance = new ConnectionManager(address, wport);
        }

        return _instance;
    }


    public string Get_MyIP()
    {
        string myip = "";

        NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
        foreach (NetworkInterface adapter in adapters)
        {
              IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
              foreach (UnicastIPAddressInformation uipi in adapterProperties.UnicastAddresses)
              {
                       //SubNetMask = 255.255.255.0 인것만 찾는다.
                     if("255.255.255.0" == uipi.IPv4Mask.ToString())
                     {
                            Console.WriteLine("IP Address = " + uipi.Address.ToString());

                            myip = uipi.Address.ToString();
                            return myip;
                     }

              }

        }


        return myip;
    }

    private void showSubnetMask() {
        NetworkInterface[]  adapters = NetworkInterface.GetAllNetworkInterfaces();
        string subnetMask = string.Empty;
        foreach (NetworkInterface adapter in adapters)
        {
            IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
            foreach (UnicastIPAddressInformation uipi in adapterProperties.UnicastAddresses) 
            { 
                //if (uipi.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetWork)
                //{
                //    if (adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                //    {
                //        subnetMask = uipi.IPv4Mask.ToString();
                //    }
                //} 
            }
        }
        string msg = "SubnetMask : " + subnetMask; 
        //MessageBox.Show(msg, "info", MessageBoxButton.OK, MessageBoxIcon.Information);

        return;
    }



}


public class WebSocketController
{
    //웹 소켓의 상태 객체
    public WebSocketState State { get; private set; } = WebSocketState.None;

    private readonly TcpClient targetClient=null;
    private readonly NetworkStream messageStream=null;
    private readonly byte[] dataBuffer = new byte[1024];

    public WebSocketController(TcpClient tcpClient)
    {
        State = WebSocketState.Connecting;  //완전한 WebSocket 연결이 아니므로 연결 중 표시

        targetClient = tcpClient;
        messageStream = targetClient.GetStream();
        messageStream.BeginRead(dataBuffer, 0, dataBuffer.Length, OnReadData, null);
    }



    private string StringByteToStringDesrilize(string JSONdata)
    {

        string strData = "";
        if (string.IsNullOrWhiteSpace(JSONdata)) { return strData; }

        char[] ch = new char[JSONdata.Length];



        for (int n = 0; n < JSONdata.Length; n++)
        {
            ch[n] = JSONdata[n];
            if (0 != ch[n])
            {
                strData += ch[n];
            }
        }

        return strData;
    }

    private void AsyncReadCallback(IAsyncResult asyncResult)
    {
        int size = 0;
        size = messageStream.EndRead(asyncResult);   //데이터 수신 종료

        byte[] byteBuffers = new byte[size];    //HTTP request method는 7자리를 넘지 않는다.
                                                   //GET만 확인하면 되므로 new byte[3]해도 상관없음
        Array.Copy(dataBuffer, byteBuffers, byteBuffers.Length);
        string httpRequestTemp = Encoding.UTF8.GetString(byteBuffers);

        string strBuffer = Encoding.ASCII.GetString(byteBuffers);

        string strHttpResponse = StringByteToStringDesrilize(httpRequestTemp);


        Console.WriteLine(":" , strHttpResponse);
    }

    private void OnReadData(IAsyncResult ar)
    {
        int size = 0;
        int bytesRecieved = 0;
        try
        {
             size = messageStream.EndRead(ar);   //데이터 수신 종료
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message); 
            return;
        }

        byte[] byteBuffers = new byte[size];    //HTTP request method는 7자리를 넘지 않는다.
                                                  //GET만 확인하면 되므로 new byte[3]해도 상관없음
        Array.Copy(dataBuffer, byteBuffers, byteBuffers.Length);
        string httpRequestTemp = Encoding.UTF8.GetString(byteBuffers);

        string strHttpResponse =  StringByteToStringDesrilize(httpRequestTemp);




        //GET 요청인지 여부 확인
        if (Regex.IsMatch(strHttpResponse, "^GET", RegexOptions.IgnoreCase))
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

    private void HandshakeToClient(int dataSize)
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

    private bool ProcessClientRequest(int dataSize)
    {
        bool bResult = false;
        bool fin = (dataBuffer[0] & 0b10000000) != 0;   // 혹시 false일 경우 다음 데이터와 이어주는 처리를 해야 함
        bool mask = (dataBuffer[1] & 0b10000000) != 0;  // 클라이언트에서 받는 경우 무조건 true
        PayloadDataType opcode = (PayloadDataType)(dataBuffer[0] & 0b00001111); // enum으로 변환

        int msglen = dataBuffer[1] - 128; // Mask bit가 무조건 1라는 가정하에 수행
        int offset = 2;     //데이터 시작점
        if (msglen == 126)  //길이 126 이상의 경우
        {
            msglen = BitConverter.ToInt16(new byte[] { dataBuffer[3], dataBuffer[2] },0);
          //  BitConverter.ToInt16
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
            byte[] decoded = new byte[msglen];
            //마스킹 키 획득
            byte[] masks = new byte[4] { dataBuffer[offset], dataBuffer[offset + 1], dataBuffer[offset + 2], dataBuffer[offset + 3] };
            offset += 4;

            for (int i = 0; i < msglen; i++)    //마스크 제거
            {
                decoded[i] = (byte)(dataBuffer[offset + i] ^ masks[i % 4]);
            }
            string strResponseValue = Encoding.UTF8.GetString(decoded);
            Console.WriteLine(strResponseValue);// Encoding.UTF8.GetString(decoded));    //데이터 출력
            switch (opcode)
            {
                case PayloadDataType.Text:
                    {
                        Console.WriteLine("Text-OK:" + strResponseValue);
                       //  SendData(Encoding.UTF8.GetBytes("Success"), PayloadDataType.Text);
                        ReusltAsync(strResponseValue);
                        bResult = true;
                    }
                    break;
                case PayloadDataType.Binary:
                    //Binary는 아무 동작 없음
                    break;
                case PayloadDataType.ConnectionClose:
                    //받은 요청이 서버에서 보낸 요청에 대한 응답이 아닌 경우에만 실행
                    if (State != WebSocketState.CloseSent)
                    {
                        SendCloseRequest(1000, "Graceful Close");
                        State = WebSocketState.Closed;
                    }
                    Dispose();      // 소켓 닫음
                    return bResult;// false;
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

        return bResult;// true;
    }


    // 바이트 배열을 String으로 변환
    private string ByteToString(byte[] strByte)
    {
        string str = Encoding.Default.GetString(strByte);
        return str;
    }


    // String을 바이트 배열로 변환
    private byte[] StringToByte(string str)
    {
        byte[] StrByte = Encoding.UTF8.GetBytes(str);
        return StrByte;
    }


    public bool SendData(string strData)
    {
        
        if (null != messageStream)
        {
            byte[] data = StringToByte(strData);
            SendData(data, PayloadDataType.Text);
            return true;
        }

        return false;
        

    }

    public bool SendData(byte[] data, PayloadDataType opcode)
    {
        bool bResult = false;
        byte[] sendByteData;
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
            sendByteData = new byte[data.Length + 2];
            firstByte.CopyTo(sendByteData, 0);
            sendByteData[1] = (byte)data.Length;    //서버에서는 Mask 비트가 0이어야 함
            data.CopyTo(sendByteData, 2);
            bResult = true;
        }
        else
        {
            // 수신과 마찬가지로 32,767이상의 길이(int16 범위 이상)의 데이터에 대응하지 못함
            sendByteData = new byte[data.Length + 4];
            firstByte.CopyTo(sendByteData, 0);
            sendByteData[1] = 126;
            byte[] lengthData = BitConverter.GetBytes((ushort)data.Length);
            Array.Copy(lengthData, 0, sendByteData, 2, 2);
            data.CopyTo(sendByteData, 4);
        }

#if true
        //Test  - 확인용
        UTF8Encoding encoder = new UTF8Encoding();
        string strBuffer = encoder.GetString(sendByteData);
#endif

        messageStream.Write(sendByteData, 0, sendByteData.Length);  //클라이언트에 전송

        return  bResult;
    }

    public void SendCloseRequest(ushort code, string reason)
    {
        byte[] closeReq = new byte[2 + reason.Length];
        BitConverter.GetBytes(code).CopyTo(closeReq, 0);
        //왜인지는 알 수 없지만 크롬에서 코드는 자리가 바뀌어야 제대로 인식할 수 있다.
        byte temp = closeReq[0];
        closeReq[0] = closeReq[1];
        closeReq[1] = temp;
        Encoding.UTF8.GetBytes(reason).CopyTo(closeReq, 2);
        SendData(closeReq, PayloadDataType.ConnectionClose);
    }

    public void Dispose()
    {
        targetClient.Close();
        targetClient.Dispose(); //모든 소켓에 관련된 자원 해제
    }

    private void ReusltAsync(string strResponseValue)
    {


        ClassSettingView setting = new ClassSettingView();

        setting.ReusltAsync(strResponseValue);

    }
}