using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BatteryGateway
{


	//const uint CTS_MAX_MAPPING_AUX = 512; //	//*!< Channel 당 최대 AUX 갯수 (온도(256) + 전압(256)) \n (Maximum number of AUX per channel) */
	//const uint CTS_MAX_MAPPING_CAN = 512;//'		/*!< Channel 당 최대 CAN 갯수 (Master(256) + Slave(256)) \n (Maximum number of CAN per channel)*/
	//const uint CTS_MAX_INSTALL_CH_COUNT = 8;//		/*!< Max Channel 개수 (Number of Max channel) */




	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_VARIABLE_CH_DATA
	{
		public CTS_CH_DATA chData;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst =512)] // CTS_MAX_MAPPING_AUX)]
		public CTS_AUX_DATA[] auxData;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]// CTS_MAX_MAPPING_CAN)]
		public CTS_CAN_DATA[] canData;
	}
}
