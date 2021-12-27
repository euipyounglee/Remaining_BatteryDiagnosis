using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryGateway
{
    public  class PythonClass
    {

        static string _strTitle;
        static  ScriptScope _scope;

        public bool CallFileView(string subTitle, string menuName)
        {
            string menuName1 = "setting";
            //1. Iron Python WPF 화면 띄우기
            Console.WriteLine(menuName);// "Settting");
            string subDirPath = "menu";//디렉토리
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            _scope = scope;

            var pyfile = "";

            CJsonParser cjson = new CJsonParser();

            SetTitleSetting(subTitle);//환경설정 타이틀

            string strRoot = System.Environment.CurrentDirectory;
            string strSettingMenu = cjson.jsonMenuParsing(strRoot, subDirPath);
            if ("" == strSettingMenu)
            {
                strSettingMenu = "settingform.py";
            }

            pyfile = string.Format("{0}\\{1}\\{2}", strRoot, subDirPath, strSettingMenu);
            var result = false;
            if (File.Exists(pyfile)) //파일 존재 확인
            {
                try
                {
                    // 함수 전달하기
                    PyCallSetFounctions(scope);

                    engine.ExecuteFile(pyfile, scope);

                    //화면이 닫이면(파이썬 화면이종료) 함수 호출이 된다.
                    dynamic Apply_func = scope.GetVariable("Apply_func");
                    var var1 = "json-save-ok";
                    
                    result = Apply_func(var1);
             

                    Console.WriteLine("결과:{0}", result);
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}", ex.Message) , string.Format("BatteryGateway Menu : {0}", menuName));
                    Console.WriteLine(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show(string.Format("File Not Found :\n{0}", pyfile)  , string.Format("BatteryGateway Menu-2 : {0}", menuName));
            }


            return result;

        }

        //환성설정에서 호출할 함수명 정의 한다.
        private bool PyCallSetFounctions(ScriptScope scope)
        {
            bool bresut = false;

            if (null != scope)
            {
                scope.SetVariable("ConnectFuncCallTCP", new Func<string, string>(PythonClass.ConnectFuncCallTCP));
                scope.SetVariable("ConnectFuncCallRs232c", new Func<string, string>(PythonClass.ConnectFuncCallRs232c));
                scope.SetVariable("ConnectFuncCallUSB2CAN", new Func<string, string>(PythonClass.ConnectFuncCallUSB2CAN));
                scope.SetVariable("ConnectFuncWebSocket", new Func<string, int, string>(PythonClass.ConnectFuncWebSocket));
                scope.SetVariable("GetTitleSetting", new Func<string>(PythonClass.GetTitleSetting));
                scope.SetVariable("ConnectFuncPNEConnect", new Func<string>(PythonClass.ConnectFuncPNEConnect)); //PNE 연결 버튼

               // scope.SetVariable("getPythonFunc", new Func<string, string>(PythonClass.getPythonFunc)); //PNE 연결 버튼 호출



                bresut = true;
            }

            return bresut;
        }

        public void webServerConnect(string ip, string port)
        {
            var result = false;
            Console.WriteLine("결과:{0}", result);
        }


        private static string StringByteToStringDesrilize(string JSONdata)
        {

            string strData = "";

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


        static public string ConnectFuncCallTCP(string vIpadress )
        {
           // var result = false;
            Console.WriteLine("결과:{0}", vIpadress);
            string strResult = "";// S_OK = "1";

            ClassMultimeter meter = new ClassMultimeter();
            string strRET =  meter.ConnectTCP(vIpadress);

            strResult = StringByteToStringDesrilize(strRET);


            return strRET;
        }

        static public string ConnectFuncCallRs232c(string strCOM)
        {
            string strResult = "";
            ClassRS232C rs232 = new ClassRS232C();

            string comPort = strCOM;
            if ("" != comPort)  {
                strResult = rs232.connect(comPort);
            }

            return strResult;
        }

        static public string ConnectFuncCallUSB2CAN(string strCOM)
        {
            string strResult = "";
#if false

            ClassUSB2CAN usbCANST5520 = new ClassUSB2CAN();

            string comPort = strCOM;
            strResult = usbCANST5520.connect(comPort);
#else

            string comPort = strCOM;
            if ("" != strCOM)
            {
                strResult = systemBase_uCANSend(comPort);//SystemBase- uCAN
            }

#endif

            return strResult;
        }

        static public string usbCANST5520Send(string strCOM)
        {
            string strResult = "";
            ClassUSB2CAN usbCANST5520 = new ClassUSB2CAN();

            string comPort = strCOM;
            strResult = usbCANST5520.connect(comPort);

            return strResult;
        }


        static public string systemBase_uCANSend(string strCOM)
        {
            string strResult = "";
            int nBaudRate = 115200;
            ClassRS232C rs232 = new ClassRS232C();
            string[] strformats =  { "Standard_t", "StardardRmote_T","Extended_e" ,"ExtendedRemote_E" };

            string formatType = "extended";
            string mode = "";

            foreach (string str in strformats)
            {
                string[]  formats = str.Split('_');
                if(formats[0].ToLower() == formatType.ToLower())
                {
                    mode = formats[1];
                    break;
                }

            }


            int count = 8;//총개수
            string strID = "0CFA00D";
            string comPort = strCOM;
            string[] strDataSample = { "0000000000000000", "0001000000000001", "0000000000000000", "0002000000000002", "0000000000000003" };
            string  strData = string.Format("{0}{1}{2:00}{3}", mode, strID, count, strDataSample[0]);
                    strData += string.Format(",{0}{1}{2:00}{3}", mode,strID, count, strDataSample[1]);
                    strData += string.Format(",{0}{1}{2:00}{3}", mode,strID, count, strDataSample[2]);


            string phrase = strData;
            string[] words = phrase.Split(',');

            foreach (string word in words)
            {

                string   strWord = word +"\r";
                System.Console.WriteLine($"<{strWord}>");

                byte[] byData = Encoding.ASCII.GetBytes(strWord);

                strResult = rs232.connectCAN(comPort, nBaudRate, byData);
                //      Thread.Sleep(2000);
                strResult = "OK";
            }



            return strResult;
        }
        

        static public string ConnectFuncWebSocket(string ip, int  port)
        {
            string strResult = "";
            var result = false;
            int nPort = 80;
            if ("" == ip) return strResult;// ""


            if( port >= 80)
            {
                nPort = port; 
            }


            string strIPTemp = ip;

            if (ip.Contains("ws://") || ip.Contains("wss://"))
            {
                strIPTemp  = strIPTemp.Replace("ws://", "");
            }



            strIPTemp = strIPTemp.Replace(":", ",");//IP와포트 구분 
            strIPTemp = strIPTemp.Replace("/", ",/");//path


            //string wsIPAdress = string.Format("ws://{0}:{1}/", ip, nPort);
            string wsIPAdress = string.Format("ws://{0}", ip );
            Console.WriteLine("결과:{0}", result);

#if false

            if (wSocketClient.getInstance().getConnectIP() != ip )
            {
                wSocketClient.getInstance().close();//.stop();
            }

            wSocketClient.getInstance(ip,nPort).task_webSocketClient(wsIPAdress);
#else

            string[] arrayAdress = strIPTemp.Split(',');

            string address="";
            int wport = 0;// 3268;
            string path = "";// "/battery";

            if(arrayAdress.Length == 3)
            {
                address  = arrayAdress[0];
                wport =  Int32.Parse(arrayAdress[1]);
                path = arrayAdress[2];
            }
            else
            {
                address = "";
                wport =  3268;
                path =  "/battery";

            }



            webSocketSharpClient.getInstance(address, wport, path).connect();
#endif

            return strResult;


        }

        static public void SetTitleSetting(string strText)
        {
            _strTitle = strText;
        }

        //환경설정의 타이틀이름 설정
        static public string GetTitleSetting()
        {
            var result = "false";
            Console.WriteLine("결과:{0}", result);

            return _strTitle;
        }

        static public string ConnectFuncPNEConnect()
        {
            var result = "false";

#if false
            try
            {
                Core Devices = new Core();

                Devices.Connect();
            }catch(Exception ex)
            {
                result = "false" + ex.Message;
            }
#else

            ClassPneCtsLib pne = new ClassPneCtsLib(_scope);
            pne.connect();

#endif
            Console.WriteLine("PNE 결과:{0}", result);

            return result;
        }

        static public string getPythonFunc(string strArg)
        {

            var getPythonFuncResult = _scope.GetVariable<Func<string,string>>("getPythonFunc");

            //getPythonFuncResult();
            Console.WriteLine("def 실행 테스트 : " + getPythonFuncResult("madla"));

            return "OK-1";
        }

    }
}
