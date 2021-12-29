using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace BatteryGateway
{
    class webSocketSharpClient
    {



        WebSocket _webSocket;
        static UTF8Encoding encoder = new UTF8Encoding();

        private static webSocketSharpClient _instance = null;
        private static string _address = "";
        private static int _wport = 80;
        private static string _path = "";


        public webSocketSharpClient(string address, int wport, string path)
        {

            Console.WriteLine("Press any key to exit...");

            if ("" == address)
            {
                address = "localhost";// Get_MyIP();// "127.0.0.1";
            }
            else if("0.0.0.0" == address)
            {
                address = Get_MyIP();// "127.0.0.1";
            }

            _address = address;
            _wport = wport;
            _path = path;

        }

        public static webSocketSharpClient getInstance(string address, int wport, string path = "/")
        {
            if (null == _instance)
            {
                _instance = new webSocketSharpClient(address, wport, path);
            }

            return _instance;
        }

        //>> 사용하지 않음. 추후 사용 할지 몰라 남겨 뒀다.
        public static webSocketSharpClient getInstance()
        {
            if (null == _instance)
            {

                int wport = _wport;
                string address = "";// 127.0.0.1";
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
#if true
                        address = strIP;
                        wport = nPort;
                        path = strPath;
#else
                        //Test 용
                        address = "192.168.30.137";// 
                        wport = 3268;// 
                        path = "/battery";// 
#endif
                    }
                    catch (Exception ex)
                    {
                        address = "127.0.0.1";
                        wport = 80;
                        Console.WriteLine(ex.Message);
                    }
                }

                if (_address != address && "" != _address)
                {
                    address = _address;
                }

                _instance = new webSocketSharpClient(address, wport, path);
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

        public static dynamic jsonParsingWebSocketValue(string key)
        {
            CJsonParser cjson = CJsonParser.Instatce();// new CJsonParser();
            dynamic dobj = cjson.getObject(key);
            return dobj;
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
                if (WebSocketState.Closed != _webSocket.ReadyState)
                {
                    _webSocket.Close();
                    _webSocket = null;
                }
                //_webSocket = null;
            }

        }

        public void close()
        {
            if (null != _instance)
            {
                stop();

                _instance = null;
            }
        }


        void connect_Sample(string address)
        {
            using (var ws = new WebSocket(address))
            //using (var ws = new WebSocket("ws://192.168.0.5:3268/battery"))
            {
                _webSocket = ws;//전역 복사
                Console.WriteLine("ReayState=" + ws.ReadyState.ToString());

                ws.OnOpen += (sender, e) =>
                {
                    Console.WriteLine("OnOpen");

                };

                ws.OnClose += (sender, e) =>
                {
                    Console.WriteLine("OClose");

                };

                ws.OnMessage += (sender, e) =>
                {
                    Console.WriteLine("OnMessage" + e.Data);

                };

                ws.OnError += (sender, e) =>
                {
                    Console.WriteLine("OnError");

                };

                ws.Connect();
                Console.WriteLine("exit \n");
                while (true)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("> ");
                    var msg = Console.ReadLine();

                    if (msg == "exit")
                        break;

                    ws.Send(msg);
                }

            }
        }


        public void connect()
        {
            string wsIPadress = string.Format("ws://{0}:{1}{2}", getConnectIP(), getConnectPort(), getConnectPath());
#if true
            connect(wsIPadress);
#else

            connect_Sample(wsIPadress);

#endif

        }

        void connect(string address)  {

            try
            {
                _webSocket = new WebSocket(address);//생성

                _webSocket.OnOpen += Ws_OnOpen; //Conenct 알림
                _webSocket.OnClose += Ws_OnClose;//서버가 닫아졌을때..
                _webSocket.OnMessage += Ws_OnMessage;
                _webSocket.OnError += Ws_OnError;

                if (null != _webSocket)
                {
                    _webSocket.Connect();
                    //Console.WriteLine("connect : \n" + _webSocket.ReadyState);
                }

            }catch(Exception ex)
            {
                close();
                Console.WriteLine(ex.Message);
            }

        }



        private void Ws_OnOpen(object sender, EventArgs e)
        {

            Console.WriteLine("OnOpen");

            string ResponeOK = "{\"daemon\":\"connect_open\"}";
            byte[] buffer = encoder.GetBytes(ResponeOK);// "{\"daemon\":\"connect_open\"}");
            Send(ResponeOK);// "ok");

        }

        private void Ws_OnClose(object sender, CloseEventArgs e)
        {
            if (null != _webSocket)
            {
                close();
            }

            //1.타이머 - 서버 연결 재 시도 하기 
            Console.WriteLine("OnClose");
        }

        private void Ws_OnMessage(object sender, MessageEventArgs e)
        {
            Console.WriteLine("Ws_OnMessage:\n" + e.Data);
            //throw new NotImplementedException();
            string strCommand = e.Data;
            ClassCommand.getInstance().funcCommand(strCommand);
        }

        private void Ws_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("Ws_OnError");
            throw new NotImplementedException();
        }


        private byte[] StringToByte(string str)
        {
            byte[] StrByte = Encoding.UTF8.GetBytes(str);
            return StrByte;
        }


        public void Send(string strSendData)
        {
            Send(_webSocket, strSendData);
        }

        public void Send(WebSocket webSocket, string strSendData)
        {
            try
            {
                webSocket.Send(strSendData);
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        static void Run()
        {
            Console.WriteLine("Run");

            while (WebSocketState.Closed != getInstance()._webSocket.ReadyState)
            {
                    Thread.Sleep(1000);
                    Console.WriteLine("> ");
                    var msg = Console.ReadLine();

                    if (msg == "exit")
                        break;

                    //ws.Send(msg);
            }

        }

    }

}
