using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Web.Script.Serialization;
using System.Net.NetworkInformation;
using System.Collections;
using System.Windows.Forms;

namespace BatteryDaemon
{
    public class wSocketClient
    {
        private static object consoleLock = new object();
        private const int sendChunkSize = 256;
        private const int receiveChunkSize = 256;
        private const bool verbose = true;
        //private static readonly TimeSpan delay = TimeSpan.FromMilliseconds(30000);//30초
        private static readonly TimeSpan delay = TimeSpan.FromMilliseconds(1350000);//30초

        private static  ClientWebSocket _webSocket = null;
        static UTF8Encoding encoder = new UTF8Encoding();

        private static wSocketClient _instance = null;
        private static string _address = "";
        private static int _wport = 80;
        private static string _path = "";

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



        public wSocketClient(string IPaddress, int port, string path="/")
        {
            Console.WriteLine("Press any key to exit...");

            string address = IPaddress;
            if ("" == address || "0.0.0.0" == address)  {
                address = Get_MyIP();// "127.0.0.1";
            }

            _address = address;
            _wport = port;
            _path = path;

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
                    if ("255.255.255.0" == uipi.IPv4Mask.ToString())
                    {
                        Console.WriteLine("IP Address = " + uipi.Address.ToString());
                        myip = uipi.Address.ToString();
                        return myip;
                    }

                }

            }

            return myip;
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

        public string getConnectPath()
        {
            return _path; //대기중인 - IP
        }

        public void stop()
        {
            if (_webSocket != null)
            {
                _webSocket.Dispose();
                _webSocket = null;
            }

        }

        public void close()
        {
            if( null != _instance)
            {
                stop();

                _instance = null;
            }
        }

        public static wSocketClient getInstance(string address, int  wport, string  path ="/")
        {
            if (null == _instance) { 
                _instance = new wSocketClient(address, wport, path);
            }
            
            return _instance;
        }

        //>> 사용하지 않음. 추후 사용 할지 몰라 남겨 뒀다.
        public static wSocketClient getInstance()
        {
            if (null == _instance)
            {

                int wport = _wport;
                string address = "127.0.0.1";
                string path = "/";
                dynamic dobj = jsonParsingWebSocketValue("webSocketServer");

                if (null != dobj)
                {
                    try
                    {
                        string strIP = (string)dobj["ip"];
                        int nPort = (int)dobj["port"];
                        string strPath = (string)dobj["path"];

                        if (nPort <= 80) nPort = 80;

                        address = strIP;
                        wport = nPort;
                        path = strPath;
                    }
                    catch (Exception ex)
                    {
                        address = "127.0.0.1";
                        wport = 80;
                        Console.WriteLine(ex.Message);
                    }
                }

                if(_address != address && "" != _address)
                {
                    address = _address;
                }

                _instance = new wSocketClient(address, wport, path);
            }

            return _instance;
        }

        public static dynamic jsonParsingWebSocketValue(string key)
        {
            CJsonParser cjson = new CJsonParser();
            dynamic dobj = cjson.getObject(key);
            return dobj;
        }

      
        public void task_webSocketClient(string strURL)
        {
            string strWebSetSocketIP = strURL;// _strwsip;
            try
            {
                Task t = Connect(strWebSetSocketIP);
                t.Wait();
                    
            }
            catch (Exception ex)
            {
                //MessageBox.Show(string.Format("{0}\n{1}", strWebSetSocketIP, ex.Message)," 접속 Error");
                Console.WriteLine(string.Format("접속 Error: {0} - {1}", strWebSetSocketIP, ex.Message));
            }

        }

        public static async Task Connect(string uri)
        {

            try
            {
                _webSocket = new ClientWebSocket();
                await _webSocket.ConnectAsync(new Uri(uri), CancellationToken.None);
                await Task.WhenAll(Receive(_webSocket), Send(_webSocket));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                if (_webSocket != null)
                    _webSocket.Dispose();
                Console.WriteLine();

                lock (consoleLock)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("WebSocket closed.");
                    Console.ResetColor();
                }
            }
        }



        // String을 바이트 배열로 변환
        private byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }


     
        private static async Task Send(ClientWebSocket webSocket,string strSendData)
        {

            byte[] buffer = encoder.GetBytes(strSendData);//Data변환
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

        }


        private  void SendData(string strSendData)
        {
#if true
            if (null != _webSocket)  {
                //OK 
                Task t = Send(_webSocket, strSendData);
                t.Wait();
            }
#else

            if (null != _webSocket)
            {

                 SendDataEx(_webSocket, Encoding.UTF8.GetBytes(strSendData), PayloadDataType.Text);
            }
#endif
            Console.WriteLine("값 전송...:", strSendData);
        }

        public static  void  SendDataEx(ClientWebSocket websocket, byte[] data, PayloadDataType opcode)
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

            //    messageStream.Write(sendData, 0, sendData.Length);  //클라이언트에 전송
          //  await websocket.SendAsync(new ArraySegment<byte>(sendData), WebSocketMessageType.Text, true, CancellationToken.None);


        }


        public static async Task SendTest(ClientWebSocket websocket, string strSendData="")
        {
            string message = "Hello World";
            if("" != strSendData)
            {
                message = strSendData;
            }

            //Console.WriteLine("Sending message: " + message);
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            await websocket.SendAsync(new ArraySegment<byte>(messageBytes), WebSocketMessageType.Text, true, CancellationToken.None);

            byte[] incomingData = new byte[1024];
            WebSocketReceiveResult result = await websocket.ReceiveAsync(new ArraySegment<byte>(incomingData), CancellationToken.None);

        }

        private static async Task Send(ClientWebSocket webSocket)
        {

            //Daem에서 주기적으로 서버에 값 전달
            byte[] buffer = encoder.GetBytes("{\"daemon\":\"connect_open\"}");
            await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

            while (webSocket.State == WebSocketState.Open)
            {
#if false
                LogStatus(false, buffer, buffer.Length);
#endif
                await Task.Delay(delay);
            }
        }

        private static async Task Receive(ClientWebSocket webSocket)
        {
            byte[] buffer = new byte[receiveChunkSize];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
                else
                {
                    LogStatus(true, buffer, result.Count);
                }
            }
        }

        private static void LogStatus(bool receiving, byte[] buffer, int length)
        {
            //1. Lock를 시작 -외부 호출 금지
            lock (consoleLock)
            {
               // Console.ForegroundColor = receiving ? ConsoleColor.Green : ConsoleColor.Gray;
                //Console.WriteLine("{0} ", receiving ? "Received" : "Sent");

                if (verbose) {
                    string strText = encoder.GetString(buffer);
                    Console.WriteLine(strText);// encoder.GetString(buffer));

                    string httpRequest = Encoding.UTF8.GetString(buffer);

                    if ("" != httpRequest )//.IndexOf("RleayCOM") > 0)
                    {

                        //1. Invoke 함수 호출
                      wSocketClient ws = wSocketClient._instance;
                      string strData =  ws.StringByteToStringDesrilizeEx(httpRequest);
                      ws.funcCommand(strData);

                    }

                    Console.WriteLine(httpRequest);
                }

               // Console.ResetColor();
            }
        }

        private static bool IsValidJson(string strInput)
        {
            string stringValue = strInput;
            if (string.IsNullOrWhiteSpace(stringValue)) { return false; }
            strInput = strInput.Trim();
            if ((strInput.StartsWith("{") && strInput.EndsWith("}")) || //For object
                (strInput.StartsWith("[") && strInput.EndsWith("]"))) //For array
            {
                try
                {
                    //var obj = JToken.Parse(strInput);
                    var obj = new System.Web.Script.Serialization.JavaScriptSerializer();// strInput);
                    return true;
                }
                catch (Exception ex) //some other exception
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        private string StringByteToStringDesrilize(string JSONdata)
        {
            bool bContinue = false;
            string strData = "";
            if (string.IsNullOrWhiteSpace(JSONdata)) { return strData ; }
            char[] ch = new char[JSONdata.Length];

            for (int n = 0; n < JSONdata.Length; n++)
            {
                ch[n] = JSONdata[n];
                if (0 != ch[n])
                {
                    //if ('{' == ch[n] && false==bContinue)
                    //{
                    //    bContinue = true;
                    //}

                    strData += ch[n];

                    //if ('}' == ch[n] && true==bContinue)
                    //{
                    //    break;   
                    //}

                    //strData += ch[n];
                    
                }
            }


            return strData;
        }

        private string StringByteToStringDesrilizeEx(string JSONdata)
        {
            bool bContinue = false;
            string strData = "";
            if (string.IsNullOrWhiteSpace(JSONdata)) { return strData; }
            char[] ch = new char[JSONdata.Length];

            for (int n = 0; n < JSONdata.Length; n++)
            {
                ch[n] = JSONdata[n];
                if (0 != ch[n])
                {
                    if ('{' == ch[n] && false == bContinue)
                    {
                        bContinue = true;
                    }

                    strData += ch[n];

                    if ('}' == ch[n] && true == bContinue)
                    {
                        break;
                    }

                    //strData += ch[n];

                }
            }


            return strData;
        }


        private int funcCommand(string strData) {

            if (string.IsNullOrWhiteSpace(strData)) { return  -1; }

            if ("" != strData)
            {
                string json1 = strData;// JSONdata;
                if(7 == json1.Length  && json1.ToLower() == "success")
                {
                    return 1;// true;
                }

                JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();

                dynamic dict= null;
                try
                {
                     dict = jsonSerializer.Deserialize<dynamic> (json1);
                }catch(Exception ex)
                {
                    //1. 문자열이 JSON 인지 파악 필요함.
                    Console.WriteLine(ex.Message);
                    string strErr = string.Format("{0}\n{1}", ex.Message, json1);
                    MessageBox.Show(strErr,"error:Func");

                    return -1;// false;
                }


                dynamic name = dict["name"]; // result is Dictio
                dynamic port = dict["port"]; // result is Dictio


                if ("RleayCOM" == name)
                {
                    //   Console.WriteLine("Log Test", json);
                    Console.WriteLine("port", port);
                    PythonClass.ConnectFuncCallUSB2CAN(port);// "COM7");
                }else if ("Multimeter" == name)
                {
                    dynamic ipadress = dict["ipadress"]; // result is Dictio

                    string vIpadress = string.Format("{0}:{1}", ipadress, port);

                    string strBuffer = PythonClass.ConnectFuncCallTCP(vIpadress);

                    string Value = strBuffer.Replace("\r\n", "");
                     Value = strBuffer.Replace("\n", "");

                    string JsonString = "{";
                    JsonString += string.Format("\"name\" : \"{0}\"", name);
                    JsonString += string.Format(",\"value\" : \"{0}\"", Value);
                    JsonString += "}";


                    SendData(JsonString);
                }

            }


            return 0;// false;

        }


    }
}
