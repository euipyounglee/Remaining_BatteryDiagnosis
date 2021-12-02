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
using System.Diagnostics;




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

            var handle = User32Wrapper.GetConsoleWindow();

            menu = new ContextMenu();

            MenuItem subMenu = new MenuItem("menu");

            mnuExit = new MenuItem("Exit");
            mnuSetting = new MenuItem("Setting");//환경설정


            mnuVisable = new MenuItem("Visible");//환경설정

            try
            {
                subMenu.MenuItems.Add(mnuSetting);//서브 메뉴
                menu.MenuItems.Add(0, mnuExit);
                menu.MenuItems.Add(1, mnuVisable);
                menu.MenuItems.Add(1, new MenuItem("----"));
                menu.MenuItems.Add(2, subMenu);


                Icon _Icon = new Icon(SystemIcons.Application, 32, 32);

                Bitmap bitmap1 = Bitmap.FromHicon(SystemIcons.Hand.Handle);

                IntPtr Hicon = bitmap1.GetHicon();
                Icon myIcon = Icon.FromHandle(Hicon);
                string strRoot = System.Environment.CurrentDirectory;
                string IconPath = string.Format("{0}\\{1}",strRoot ,"Tray.ico");
                if (File.Exists(IconPath) == false)
                {
                    using (System.IO.FileStream f = new System.IO.FileStream(IconPath, System.IO.FileMode.OpenOrCreate))
                    {
                        _Icon.Save(f);
                    }
                }


                notificationIcon = new NotifyIcon();

#if false
                Icon = myIcon,// new System.Drawing.Icon("./Tray.ico"),
#else
                notificationIcon.Icon = new System.Drawing.Icon(IconPath);
#endif
                notificationIcon.ContextMenu = menu;
                notificationIcon.Text = "Main";

                mnuSetting.Click += new EventHandler(mnuSetting_Click);
                mnuExit.Click += new EventHandler(mnuExit_Click);

                mnuVisable.Click += new EventHandler(mnuVisable_Click);

                notificationIcon.Visible = true;

            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            //Application
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

          //  webSocketClient wb = new webSocketClient(sFromString);
           // wb.StartWS();

            return strReuslt;// "Test";
        }


        private void mnuSetting_Click(object sender, EventArgs e)
        {
            MenuSetting();
        }

        public bool MenuSetting() 
        {
#if false
            string menuName = "setting";
#else
            //  PythonClass py = new PythonClass();

            string strTitle = "Daemon";
            wSocketClient wsServ = wSocketClient.getInstance();

            string subTitle = string.Format("{0}-{1}:{2}{3}", strTitle, wsServ.getConnectIP(), wsServ.getConnectPort(), wsServ.getConnectPath());



            return callFileView(subTitle, "Settting");

#endif

        }

        public void connectSettingTest(int type)
        {
            var result = false;
            Console.WriteLine("결과:{0}", type);
        }


        public bool callFileView(string strTitle, string menuName)
        {
            PythonClass py = new PythonClass();

            string subTitle = strTitle;// "Daemon";

            return py.CallFileView(subTitle, "Settting");
        }

    }
   
}
