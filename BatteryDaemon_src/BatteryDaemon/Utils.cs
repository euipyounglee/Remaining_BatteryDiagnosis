using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;

namespace BatteryDaemon
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
    
    sealed class CmdArgumentException : Exception {
    public CmdArgumentException(string message) : base(message) {
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

        public static void Gaaa(string strProcessesByName)
        {

            // 이름이 notepad인 프로세스.(.exe 붙이면 안됨)
            Process P = Process.GetProcessesByName("notepad")[0];

            // icon 파라메타용.
            IntPtr[] phicon = new IntPtr[] { IntPtr.Zero };
            IntPtr[] piconid = new IntPtr[] { IntPtr.Zero };

            // 추출
            PrivateExtractIcons(new StringBuilder(P.MainModule.FileName), 0, 0, 0, phicon, piconid, 1, 0);

#if true
            // 추출된게 있다면
            if (phicon[0] != IntPtr.Zero)
            {
                // name.ico로 저장.
                Icon.FromHandle(phicon[0]).Save(new FileStream("name.ico", FileMode.Create));
                Console.WriteLine("name.ico is saved.");
            }
#else
            string strRoot = System.Environment.CurrentDirectory;
            string IconPath = string.Format("{0}\\{1}", strRoot, "Tray.ico");

            if (phicon[0] != IntPtr.Zero)
                //if (File.Exists(IconPath) == false)
            {
                using (System.IO.FileStream f = new System.IO.FileStream(IconPath, System.IO.FileMode.OpenOrCreate))
                {
                    myIcon.Save(f);
                    //_Icon.Save(f);
                    //  icon1.Save(f);
                }
            }
#endif


        }

    }
}
