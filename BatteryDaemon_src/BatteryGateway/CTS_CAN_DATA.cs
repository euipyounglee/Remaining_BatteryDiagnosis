using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BatteryGateway
{
   
	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_CAN_DATA
	{
		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte canType;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte data_type;

		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 function_division;

		public CTS_CAN_VALUE canVal;
	}
}
