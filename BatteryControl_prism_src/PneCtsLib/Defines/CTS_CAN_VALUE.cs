using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	[StructLayout(LayoutKind.Explicit)]
	public struct CTS_CAN_VALUE
	{
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public int[] lVal;

		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public float[] fVal;

		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
		public SByte[] strVal;
	}
}
