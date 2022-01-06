using SharedLib.Data.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SharedLib.Core
{
	public class XmlHelper
	{
		public const string LOCALREPOSITORY_FILENAME = "LocalConfig.xml";

		public static void Save(LocalConfigVM vm)
		{
			string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
			path += $@"\{LOCALREPOSITORY_FILENAME}";

			if (!File.Exists(path))
			{
				File.Create(path).Dispose();
			}

			using (StreamWriter wr = new StreamWriter(path))
			{
				XmlSerializer xs = new XmlSerializer(typeof(LocalConfigVM));
				xs.Serialize(wr, vm);

				wr.Flush();
				wr.Close();
				wr.Dispose();
			}
		}

		public static LocalConfigVM Load()
		{
			string path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
			path += $@"\{LOCALREPOSITORY_FILENAME}";

			if (!File.Exists(path))
			{
				var config = new LocalConfigVM();
				Save(config);
			}

			LocalConfigVM vm = null;
			using (var reader = new StreamReader(path))
			{
				XmlSerializer xs = new XmlSerializer(typeof(LocalConfigVM));
				vm = (LocalConfigVM)xs.Deserialize(reader);

				reader.Close();
				reader.Dispose();
			}
			return vm;
		}
	}
}
