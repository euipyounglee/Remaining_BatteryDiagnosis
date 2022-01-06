using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_AUX_DATA_D
	{
		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 chNo;

		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 chType;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public Int32 lValue;
	}
}
