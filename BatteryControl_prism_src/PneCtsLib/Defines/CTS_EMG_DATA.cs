using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_EMG_DATA
	{
		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 Code;

		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 Value;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int reserved;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
		public byte[] szName;
	}
}
