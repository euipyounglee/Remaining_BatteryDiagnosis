using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System.IO;
using static BatteryDaemon.Program;






namespace BatteryDaemon
{
    class CTrayIcon 
    {

        public static ContextMenu menu;
        public static MenuItem mnuExit;
        public static NotifyIcon notificationIcon;

        public static MenuItem mnuSetting;
        public static MenuItem mnuVisable;

        private const uint WM_CLOSE = 0x10;



        public  void systemTry()
        {
#if false
            Thread notifyThread = new Thread(
                delegate ()
                {
                    menu = new ContextMenu();
                    mnuExit = new MenuItem("Exit");
                    menu.MenuItems.Add(0, mnuExit);

                    notificationIcon = new NotifyIcon()
                    {
                        //Icon = Properties.Resources.Services,
                        Icon = new System.Drawing.Icon("./Tray.ico"),
                        ContextMenu = menu,
                        Text = "Main"
                    };
                    mnuExit.Click += new EventHandler(mnuExit_Click);

                    notificationIcon.Visible = true;
                }
            );

            notifyThread.Start();
#else

            var handle = User32Wrapper.GetConsoleWindow();

            menu = new ContextMenu();

            MenuItem subMenu = new MenuItem("menu");

            mnuExit = new MenuItem("Exit");
            mnuSetting = new MenuItem("Setting");//환경설정


#if false
            if (Program.IsWindowVisible(handle))
            {
                mnuVisable = new MenuItem("Visible");//환경설정
            }
            else
            {
                mnuVisable = new MenuItem("Show");//환경설정
            }
#else

            mnuVisable = new MenuItem("Visible");//환경설정
#endif


            subMenu.MenuItems.Add(mnuSetting);//서브 메뉴

            menu.MenuItems.Add(0, mnuExit);
            menu.MenuItems.Add(1, mnuVisable);

            menu.MenuItems.Add(1, new MenuItem("----") );
            //menu.MenuItems.Add(new ToolStripSeparator.ToolStripItemAccessibleObject());// ToolStripSeparator());

            menu.MenuItems.Add(2, subMenu);

            notificationIcon = new NotifyIcon()
            {
                //Icon = Properties.Resources.Services,
                Icon = new System.Drawing.Icon("./Tray.ico"),
                ContextMenu = menu,
                Text = "Main"
            };

            mnuSetting.Click += new EventHandler(mnuSetting_Click);
            mnuExit.Click += new EventHandler(mnuExit_Click);


            mnuVisable.Click += new EventHandler(mnuVisable_Click);

            notificationIcon.Visible = true;



            //Application
#endif
        }


        static void mnuVisable_Click(object sender, EventArgs e)
        {
            var handle = User32Wrapper.GetConsoleWindow();
            if (Program.IsWindowVisible(handle))
                User32Wrapper.ShowWindow(handle,0);
            else
                User32Wrapper.ShowWindow(handle,5);
        }

        static void mnuExit_Click(object sender, EventArgs e)
        {
            notificationIcon.Dispose();
            Application.Exit();
            var handle = User32Wrapper.GetConsoleWindow();
            User32Wrapper.SendMessage(handle, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        static public string webServerConnect(string sFromString)
        {

            string strReuslt = "Test";

            Console.WriteLine("call args{0}", sFromString);

            webSocketClient wb = new webSocketClient(sFromString);
            wb.StartWS();

            return strReuslt;// "Test";
        }


        private void mnuSetting_Click(object sender, EventArgs e)
        {
            MenuSetting();
        }

        public bool MenuSetting() 
        { 
            string menuName = "setting";
            //1. Iron Python WPF 화면 띄우기
            Console.WriteLine(menuName);// "Settting");

            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();
            var pyfile = "";

            CJsonParser cjson = new CJsonParser();

           string strRoot =  System.Environment.CurrentDirectory;
            string strSettingMenu = cjson.jsonMenuParsing(strRoot, menuName);
           if("" == strSettingMenu)
            {
                strSettingMenu = "settingform.py";
            }

            pyfile = string.Format("{0}\\{1}", strRoot, strSettingMenu);

            if (File.Exists(pyfile))
            {
                // 함수 전달하기
                scope.SetVariable("webServerConnect", new Func<string, string> (CTrayIcon.webServerConnect));

                engine.ExecuteFile(pyfile, scope);
                //화면이 닫이면(파이썬 화면이종료) 함수 호출이 된다.
                dynamic Apply_func = scope.GetVariable("Apply_func");
                var var1 = 2;
                var var2 = 2;
                var result = Apply_func(var1, var2);

                Console.WriteLine("결과:{0}", result);

            }
            else
            {
                MessageBox.Show(string.Format("File Not Found :\n{0}", pyfile)
                    , string.Format("Menu : {0}",menuName));
            }


            return true;

        }


    }
   
}
