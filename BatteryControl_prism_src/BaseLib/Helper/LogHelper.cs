using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Helper
{
	public class LogHelper
	{
		/// <summary>
		/// 디버그 로그 path
		/// </summary>
		public static readonly string PDEBUG_PATH = $@"{AppDomain.CurrentDomain.BaseDirectory}log";

		/// <summary>
		/// 사용불가 플래그
		/// </summary>
		public const bool IsDisabled = false;

		/// <summary>
		/// write log line
		/// </summary>
		/// <param name="path"></param>
		/// <param name="msg"></param>
		public static void WriteLine(string path, string msg)
		{
			if (IsDisabled) return;

			try
			{
				using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path, true))
				{
					var now = DateTime.Now;
					sw.WriteLine($"[{string.Format("{0:D4}.{1:D2}.{2:D2} {3:D2}:{4:D2}:{5:D2}.{6:D3}", now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, now.Millisecond)}] {msg}");
					sw.Flush();
					sw.Close();
					sw.Dispose();
				}
			}
			catch
			{

			}
		}

		/// <summary>
		/// write stream to hex log line
		/// </summary>
		/// <param name="path"></param>
		/// <param name="stream"></param>
		public static void WriteLine(string path, byte[] stream)
		{
			if (IsDisabled) return;

			StringBuilder sb = new StringBuilder();
			foreach (var b in stream)
			{
				sb.Append($"{string.Format("{0:X2}", b)} ");
			}
			WriteLine(path, sb.ToString());
		}

		/// <summary>
		/// write debug log
		/// </summary>
		/// <param name="msg"></param>
		public static void Debug(string ch, string msg)
		{
			var dt = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
			WriteLine($@"{PDEBUG_PATH}\Cycler\dbg_{ch}_{dt}.log", msg);
		}

        public static void GradeLog(string ch, string msg)
        {
            var dt = string.Format("{0:D4}{1:D2}{2:D2}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            WriteLine($@"{PDEBUG_PATH}\result_{ch}_{dt}.log", msg);
        }

        public static void BuildVersion(string msg)
        {
			   string basePath = $@"{AppDomain.CurrentDomain.BaseDirectory}";
			   string fileName = $@"{AppDomain.CurrentDomain.BaseDirectory}BuildDate.txt";

			   foreach (string file in Directory.EnumerateFiles(basePath, "BuildDate.txt", SearchOption.TopDirectoryOnly))
            {
				    System.IO.File.Delete(file);
			   }

				try
				{
					 using (System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true))
					 {
						 var now = DateTime.Now;
						 sw.WriteLine($"{msg}");
		 				 sw.Flush();
						 sw.Close();
						 sw.Dispose();
					 }
				}
				catch
				{
				}
		}
    }
}
