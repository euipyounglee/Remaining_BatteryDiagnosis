using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Core
{
	[StructLayout(LayoutKind.Explicit)]
	struct WORDConverter
	{
		[FieldOffset(0)]
		public uint Value;

		[FieldOffset(0)]
		public ushort LOWORD;

		[FieldOffset(2)]
		public ushort HIWORD;
	}

	public class ExtensionMethods
	{
		/// <summary>
		/// deep copy
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static T DeepCopy<T>(T obj)
		{
			BinaryFormatter s = new BinaryFormatter();
			using (MemoryStream ms = new MemoryStream())
			{
				s.Serialize(ms, obj);
				ms.Position = 0;
				T t = (T)s.Deserialize(ms);

				return t;
			}
		}
	}
	
}
