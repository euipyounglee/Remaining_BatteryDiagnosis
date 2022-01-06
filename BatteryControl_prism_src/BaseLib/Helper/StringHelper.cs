using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLib.Helper
{
	public class StringHelper
	{
		public static string Substring(string source, string value)
		{
			int offset = source.IndexOf(value);
			if (offset <= 0)
			{
				return source;
			}
			else
			{
				return source.Substring(0, offset);
			}
		}

		public static string ByteArrayToHexString(byte[] stream)
		{
			StringBuilder sb = new StringBuilder();
			foreach (var b in stream)
			{
				sb.Append(string.Format("{0:X2} ", b));
			}
			return sb.ToString();
		}

	}
}
