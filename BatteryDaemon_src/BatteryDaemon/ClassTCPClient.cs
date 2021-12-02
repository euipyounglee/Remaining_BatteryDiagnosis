using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace BatteryDaemon
{
    class ClassTCPClient
    {
        TcpListener Server;

        TcpClient Client;

        StreamReader Reader;

        StreamWriter Writer;

        NetworkStream stream;

        Thread ReceiveThread;

        bool Connected;



        public int  connect(string ipadress,int nPort) // 폼 실행
        {

            String IP = ipadress;// "192.168.x.x"; // 접속 할 서버 아이피를 입력

            int port = nPort;// 8080; // 포트
            if ("" == IP || 0 == port) return 0;

            Client = new TcpClient();

          //  Client.Connect()

            Client.Connect(IP, port);

            stream = Client.GetStream();

            Connected = true;

            //textBox1.AppendText("Connected to Server!" + "\r\n");

            Reader = new StreamReader(stream);

            Writer = new StreamWriter(stream);

            ReceiveThread = new Thread(new ThreadStart(Receive));

            ReceiveThread.Start();

            return 0;

        }


        public string connectTry(string ipadress, int nPort)
        {
            int nResult = 0;
            string ReceiveMessage = "";
            String IP = ipadress;// "192.168.x.x"; // 접속 할 서버 아이피를 입력

            int port = nPort;// 8080; // 포트
            if ("" == IP || 0 == port) return "";

            Client = new TcpClient();

            Client.SendTimeout = 1000 * 5;//5sec
            Client.ReceiveTimeout = 1000 * 5;//5sec

            try { 

              Client.Connect(IP, port);

              if (Client.Connected)
              {

                    NetworkStream ns = Client.GetStream();

                    string[] Messages = { ":MEASure?", "*IDN?" };

                    string Message = Messages[0];// +"\n";
                    Message += "\n"; //붙여준다.

                    byte[] SendByteMessage = Encoding.ASCII.GetBytes(Message);

                    ns.Write(SendByteMessage, 0, SendByteMessage.Length);



                    byte[] ReceiveByteMessage = new byte[32];

                    ns.Read(ReceiveByteMessage, 0, 32);

                    //수신 메세지

                    ReceiveMessage = Encoding.ASCII.GetString(ReceiveByteMessage);

                    ns.Close();

                    Client.Close();
                    if (ReceiveMessage.Length > 0) nResult = 1;


              }



            }  catch(Exception ex)  {

                Client.Close();
                Console.WriteLine(ex.ToString());

                ReceiveMessage = ex.Message;//.ToString();
            }

          
            return ReceiveMessage;// nResult;

        }


        private void Receive() // 서버로 부터 값 받아오기
        {

            //AddTextDelegate AddText = new AddTextDelegate(textBox1.AppendText);

            while (Connected)

            {

                Thread.Sleep(1);

                if (stream.CanRead)

                {

                    string tempStr = Reader.ReadLine();

                    if (tempStr.Length > 0)
                    {

                        //Invoke(AddText, "You : " + tempStr + "\r\n");

                    }

                }

            }

        }


    }
}
