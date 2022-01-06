using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	[StructLayout(LayoutKind.Sequential)]
	public struct DEBUG_TEST
	{
		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int nTest;

		[MarshalAs(UnmanagedType.R4, SizeConst = 1)]
		public float fTest;
	}
}
