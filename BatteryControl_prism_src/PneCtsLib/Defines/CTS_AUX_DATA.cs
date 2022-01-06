using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_AUX_DATA
	{
		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 auxChNo;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte auxChType;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte auxTempTableType;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lValue;
	}
}
