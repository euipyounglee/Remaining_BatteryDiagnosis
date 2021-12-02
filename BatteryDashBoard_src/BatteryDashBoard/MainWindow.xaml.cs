using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Net;
//using System.Net.Sockets;

using System.Diagnostics;
using System.Security.Permissions;
using System.Threading;
using System.Data.SQLite;
using System.Net.Sockets;
//using System.Diagnostics;



namespace BatteryDashBoard
{

    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)] //자바스크립트와 연동
    
    public class HtmlInteropClass
    {

        public void SetAValue(string title)
        {

            //외부 함수 전용 공간을 만들어서 그쪽으로 옮겨서 처리한다.

          //  var ef = new ExternalFunction.EF_1_1();

           // ef.SetAValueEF(title);

        }

    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        //  public  webServer _wServer = null;

        //private static webServer instance = null;// new webServer();
        private static ConnectionManager _connect = null;

        //_connect

        static int _wport=0;
        Server _server = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public string MyIP()
        {
            string localIP = "Not available, please check your network seetings!";

            //IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());

            //foreach (IPAddress ip in host.AddressList)
            //{

            //    if (ip.AddressFamily == AddressFamily.InterNetwork)
            //    {

            //        localIP = ip.ToString();
            //        Console.WriteLine(localIP);
            //        break;
            //    }

            //}
            return localIP;
        }




           
        
        public static ConnectionManager getConnect_Instance()
        {
            if(null == _connect)
            {
              
                _connect = ConnectionManager.getInstance();
            }

            return _connect;
        }



        public static string LocalMyIP()
        {
            IPHostEntry host1 = Dns.GetHostByName(Dns.GetHostName());
            string myip = host1.AddressList[0].ToString();

            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    Console.WriteLine("IP Address = " + ip.ToString());
                   // netMask:= ipnet.IP.DefaultMask()
                       

                    // ip.MapToIPv4.netMak

                }
            }


            return myip;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _Loaded();
#if false
            //  battery_DiagnosisServer(true);//이미실행되어 있으면 종료 시킴(중복방지)
            ClassDaemon daemon = new ClassDaemon();//.ProcessProcessRun();
            daemon.ProcessProcessRun();
#endif
            DbConnect();
        }

        private void _Loaded()
        {

            _wport = 0;// wport;
            //=========================================================================
            //    webServer.getInstance().ThreadStart(wport);
            //=========================================================================
            getConnect_Instance();

        }


        public string sendData(string strData)
        {
            string strResult = "100";
            if (null != _connect)
            {
                try
                {
                    if (getConnect_Instance()._webSocketController.SendData(strData))  {
                        strResult = "200";
                    }
                }
                catch (System.NullReferenceException ex1)
                {
                    MessageBox.Show(ex1.Message +"\nor Daemon(Client) 연결 되었는지 다시 확인 필요합니다.","error");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message ,"error");
                }
            }

            return strResult;// false;// False;
        }


        private bool webUrlCheck(string webSite) {
            bool bResut = true;
            //HttpWebRequest request = null;
            //HttpWebResponse response = null;
            //string url = webSite;

            //try  {
            //    request = WebRequest.Create(url) as HttpWebRequest;
            //    request.Method = "HEAD";
            //    response = request.GetResponse() as HttpWebResponse;
            //    response.Close();
            //    request.Abort();

            //} catch  {
            //    Console.WriteLine(url);
            //    bResut = false;
            //    if(null != response)   response.Close();
            //}

            return bResut;
        }

        public void InvokeMe(object msg)
        {
            //     MessageBox.Show(msg.ToString(), "테스트", MessageBoxButtons.OK, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign);
            MessageBox.Show(msg + "\nOK","C# 제목");
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            
           // myBrowser.InvokeScript("show");
        }

        private void GoForward_Click(object sender, RoutedEventArgs e)
        {
            
         //   myBrowser.InvokeScript("hide");
        }
        private void JsCall()
        {
          //  myBrowser.NavigateToString("<html><script>function callMe() {alert('Hello');} document.myfunc = callMe;</script><body>Hello World</body></html>");
          //  myBrowser.LoadCompleted += (s, e) => myBrowser.InvokeScript("callMe");


        }

        public void LoadJson(string jsonDir)
        {
            int nPort = 0;// jObject["port"];
#if false
            string strJson = String.Format("{0}\\{1}", jsonDir, "config.json");

            using (StreamReader r = new StreamReader(strJson))// "file.json"))
            {
                string json = r.ReadToEnd();
               

                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    if("port" ==item.Name)
                    {
                        Console.WriteLine("{0}", item.Value);
                        nPort = item.Value;
                        break;
                    }
                    //Console.WriteLine("{0} {1}", item.temp, item.vcc);
                }

            }
#else
            //Server _server = new Server();
            //int wport = jsonParsingPort("wport");
            //_server.serverStart("127.0.0.1",2222);
#endif
        }

        //포트 알아내기
        private string jsonParsingString( string key)
        {
            string strValue = "";// 80;// jObject["port"];
            string currentPath = System.IO.Directory.GetCurrentDirectory();
            string configDirpath = String.Format("{0}", currentPath);

            CJsonParser cjson = new CJsonParser();
            strValue = cjson.jsonParsing(configDirpath, key);


            return strValue;
        }

        private int jsonParsingPort( string key)
        {
            string currentPath = System.IO.Directory.GetCurrentDirectory();

            return jsonParsingPort(currentPath, key);
        }


        public static dynamic jsonParsingWebSocketValue(string key)
        {

            CJsonParser cjson = new CJsonParser();
            dynamic dobj = cjson.getObject(key);
            return dobj;
        }


        //포트 알아내기
        public int jsonParsingPort(string jsonDir ,string key)
        {
            int nPort = 80;// jObject["port"];

           CJsonParser cjson = new CJsonParser();
           nPort =  cjson.getConfigjsonInt32(key);

            return nPort;
        }


        private bool ProcessFindAndKill(string webServerName)
        {
            bool bResult = false;

            Process[] processList = Process.GetProcessesByName(webServerName);


            foreach (var process in Process.GetProcesses())
            {
                if ( process.ProcessName == webServerName)
                {
                    process.Kill();
                    bResult = true;
                }
                
            }


            return bResult;
        }



        private bool killProcess(string webServerName)
        {
            bool bResult = false;
            Process[] processList = Process.GetProcessesByName(webServerName);// "notepad");


            foreach (var process in Process.GetProcesses())
            {

                if(webServerName == process.ProcessName)
                {
                    process.Kill();
                    bResult = true;
                }

                Console.WriteLine("{0}-{1}", process.Id , process.ProcessName);


            }

            return bResult;
        }

        private static void WriteProcessInfo(Process processInfo)
        {

            Console.WriteLine("Process : {0}", processInfo.ProcessName);
            Console.WriteLine("시작시간 : {0}", processInfo.StartTime);
            Console.WriteLine("프로세스 PID : {0}", processInfo.Id);

        //    Console.WriteLine("메모리 : {0}", processInfo.VirtualMemorySize);

        }


        //void ExecuteCommand(string command)
        //{
        //    int exitCode;

        //    ProcessStartInfo processInfo;
        //    Process process;

        //    string arguments = "";
        //    string fileName = command; //실행파일- 서버 동작
        //    if (File.Exists(fileName) == true) 
        //    {

        //        processInfo = new ProcessStartInfo(fileName, arguments);
        //        string strConsle = jsonParsingString("CreateConsle");
        //        bool bNoneConsle = true;// false;// true;
        //        if ("True" == strConsle)
        //            bNoneConsle = false; //윈도우 콘솔창 여부 : true(띄우지 않는다)

        //        processInfo.CreateNoWindow = bNoneConsle;// true;
        //        processInfo.UseShellExecute = false;
        //        // *** Redirect the output ***
        //        processInfo.RedirectStandardError = true;
        //        processInfo.RedirectStandardOutput = true;


        //        string output = "";
        //        string error = "";
        //        process = Process.Start(processInfo);
        //        process.WaitForExit();

        //        // *** Read the streams ***

        //        exitCode = process.ExitCode;
        //        Console.WriteLine("output>>" + (String.IsNullOrEmpty(output) ? "(none)" : output));
        //        Console.WriteLine("error>>" + (String.IsNullOrEmpty(error) ? "(none)" : error));
        //        Console.WriteLine("ExitCode: " + exitCode.ToString(), "ExecuteCommand");
        //        process.Close();
        //    }

        //}

        //private string GetconfigProcess()
        //{
        //    string currentPath = System.IO.Directory.GetCurrentDirectory();
        //    //string configDirpath = String.Format("{0}\\{1}", currentPath, "httpServer");
        //    string configDirpath = String.Format("{0}", currentPath);//, "httpServer");

        //    string webServerName = jsonParsingString("daemon");

        //    string FileName = String.Format("{0}{1}", configDirpath , ("" == webServerName ? webServerName : "\\"+ webServerName));
        //    return FileName;
        //}

        private bool processRun()
        {
           bool bResult = true;
            // 프로세스 파일명 정의
            //파이썬 exe를 직접 실행해서 파이썬 코드가 실행되도록 한다.
           var psi = new ProcessStartInfo();

           string currentPath = System.IO.Directory.GetCurrentDirectory();
           string configDirpath = String.Format("{0}\\{1}", currentPath, "httpServer");

        string webServerName = "";// jsonParsingString("webserver");

            string  FileName = configDirpath + "\\" + webServerName;
           if (!File.Exists(FileName)) return false;

            psi.FileName = "cmd.exe";

            string configFile = FileName;

            psi.Arguments = configFile;

           //3) Proecss configuration
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            //4) return value def
            var erros = "";
            var results = "";

#if true
            using (var process = System.Diagnostics.Process.Start(psi))
            {
                erros = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }

            Console.WriteLine(erros);
            Console.WriteLine(results);
#else
            var process = System.Diagnostics.Process.Start(psi);
#endif

            return bResult;
        }

        // 화면 갱신
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //refreshButton(myBrowser);
        //}

        //private const int INTERNET_OPTION_END_BROWSER_SESSION = 42;

        //[System.Runtime.InteropServices.DllImport("wininet.dll", SetLastError = true)]
        //private static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr IpBuffer, int IpdwBufferLength);

        //public void Init_Browser()
        //{
        //    //InternetSetOption(IntPtr.Zero, INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0);
        //}

        //private void refreshButton(WebBrowser wb)
        //{

        //_Loaded();
        //Init_Browser();
        //wb.Refresh();
        //}

        ////외부프로그램 실행 시키기
        //private void ProcessProcessRun()
        //{
        //    Thread myThread = new Thread(Func);
        //    string commandName = GetconfigProcess();
        //    myThread.Start(commandName);

        //}

        //private void Func(object obj)
        //{
        //   string commandName = (string) obj;
        //    ClassDaemon daemon = new ClassDaemon();

        //   daemon.ExecuteCommand(commandName);
        //}

        //private bool battery_DiagnosisServer(bool bKill)
        //{
        //    bool bResult = false;
        //string webServerName = jsonParsingString("daemon");
        //string ServerName = "BatteryDaemon";

        //if ("" != webServerName)
        //{
        //    ServerName = System.IO.Path.GetFileNameWithoutExtension(webServerName);
        //}
        //if (bKill)
        //{
        //    bResult = ProcessFindAndKill(ServerName);
        //}

        //    return bResult;
        //}

        private void Window_Closed(object sender, EventArgs e)
        {
            //  MessageBox.Show("종료합니다.");
           // battery_DiagnosisServer(true);
        }

        private void DbConnect()
        {
            string currentPath = System.IO.Directory.GetCurrentDirectory();

            ClassSqlite db = new ClassSqlite();
            db.CreateDataSqlite(currentPath);
        }

        //private void Window_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if(e.Key == Key.F5)
        //    {
        //     //   refreshButton(myBrowser);
        //    }
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    CMailMessage mail = new CMailMessage();
        //    mail.mailSend();
        //}

    }
}
