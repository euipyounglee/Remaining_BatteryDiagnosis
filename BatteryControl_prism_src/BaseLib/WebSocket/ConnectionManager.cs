using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BaseLib.WebSocket
{
    public class ConnectionManager
    {


        private readonly TcpListener tcpListener = null;

        public WebSocketController _webSocketController = null;
        private static ConnectionManager _instance = null;
        private static int _wport = 8005;

        private static string _address = "";
        public ConnectionManager(string IPaddress, int port)
        {
            string address = IPaddress;
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

            //tcpListener.BeginR
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

            Debug.WriteLine("client connect " + _webSocketController.State.ToString());//파라메터값이 달라고 접속 한다.
#if true
            MessageBox.Show("Client - GateWay 와 연결 됨");
#else

#endif
            //다음 클라이언트를 대기
            tcpListener.BeginAcceptTcpClient(OnAcceptClient, null);
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
                    }
                    catch (Exception ex)
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

        private void showSubnetMask()
        {
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
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



}
