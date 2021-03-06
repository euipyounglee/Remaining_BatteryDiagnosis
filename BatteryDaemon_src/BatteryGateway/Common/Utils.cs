using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using static BatteryGateway.Program;
using System.Windows.Forms;

namespace BatteryGateway.Common
{

    //[System.Runtime.InteropServices.DllImport("user32.dll")]
    //public  static extern UInt32 PrivateExtractIcons(
    //StringBuilder lpszFile,
    //Int32 nIconIndex,
    //Int32 csIcon,
    //Int32 cyIcon,
    //IntPtr[] phicon,
    //IntPtr[] piconid,
    //UInt32 nIcons,
    //UInt32 flags
    //);

//     SW_SHOW

    sealed class CmdArgumentException : Exception {
        public CmdArgumentException(string message) : base(message) 
        {
        }
    }

  static class Utils {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern UInt32 PrivateExtractIcons(
            StringBuilder lpszFile,
            Int32 nIconIndex,
            Int32 csIcon,
            Int32 cyIcon,
            IntPtr[] phicon,
            IntPtr[] piconid,
            UInt32 nIcons,
            UInt32 flags
        );
        
        
        public static T GetOrDefault<K, T>(this IDictionary<K, T> dict, K key, T t = default(T)) {
      if (dict.TryGetValue(key, out T v)) {
        return v;
      }
      return t;
    }

    public static Dictionary<string, string[]> GetCommondLines(string[] args) {
      Dictionary<string, string[]> cmds = new Dictionary<string, string[]>();

      string key = "";
      List<string> values = new List<string>();

      foreach (var i in args) {
        if (i.StartsWith("-")) {
          if (!string.IsNullOrEmpty(key)) {
            cmds.Add(key, values.ToArray());
            key = "";
            values.Clear();
          }
          key = i;
        } else {
          values.Add(i);
        }
      }

      if (!string.IsNullOrEmpty(key)) {
        cmds.Add(key, values.ToArray());
      }
      return cmds;
    }

    public static string GetArgument(this Dictionary<string, string[]> args, string name, bool isOption = false) {
      string[] values = args.GetOrDefault(name);
      if (values == null || values.Length == 0) {
        if (isOption) {
          return null;
        }
        throw new CmdArgumentException(name + " is not found");
      }
      return values[0];
    }
        public static string getRoot()
        {
            string strRoot = System.Environment.CurrentDirectory;
            return strRoot;
        }

        public static Icon BitmapToIcon()
        {
            Icon _Icon = new Icon(SystemIcons.Application, 32, 32);

            // Bitmap bitmap1 = Bitmap.FromHicon(SystemIcons.Hand.Handle);
          //  Bitmap bitmap1 = Bitmap.FromHicon(SystemIcons.Hand.Handle);

          //  IntPtr Hicon = bitmap1.GetHicon();
         //   Icon myIcon = Icon.FromHandle(Hicon);
            string strRoot = System.Environment.CurrentDirectory;
            using (System.IO.FileStream f = new System.IO.FileStream(strRoot + "\\Tray1.ico", System.IO.FileMode.OpenOrCreate))
            {
                //myIcon.Save(f);
                _Icon.Save(f);
            }

            return _Icon;// myIcon;
        }

        public static Icon GetResourceIcon(string name, Size size) {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string[] resourceNameArray = assembly.GetManifestResourceNames();
            string search = string.Format("{0}", name);
            foreach (string resourceName in resourceNameArray) 
            { if (resourceName.EndsWith(search, StringComparison.CurrentCultureIgnoreCase))
                { using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                    {
                        return new Icon(stream, size);
                    }
                } 
            }
            return null; 
        }

        public static string rootPath()
        {

            int nReuslt = 0;
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //path = Path.Combine(path, IntPtr.Size == 8 ? "x64" : "x86");

            return path;
        }

        public static bool isWindowVisible(IntPtr hwnd)
        {
            return User32Wrapper.ShowWindow(hwnd, Program.SW_SHOW);
        }

        //나의 콘솔 핸들 얻기
        public static IntPtr getSafeHwnd()
        {
            IntPtr hWnd = User32Wrapper.GetConsoleWindow();

            return hWnd;

        }

        //실행 파일 위치
        public static string  GetAppPathName()
        {

           string  appPathName = Assembly.GetEntryAssembly().Location;// Application.ExecutablePath;

            return appPathName;
        }

    
    }
}
