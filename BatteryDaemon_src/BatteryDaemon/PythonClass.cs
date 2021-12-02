using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BatteryDaemon
{
    public  class PythonClass
    {

        static string _strTitle;

        public bool CallFileView(string subTitle, string menuName)
        {
            string menuName1 = "setting";
            //1. Iron Python WPF 화면 띄우기
            Console.WriteLine(menuName);// "Settting");
            string subDirPath = "menu";//디렉토리
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            var pyfile = "";

            CJsonParser cjson = new CJsonParser();

            SetTitleSetting(subTitle);//환경설정 타이틀

            string strRoot = System.Environment.CurrentDirectory;
            string strSettingMenu = cjson.jsonMenuParsing(strRoot, subDirPath);
            if ("" == strSettingMenu)
            {
                strSettingMenu = "settingform";
                strSettingMenu += ".py";
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
                    MessageBox.Show(string.Format("{0}", ex.Message) , string.Format("Daemon Menu : {0}", menuName));
                    Console.WriteLine(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show(string.Format("File Not Found :\n{0}", pyfile)  , string.Format("Daemon Menu-2 : {0}", menuName));
            }


            return result;

        }

        //환성설정에서 호출할 함수명 정의 한다.
        private bool PyCallSetFounctions(ScriptScope scope)
        {
            bool bresut = false;

            scope.SetVariable("ConnectFuncCallTCP", new Func<string, string>(PythonClass.ConnectFuncCallTCP));
            scope.SetVariable("ConnectFuncCallRs232c", new Func<string, string>(PythonClass.ConnectFuncCallRs232c));
            scope.SetVariable("ConnectFuncCallUSB2CAN", new Func<string, string>(PythonClass.ConnectFuncCallUSB2CAN));

            scope.SetVariable("ConnectFuncWebSocket", new Func<string, int,string>(PythonClass.ConnectFuncWebSocket));

            scope.SetVariable("GetTitleSetting", new Func<string>(PythonClass.GetTitleSetting));

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
            string strResult="";// S_OK = "1";
            string[] adress =vIpadress.Split(':');
            string strIP = "";
            int nPort = 0;
            if( adress.Length == 2)
            {
                strIP = adress[0];
                nPort = int.Parse(adress[1]);
            }

            ClassTCPClient client = new ClassTCPClient();
            string  strRET = client.connectTry(strIP, nPort);

            strResult = StringByteToStringDesrilize(strRET);

            return strResult;
        }

        static public string ConnectFuncCallRs232c(string strCOM)
        {
            string strResult = "";
            ClassRS232C rs232 = new ClassRS232C();

            string comPort = strCOM;
            strResult = rs232.connect(comPort);

            return strResult;
        }

        static public string ConnectFuncCallUSB2CAN(string strCOM)
        {
            string strResult = "";
            ClassUSB2CAN usbCANST5520 = new ClassUSB2CAN();

            string comPort = strCOM;
            strResult = usbCANST5520.connect(comPort);

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

            string wsIPAdress = string.Format("ws://{0}:{1}/", ip, nPort);
            Console.WriteLine("결과:{0}", result);

#if false
            webSocketClient wb = new webSocketClient(wsIPAdress);
            wb.StartWS();
#else

            if (wSocketClient.getInstance().getConnectIP() != ip )
            {
                wSocketClient.getInstance().close();//.stop();
            }

            wSocketClient.getInstance(ip,nPort).task_webSocketClient(wsIPAdress);
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


    }
}
