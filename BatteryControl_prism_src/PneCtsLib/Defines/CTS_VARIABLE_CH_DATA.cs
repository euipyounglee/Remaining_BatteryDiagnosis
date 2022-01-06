using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Defines
{
	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_VARIABLE_CH_DATA
	{
		public CTS_CH_DATA chData;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.CTS_MAX_MAPPING_AUX)]
		public CTS_AUX_DATA[] auxData;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = Constants.CTS_MAX_MAPPING_CAN)]
		public CTS_CAN_DATA[] canData;
	}
}
