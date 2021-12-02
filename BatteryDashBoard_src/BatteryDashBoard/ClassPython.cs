using IronPython.Hosting;
using IronPython.SQLite;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace BatteryDashBoard
{
    class ClassPython
    {
        ScriptEngine _engine=null;
        ScriptScope _scope = null;
        webServer _wsServer =null;
        ~ClassPython()
        {


            _engine = null;
            _scope = null;
          //  System.gc();
        
        }

        public ClassPython()
        {

        }

        public ClassPython(webServer wsServer)
        {
            _wsServer = wsServer;
        }

        public bool CallFileView2(string subDirPath, string menuName)
        {
            var ipy = Python.CreateRuntime();
            var pyfile = "";
            string strRoot = System.Environment.CurrentDirectory;
            string strSettingMenu = "";// cjson.jsonMenuParsing(strRoot, subDirPath);
            if ("" == strSettingMenu)
            {
                strSettingMenu = "sqlite3db.py";
            }

            pyfile = string.Format("{0}\\{1}\\{2}", strRoot, subDirPath, strSettingMenu);

            if (File.Exists(pyfile))
            {
                dynamic test = ipy.UseFile(pyfile);// "Test.py");
                test.createDB(); //함수 호출

            }
            //  Console.ReadLine();
            return false;
        }

        public bool CallFileView(string subDirPath, string menuName)
        {
            bool bResult = false;
             _engine = Python.CreateEngine();
             _scope = _engine.CreateScope();
            var pyfile = "";

            string strRoot = System.Environment.CurrentDirectory;
            string strSettingMenu = "";
            if ("" == strSettingMenu)
            {
                strSettingMenu = "sqlite3db.py";
            }

            pyfile = string.Format("{0}\\{1}\\{2}", strRoot, subDirPath, strSettingMenu);

            if (File.Exists(pyfile))
            {
                try
                {
                    // 함수 전달하기
                    PyCallSetFounctions(_scope);

                    _engine.ExecuteFile(pyfile, _scope);

                    //화면이 닫이면(파이썬 화면이종료) 함수 호출이 된다.
                    dynamic createDB_func = _scope.GetVariable("createDB");
                    var result = createDB_func();

                    Console.WriteLine("결과:{0}", result);
                    //JsonConfig 파일 Write()
                    bResult = result;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("{0}", ex.ToString())
                  , string.Format("Menu : {0}", menuName));
                    Console.WriteLine(ex.ToString());
                }

            }
            else
            {
                MessageBox.Show(string.Format("File Not Found :\n{0}", pyfile)
                    , string.Format("Menu : {0}", menuName));
            }



            return bResult;
        }

        //환성설정에서 호출할 함수명 정의 한다.
        private bool PyCallSetFounctions(ScriptScope scope)
        {
            bool bresut = false;

#if false
            scope.SetVariable("ConnectFuncCallTCP", new Func<string, string>(ClassPython.ConnectFuncCallTCP));
            scope.SetVariable("ConnectFuncCallRs232c", new Func<string, string>(ClassPython.ConnectFuncCallRs232c));

            scope.SetVariable("ConnectFuncWebSocket", new Func<string, int, string>(ClassPython.ConnectFuncWebSocket));
#else

            scope.SetVariable("ConnectFuncCallUSB2CAN", new Func<string, string>(ClassPython.ConnectFuncCallUSB2CAN));
#endif


            return bresut;
        }

        static public string ConnectFuncCallUSB2CAN(string strCOM)
        {
            string strResult = "";
#if false
            ClassUSB2CAN usbCANST5520 = new ClassUSB2CAN();

            string comPort = strCOM;
            strResult = usbCANST5520.connect(comPort);
#else

#endif

            return strResult;
        }


    }
}
