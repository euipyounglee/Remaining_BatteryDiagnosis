using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BatteryDashBoard.View
{
    class ClassSettingView
    {

        static string _strTitle;
        ConnectionManager _wsServer;

        static string _strValue = "";

        public ClassSettingView()
        {
            Console.WriteLine("ClassSetting-view");
        }

        public ClassSettingView(ConnectionManager ws)
        {
            _wsServer = ws;
            Console.WriteLine("ClassSetting_view");
        }

        public bool CallFileView(string subTitle, string menuName)
        {
            string menuName1 = "setting";
            //1. Iron Python WPF 화면 띄우기
            Console.WriteLine(menuName);// "Settting");


            SetTitleSetting(subTitle);
            string subDirPath = "menu";//디렉토리
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            var pyfile = "";

            CJsonParser cjson = new CJsonParser();

            string strRoot = System.Environment.CurrentDirectory;
            string strSettingMenu = cjson.jsonMenuParsing(strRoot, subDirPath);
            if ("" == strSettingMenu)
            {
                strSettingMenu = "settingform";
                //   strSettingMenu += "_old";
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
                    var var1 = "22";
                    result = Apply_func(var1);

                    Console.WriteLine("결과:{0}", result);
                    //JsonConfig 파일 Write()
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}", ex.Message), string.Format("Daemon Menu-1 : {0}", menuName));
                    Console.WriteLine(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show(string.Format("File Not Found :\n{0}", pyfile), string.Format("Daemon Menu-2 : {0}", menuName));
            }


            return result;

        }

        //환성설정에서 호출할 함수명 정의 한다.
        private bool PyCallSetFounctions(ScriptScope scope)
        {
            bool bresut = false;

            scope.SetVariable("ConnectFuncCallTCP", new Func<string, string>(ClassSettingView.ConnectFuncCallTCP));//Multimeter
            scope.SetVariable("ConnectFuncCallRs232c", new Func<string, string>(ClassSettingView.ConnectFuncCallRs232c));//절연저항 시험기
            scope.SetVariable("ConnectFuncCallUSB2CAN", new Func<string, string>(ClassSettingView.ConnectFuncCallUSB2CAN));//Relay Controller
            
            scope.SetVariable("GetTitleSetting", new Func<string >(ClassSettingView.GetTitleSetting));


            return bresut;
        }

        public void webServerConnect(string ip, string port)
        {
            var result = false;
            Console.WriteLine("결과:{0}", result);
        }


        //환경설정의 타이틀이름 설정
        static public void SetTitleSetting(string strText)
        {
            _strTitle = strText;
        }

        //환경설정의 타이틀이름 얻어오기
        static public string GetTitleSetting()
        {
            var result = "false";
            Console.WriteLine("결과:{0}", result);

            return _strTitle;
        }



        static public string ConnectFuncCallTCP(string vIpadress)
        {
            var result = false;

            Console.WriteLine("결과:{0}", vIpadress);

            string S_OK = "200";
            if ("" == vIpadress)
            {

                CJsonParser cjson = new CJsonParser();

                string strJson = _strValue;

                string strValue = cjson.jsonDataParsing(strJson, "value");

                if ("" != strValue)  {
                    //1.파이썬으로 값을 돌려준다.
                    S_OK = strValue;// _strValue;
                }
                else  {
                    S_OK = "100";
                }



            }
            else
            {
                string[] adress = vIpadress.Split(':');
                string strIP = "";
                int nPort = 0;
                string port = "80";

                if (adress.Length == 2)
                {
                    strIP = adress[0];
                    nPort = int.Parse(adress[1]);

                    port = adress[1];
                }

                //ClassTCPClient client = new ClassTCPClient();
                //S_OK = client.connectTry(strIP, nPort);


                string name = "Multimeter";
                string JsonString = "{";
                JsonString += string.Format("\"name\" : \"{0}\"", name);
                JsonString += string.Format(",\"ipadress\" : \"{0}\"", strIP);
                JsonString += string.Format(",\"port\" : {0}", port);
                JsonString += "}";


                //1. 멀티미터에 값을 전달해준다.
                // S_OK ="값 전달하기"

                S_OK = callSend(JsonString);
            }

            return S_OK;
        }

        static public string ConnectFuncCallRs232c(string strCOM)
        {
            string strResult = "";
            //ClassRS232C rs232 = new ClassRS232C();

            string comPort = strCOM;
            //strResult = rs232.connect(comPort);

            return strResult;
        }

        static public string ConnectFuncCallUSB2CAN(string strCOM)
        {
            string strResult = "";
            //ClassUSB2CAN usbCANST5520 = new ClassUSB2CAN();

            string comPort = strCOM;
            //strResult = usbCANST5520.connect(comPort);

#if false

            MainWindow main = new MainWindow();
            string name = "RleayCOM";
            string JsonString = "{";
            JsonString += string.Format("\"name\" : \"{0}\",", name);
            JsonString += string.Format("\"comPort\" : \"{0}\"",comPort);
            JsonString += "}";

            main.sendData(JsonString);
#else

            string name = "RleayCOM";
            string JsonString = "{";
            JsonString += string.Format("\"name\" : \"{0}\"", name);
            JsonString += string.Format(",\"port\" : \"{0}\"", comPort);
            JsonString += "}";

            callSend(JsonString);
#endif

            return strResult;
        }

        private static string callSend(string JsonString)
        {

            string strReslut = "";
            MainWindow main = new MainWindow();


            strReslut = main.sendData(JsonString);

            return strReslut;// false;
        }


        public  void ReusltAsync(string response)
        {

            _strValue = response;

            Console.WriteLine(":"+response);

        }

    }
}
