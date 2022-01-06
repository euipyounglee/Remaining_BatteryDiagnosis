using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Helper
{
	public class HashHelper
	{
		/// <summary>
		/// to sha512
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string Sha512(string value)
		{
			using (SHA512 shaM = new SHA512Managed())
			{
				var hash = shaM.ComputeHash(System.Text.Encoding.Default.GetBytes(value));
				return Convert.ToBase64String(hash);
			}
		}
	}
}
