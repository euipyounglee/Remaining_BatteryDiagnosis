using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BatteryGateway
{
	// @brief Aux Data 구조체 (Module에 setting 하는 값이며 Max 512개) 
	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_AUX_DATA
	{
		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 chNo;

		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 chType;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public Int32 lValue;
	}
}
