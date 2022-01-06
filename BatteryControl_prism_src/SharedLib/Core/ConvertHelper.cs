using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Core
{
	public class ConvertHelper
	{
		public static byte[] HexStringToBytes(string hexValues)
		{
			string[] hexValuesSplit = hexValues.Split(' ');
			byte[] stream = new byte[hexValuesSplit.Length];

			for (int i=0; i<hexValuesSplit.Length; ++i)
			{
				// Convert the number expressed in base-16 to an integer.
				int value = Convert.ToInt32(hexValuesSplit[i], 16);
				stream[i] = (byte)value;
			}

			return stream;
		}
	}
}
