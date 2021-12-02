using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatteryDashBoard
{
    public class ClassDaemon
    {

        //외부프로그램 실행 시키기
        public  void ProcessProcessRun()
        {
            Thread myThread = new Thread(Func);
            string commandName = GetconfigProcess();
            myThread.Start(commandName);

        }

        private void Func(object obj)
        {
            string commandName = (string)obj;
            ClassDaemon daemon = new ClassDaemon();

            daemon.ExecuteCommand(commandName);
        }

        void ExecuteCommand(string command)
        {
            int exitCode;

            ProcessStartInfo processInfo;
            Process process;

            string arguments = "";
            string fileName = command; //실행파일- 서버 동작
            if (File.Exists(fileName) == true)
            {

                processInfo = new ProcessStartInfo(fileName, arguments);
                string strConsle = jsonParsingString("CreateConsle");
                bool bNoneConsle = true;// false;// true;
                if ("True" == strConsle)
                    bNoneConsle = false; //윈도우 콘솔창 여부 : true(띄우지 않는다)

                processInfo.CreateNoWindow = bNoneConsle;// true;
                processInfo.UseShellExecute = false;
                // *** Redirect the output ***
                processInfo.RedirectStandardError = true;
                processInfo.RedirectStandardOutput = true;


                string output = "";
                string error = "";
                process = Process.Start(processInfo);
                process.WaitForExit();

                // *** Read the streams ***

                exitCode = process.ExitCode;
                Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
                Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
                Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
                process.Close();
            }

        }

        private string GetconfigProcess()
        {
            string currentPath = System.IO.Directory.GetCurrentDirectory();
            //string configDirpath = String.Format("{0}\\{1}", currentPath, "httpServer");
            string configDirpath = String.Format("{0}", currentPath);//, "httpServer");

            string webServerName = jsonParsingString("daemon");

            string FileName = String.Format("{0}{1}", configDirpath, ("" == webServerName ? webServerName : "\\" + webServerName));
            return FileName;
        }

        //포트 알아내기
        private string jsonParsingString(string key)
        {
            string strValue = "";// 80;// jObject["port"];
            string currentPath = System.IO.Directory.GetCurrentDirectory();
            string configDirpath = String.Format("{0}", currentPath);

            CJsonParser cjson = new CJsonParser();
            strValue = cjson.jsonParsing(configDirpath, key);


            return strValue;
        }

        private int jsonParsingPort(string key)
        {
            string currentPath = System.IO.Directory.GetCurrentDirectory();

            return jsonParsingPort(currentPath, key);
        }

        //포트 알아내기
        private int jsonParsingPort(string jsonDir, string key)
        {
            int nPort = 80;// jObject["port"];

            CJsonParser cjson = new CJsonParser();
            nPort = cjson.getConfigjsonInt32(key);

            return nPort;
        }


    }
}
