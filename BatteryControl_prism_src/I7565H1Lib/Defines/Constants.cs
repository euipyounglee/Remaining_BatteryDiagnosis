using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I7565H1Lib.Defines
{
	public class Constants
	{
		/// <summary>
		/// error messages
		/// </summary>
		private static string[] ErrMsg =
		{
			"No_Err",               "DEV_ModName_Err",          "DEV_ModNotExist_Err",
			"DEV_PortNotExist_Err", "DEV_PortInUse_Err",        "DEV_PortNotOpen_Err",
			"CAN_ConfigFail_Err",   "CAN_HARDWARE_Err",         "CAN_PortNo_Err",
			"CAN_FIDLength_Err",    "CAN_DevDisconnect_Err",    "CAN_TimeOut_Err",
			"CAN_ConfigCmd_Err",    "CAN_ConfigBusy_Err",       "CAN_RxBufEmpty",
			"CAN_TxBufFull",        "CAN_UserDefISRNo_Err" ,    "CAN_HWSendTimerNo_Err"
		};

		/// <summary>
		/// 오류메시지
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public static string GetErrorMessage(int code)
		{
			try
			{
				return ErrMsg[code];
			}
			catch
			{
				return "Undefined Error";
			}
		}

	}
}
