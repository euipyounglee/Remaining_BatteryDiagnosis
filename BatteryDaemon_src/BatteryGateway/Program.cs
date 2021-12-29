using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
//using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BatteryGateway
{

    class Program
    {
        public  const int SW_HIDE = 0;
        public  const int SW_SHOW = 5;


        public class User32Wrapper
        {
            // GetMessage
            [DllImport(@"user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern bool GetMessage(ref MSG message, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

            [DllImport(@"user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern bool TranslateMessage(ref MSG message);

            [DllImport(@"user32.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
            public static extern long DispatchMessage(ref MSG message);

            [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
            public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

            [DllImport("Kernel32")]
            public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);


            [DllImport("kernel32.dll")]
            public static extern IntPtr GetConsoleWindow();


            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            [DllImport("user32")]
            public static extern int IsWindowVisible(IntPtr hwnd);


            [StructLayout(LayoutKind.Sequential)]
            public struct POINT
            {
                long x;
                long y;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MSG
            {
                IntPtr hwnd;
                public uint message;
                UIntPtr wParam;
                IntPtr lParam;
                uint time;
                POINT pt;
            }

        }


        public enum CtrlTypes
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }


        private const int WM_CUSTOM_EXIT = 0x0400 + 2000;
        private const int WM_CUSTOM_1 = 0x0400 + 2001;
        private const int WM_CUSTOM_2 = 0x0400 + 2002;
        private const int WM_CUSTOM_3 = 0x0400 + 2003;
        private const uint WM_CLOSE = 0x10;
        private const uint WM_DISTORY = 0x0002;
        private const uint WM_QUIT = 0x0012;


        private const uint SC_CLOSE = 0xf060; /// <summary> /// MF_ENABLED /// </summary>    
        private const uint MF_ENABLED = 0x00000000; /// <summary> /// MF_GRAYED /// </summary>        
        private const uint MF_GRAYED = 0x00000001;



        [DllImport("user32.dll")] 
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool revert);


        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hWnd, uint menuItemID, uint enabled);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);


        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        public static  CTrayIcon tray = null;

        static void Main(string[] args)
        {


            DllCureenSet();
            CJsonParser cjson =  CJsonParser.Instatce();

            User32Wrapper.MSG msg = new User32Wrapper.MSG();
            var handle = User32Wrapper.GetConsoleWindow();

            User32Wrapper.SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);
            //AppDomain.CurrentDomain.ProcessExit += CurrentDomain_ProcessExit;// 종료 이벤트


            User32Wrapper.ShowWindow(handle, SW_HIDE);
            if (args.Length  > 0)
            {
                if("-show" == args[0])
                {
                    User32Wrapper.ShowWindow(handle, SW_SHOW);
                }

            }
            else
            {
                bool bVisible = cjson.getConfigjsonBoolean("CreateConsle");

                int nCmdShow = bVisible ? SW_SHOW : SW_HIDE;
                User32Wrapper.ShowWindow(handle, nCmdShow);
            }

#if true
            tray = new CTrayIcon();
            tray.systemTry();


            //DashBoard에 서버 접속하기
            WebSockConnect();

#endif
            while (User32Wrapper.GetMessage(ref msg, IntPtr.Zero, 0, 0))
            {
                
                if(WM_DISTORY == msg.message || WM_QUIT ==  msg.message)
                {
                    handle = User32Wrapper.GetConsoleWindow();

                    User32Wrapper.ShowWindow(handle, SW_HIDE);

                    return;
                }

                User32Wrapper.TranslateMessage(ref msg);
                User32Wrapper.DispatchMessage(ref msg);
            }


        }

        static wSocketClient _ws=null;
        static webSocketSharpClient _wsShap = null;

        private static void WebSockConnect()
        {

            Console.WriteLine("WebSockConnect");
            _wsShap = webSocketSharpClient.getInstance();
            _wsShap.connect();
            
        }

        private static int jsonParsingPort(string key)
        {
            string currentPath = System.IO.Directory.GetCurrentDirectory();

            return jsonParsingPort(currentPath, key);
        }

        //포트 알아내기
        public static int jsonParsingPort(string jsonDir, string key)
        {
            int nPort = 80;

            CJsonParser cjson = CJsonParser.Instatce();// new CJsonParser();
            nPort = cjson.getConfigjsonInt32(key);

            return nPort;
        }

        public static dynamic  jsonParsingWebSocketValue( string key)
        {

            CJsonParser cjson = CJsonParser.Instatce(); //new CJsonParser();
            dynamic dobj = cjson.getObject(key);
            return dobj;
        }


        private static void CurrentDomain_ProcessExit1(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        static public void KillAppHide()
        {
            //종료 될때.. 
            Console.WriteLine("종료");

            var handle = User32Wrapper.GetConsoleWindow();

            User32Wrapper.ShowWindow(handle, SW_HIDE);

        }

        protected static void myHandler(object sender, ConsoleCancelEventArgs args)
        {
            KillAppHide();
        }

        static void consleVisible()
        {
            var handle = User32Wrapper.GetConsoleWindow();

            User32Wrapper.ShowWindow(handle, SW_HIDE);

        }
            
        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
        //    var handle = User32Wrapper.GetConsoleWindow();

          //  User32Wrapper.ShowWindow(handle, SW_HIDE);

            consleVisible();


            throw new NotImplementedException();
        }

        private static void SetCloseButtonEnabled(IntPtr windowHandle, bool enabled) 
        { 
            IntPtr systemMenuHandle = GetSystemMenu(windowHandle, false); 
            EnableMenuItem(systemMenuHandle, SC_CLOSE, (uint)(MF_ENABLED | (enabled ? MF_ENABLED : MF_GRAYED)));
        }

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            // Put your own handler here
            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:
                    Console.WriteLine("CTRL+C received!");
                    break;

                case CtrlTypes.CTRL_BREAK_EVENT:
                    Console.WriteLine("CTRL+BREAK received!");
                    break;

                case CtrlTypes.CTRL_CLOSE_EVENT:
                    Console.WriteLine("Program being closed!");
                    // consleVisible();

                    KillAppHide();

                    return true;// false;
                    //break;

                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:
                    Console.WriteLine("User is logging off!");
                    break;
            }

            return false;// true;
        }


            
        static int DllCureenSet()
        {

            int nReuslt = 0;
            var path = Utils.rootPath();

#if x64
            var pathTemp = Path.Combine(path, "x64");
#else
            var pathTemp = Path.Combine(path,  "x86");
#endif

            path = Path.Combine(path, IntPtr.Size == 8 ? "x64" : "x86");

            if (pathTemp != path)
            {
                path = pathTemp;
            }

            //DLL 읽기 지정하기
            string assemblyProbeDirectory = "";
            if (8 == IntPtr.Size)
            {
                assemblyProbeDirectory = string.Format("{0}", path);
            }
            else
            {
                assemblyProbeDirectory = string.Format("{0}", path);
            }

            if (Directory.Exists(assemblyProbeDirectory))
            {
                //현재 실행 위치를 세팅한다.
                Directory.SetCurrentDirectory(assemblyProbeDirectory);
                nReuslt = 1;
            }

            return nReuslt;
        }

    }


}
