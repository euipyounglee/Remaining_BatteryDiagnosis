using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BatteryDaemon
{
    class CTryIcon 
    {
        // SystemTray;


        //public static ContextMenu menu;
        //public static MenuItem mnuExit;
        //public static NotifyIcon notificationIcon;

        //static NotifyIcon notifyIcon = new NotifyIcon();
        //static bool Visible = true;

        static NotifyIcon notifyIcon;

        private void systemTry()
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
                        Icon = Properties.Resources.Services,
                        ContextMenu = menu,
                        Text = "Main"
                    };
                    mnuExit.Click += new EventHandler(mnuExit_Click);

                    notificationIcon.Visible = true;
                    System.Net.Mime.MediaTypeNames.Application.Run();
                }
            );

            notifyThread.Start();
#else

            //NotifyIcon icon = new  System.Windows.Forms.NotifyIcon();
            //icon.Icon = new System.Drawing.Icon("./cat.ico");
            //icon.Visible = true;
            //icon.BalloonTipText = "Hello from My Kitten";
            //icon.BalloonTipTitle = "Cat Talk";
            //icon.BalloonTipIcon = ToolTipIcon.Info;
            //icon.ShowBalloonTip(2000);



            //Application
#endif
        }


        static void mnuExit_Click(object sender, EventArgs e)
        {
          //  notificationIcon.Dispose();
          //  Application.Exit();
        }

    }
   
}
