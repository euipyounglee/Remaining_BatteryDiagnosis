using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;  //시리얼통신을 위해 추가해줘야 함
using System.Threading;
using System.Windows.Forms;

namespace BatteryDaemon
{
    class ClassRS232C
    {

        SerialPort serialPort1 = null;

        public string  connect(string strCOM)
        {
            string strResult = "";
            string[] nPorts = SerialPort.GetPortNames(); //연결 가능한 시리얼포트 이름을 콤보박스에 가져오기 
            serialPort1 =  new  SerialPort();

            if (!serialPort1.IsOpen)  //시리얼포트가 열려 있지 않으면
            {

                serialPort1.PortName = strCOM;// comboBox_port.Text;  //콤보박스의 선택된 COM포트명을 시리얼포트명으로 지정
                serialPort1.BaudRate = 9600;  //보레이트 변경이 필요하면 숫자 변경하기
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                //serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); //이것이 꼭 필요하다



                serialPort1.ReadTimeout = 1000 * 10;
                serialPort1.WriteTimeout  = 1000 * 5;

                try
                {
                    serialPort1.Open();  //시리얼포트 열기

                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //MessageBox.Show(ex.Message, "Connect Error");
                    strResult = ex.Message;

                }

                if (serialPort1.IsOpen)
                {

                    //  label_status.Text = "포트가 열렸습니다.";
                    // comboBox_port.Enabled = false;  //COM포트설정 콤보박스 비활성화
                    string[] strCommads = { ":MEASure?", ":TIMer?" };
                    string strCommad = ":MEASure?";

                    strCommad = strCommads[0];
                    strCommad += "\r\n";//꼭넣어줘야 한다. 명령어 마지막이라는 표시


                    serialPort1.Write(strCommad);// ":MEASure?");
                    Thread.Sleep(1800);//너무 빠르면 값을 얻어 오지 못한다.

                    ////수신 메세지
                    ///


                    int RecvSize = serialPort1.BytesToRead;
                    string RecvStr = string.Empty;
                    // Recv Data가 있는 경우...
                    if (RecvSize != 0)
                    {
                        byte[] buff = new byte[RecvSize];

                        // Size 만큼 Read...
                        serialPort1.Read(buff, 0, RecvSize);
                        RecvStr = Encoding.ASCII.GetString(buff);

                        strResult = RecvStr;
                    }

                    serialPort1.Close();

                }

                // serialPort1.ReadBufferSize();//.ReadBufferSize();//.ReadByte();
            }
                // label_status.Text = "포트가 이미 열려 있습니다.";
            if(null != serialPort1 && serialPort1.IsOpen)
                  serialPort1.Close();

            return strResult;
        }

        public string connect(string strCOM,int BaudRate)
        {
            string strResult = "";
            string[] nPorts = SerialPort.GetPortNames(); //연결 가능한 시리얼포트 이름을 콤보박스에 가져오기 
            serialPort1 = new SerialPort();

            if (!serialPort1.IsOpen)  //시리얼포트가 열려 있지 않으면
            {

                serialPort1.PortName = strCOM;// comboBox_port.Text;  //콤보박스의 선택된 COM포트명을 시리얼포트명으로 지정
                serialPort1.BaudRate = BaudRate;// 9600;  //보레이트 변경이 필요하면 숫자 변경하기
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                //serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); //이것이 꼭 필요하다



                serialPort1.ReadTimeout = 1000 * 10;
                serialPort1.WriteTimeout = 1000 * 5;

                try
                {
                    serialPort1.Open();  //시리얼포트 열기

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //MessageBox.Show(ex.Message, "Connect Error");
                    strResult = ex.Message;

                }

                if (serialPort1.IsOpen)
                {

                    //  label_status.Text = "포트가 열렸습니다.";
                    // comboBox_port.Enabled = false;  //COM포트설정 콤보박스 비활성화
                    string[] strCommads = { ":MEASure?", ":TIMer?" };
                    string strCommad = ":MEASure?";

                    strCommad = strCommads[0];
                    strCommad += "\r\n";//꼭넣어줘야 한다. 명령어 마지막이라는 표시


                    serialPort1.Write(strCommad);// ":MEASure?");
                    Thread.Sleep(1800);//너무 빠르면 값을 얻어 오지 못한다.

                    ////수신 메세지
                    ///


                    int RecvSize = serialPort1.BytesToRead;
                    string RecvStr = string.Empty;
                    // Recv Data가 있는 경우...
                    if (RecvSize != 0)
                    {
                        byte[] buff = new byte[RecvSize];

                        // Size 만큼 Read...
                        serialPort1.Read(buff, 0, RecvSize);
                        RecvStr = Encoding.ASCII.GetString(buff);

                        strResult = RecvStr;
                    }

                    serialPort1.Close();

                }

                // serialPort1.ReadBufferSize();//.ReadBufferSize();//.ReadByte();
            }
            // label_status.Text = "포트가 이미 열려 있습니다.";
            if (null != serialPort1 && serialPort1.IsOpen)
                serialPort1.Close();

            return strResult;
        }


      //  Write(byte[] buffer, int offset, int count);
        public string connectCAN(string strCOM, int BaudRate, byte[] Buff )//string strCommad)
        {
            string strResult = "";
            string[] nPorts = SerialPort.GetPortNames(); //연결 가능한 시리얼포트 이름을 콤보박스에 가져오기 
            serialPort1 = new SerialPort();

            if (!serialPort1.IsOpen)  //시리얼포트가 열려 있지 않으면
            {

                serialPort1.PortName = strCOM;// comboBox_port.Text;  //콤보박스의 선택된 COM포트명을 시리얼포트명으로 지정
                serialPort1.BaudRate = BaudRate;// 9600;  //보레이트 변경이 필요하면 숫자 변경하기
                serialPort1.DataBits = 8;
                serialPort1.StopBits = StopBits.One;
                serialPort1.Parity = Parity.None;
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); //이것이 꼭 필요하다



                serialPort1.ReadTimeout = 1000 * 10;
                serialPort1.WriteTimeout = 1000 * 5;

                try
                {
                    serialPort1.Open();  //시리얼포트 열기

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    //MessageBox.Show(ex.Message, "Connect Error");
                    strResult = ex.Message;

                }

                if (serialPort1.IsOpen)
                {

                    //  label_status.Text = "포트가 열렸습니다.";
                    // comboBox_port.Enabled = false;  //COM포트설정 콤보박스 비활성화
                    //    string[] strCommads = { ":MEASure?", ":TIMer?" };
                    //     string strCommad = ":MEASure?";

                    //  strCommad = strCommads[0];
                    //   strCommad += "\r\n";//꼭넣어줘야 한다. 명령어 마지막이라는 표시

                    //  byte[]  RecvStr = Encoding.ASCII.GetBytes();//.GetString(buff);


                    //     serialPort1.Write(strCommad);// ":MEASure?");

                   // byte[] buffer = new byte[16];
                    int offset = 0;
                    int count = Buff.Length;// 0;


                    serialPort1.Write(Buff,  offset,  count);

                    Thread.Sleep(1800);//너무 빠르면 값을 얻어 오지 못한다.

                    ////수신 메세지
                    ///


                    int RecvSize = serialPort1.BytesToRead;
                    string RecvStr = string.Empty;
                    // Recv Data가 있는 경우...
                    if (RecvSize != 0)
                    {
                        byte[] buff = new byte[RecvSize];

                        // Size 만큼 Read...
                        serialPort1.Read(buff, 0, RecvSize);
                        RecvStr = Encoding.ASCII.GetString(buff);

                        strResult = RecvStr;
                    }

                    serialPort1.Close();

                }

                // serialPort1.ReadBufferSize();//.ReadBufferSize();//.ReadByte();
            }
            // label_status.Text = "포트가 이미 열려 있습니다.";
            if (null != serialPort1 && serialPort1.IsOpen)
                serialPort1.Close();

            return strResult;
        }



        private  void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            int RecvSize = serialPort1.BytesToRead;
            string RecvStr = string.Empty;
            // Recv Data가 있는 경우...
            if (RecvSize != 0)
            {
                byte[] buff = new byte[RecvSize];

                // Size 만큼 Read...
                serialPort1.Read(buff, 0, RecvSize);
                for (int i = 0; i < RecvSize; i++)
                {
                    // Hex 변환...
                    foreach (byte bData in buff)
                        RecvStr += " " + buff[i].ToString("X2");
                }
                //textBox.Text += RecvStr;
            }
        }


    }
}
