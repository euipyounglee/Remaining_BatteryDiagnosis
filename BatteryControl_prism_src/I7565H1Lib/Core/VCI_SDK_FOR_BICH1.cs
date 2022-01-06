using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace I7565H1Lib.Core
{
	public class VCI_SDK_FOR_BICH1
	{
		#region dllname

		private const string DLLNAME = "VCI_CAN_FOR_BICH1.dll";

		#endregion

		#region method

		[DllImport(DLLNAME)]
		public static extern int VCI_CloseCAN(byte DevPort);

		[DllImport(DLLNAME)]
		public static extern int VCI_Clr_BufOverflowLED(byte CAN_No);

		[DllImport(DLLNAME)]
		public static extern int VCI_Clr_RxMsgBuf(byte CAN_No);

		[DllImport(DLLNAME)]
		public static extern int VCI_Clr_UserDefISR(byte ISRNo);

		[DllImport(DLLNAME)]
		public static extern int VCI_DisableHWCyclicTxMsg();

		[DllImport(DLLNAME)]
		public static extern int VCI_DisableHWCyclicTxMsgNo(byte HW_TimerNo);

		[DllImport(DLLNAME)]
		public static extern void VCI_DoEvents();

		[DllImport(DLLNAME)]
		public static extern int VCI_EnableHWCyclicTxMsgNo(byte CAN_No, byte Mode, byte RTR, byte DLC, uint ID, byte[] Data, uint TimePeriod, uint TransmitTimes, byte HW_TimerNo);

		[DllImport(DLLNAME)]
		public static extern int VCI_EnableHWCyclicTxMsgNo_Ex(byte CAN_No, byte Mode, byte RTR, byte DLC, uint ID, byte[] Data, uint TimePeriod, uint TransmitTimes, byte HW_TimerNo, byte AddMode, uint DLAddVal, uint DHAddVal);

		[DllImport(DLLNAME)]
		public static extern int VCI_EnableHWCyclicTxMsg_NoStruct(byte CAN_No, byte Mode, byte RTR, byte DLC, uint ID, byte[] Data, uint TimePeriod, uint TransmitTimes);

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_CANBaud_BitTime(byte CAN_No, ref byte T1Val, ref byte T2Val, ref byte SJWVal);

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_CANFID_NoStruct(byte CAN_No, ref ushort SSFF_Num, ref ushort GSFF_Num, ref ushort SEFF_Num, ref ushort GEFF_Num, ushort[] SSFF_FID, uint[] GSFF_FID, uint[] SEFF_FID, uint[] GEFF_FID);

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_CANStatus_NoStruct(byte CAN_No, ref uint CurCANBaud, ref byte CANReg, ref byte CANTxErrCnt, ref byte CANRxErrCnt, ref byte MODState);

		[DllImport(DLLNAME)]
		public static extern ulong VCI_Get_DllVer();

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_ISRCANData(byte ISRNo, ref byte DLC, byte[] Data);

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_ISRCANData_Ex(byte ISRNo, ref uint ID, ref byte DLC, byte[] Data);

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_MODInfo_NoStruct(char[] Mod_ID, char[] FW_Ver, char[] HW_SN);

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_RxMsgBufIsFull(byte CAN_No, ref byte Flag);

		[DllImport(DLLNAME)]
		public static extern int VCI_Get_RxMsgCnt(byte CAN_No, ref uint RxMsgCnt);

		[DllImport(DLLNAME)]
		public static extern int VCI_OpenCAN_NoStruct(byte DevPort, byte DevType, uint CAN1_Baud, uint CAN2_Baud);

		[DllImport(DLLNAME)]
		public static extern int VCI_OpenCAN_NoStruct_Ex(byte DevPort, byte DevType, uint CAN1_Baud, uint CAN2_Baud, byte CAN1_T2Val, byte CAN2_T2Val);

		[DllImport(DLLNAME)]
		public static extern int VCI_RecvCANMsg_NoStruct(byte CAN_No, ref byte Mode, ref byte RTR, ref byte DLC, ref uint ID, ref uint TimeL, ref uint TimeH, byte[] Data);

		[DllImport(DLLNAME)]
		public static extern int VCI_Rst_MOD();

		[DllImport(DLLNAME)]
		public static extern int VCI_SendCANMsg_NoStruct(byte CAN_No, byte Mode, byte RTR, byte DLC, uint ID, byte[] Data);

		[DllImport(DLLNAME)]
		public static extern int VCI_Set_CANFID_AllPass(byte CAN_No);

		[DllImport(DLLNAME)]
		public static extern int VCI_Set_CANFID_NoStruct(byte CAN_No, ushort SSFF_Num, ushort GSFF_Num, ushort SEFF_Num, ushort GEFF_Num, ushort[] SSFF_FID, uint[] GSFF_FID, uint[] SEFF_FID, uint[] GEFF_FID);

		[DllImport(DLLNAME)]
		public static extern int VCI_Set_MOD_Ex(byte[] Data);

		[DllImport(DLLNAME)]
		public static extern int VCI_Set_UserDefISR(byte ISRNo, byte CAN_No, byte Mode, uint CANID, PFN_UserDefISR UserDefISR);

		public delegate void PFN_UserDefISR();

		#endregion

		#region variables

		public const int No_Err = 0;
		public const int DLC_7 = 7;
		public const int DLC_8 = 8;
		public const int ADDITION_MODE = 1;
		public const int MULTIPLE_MODE = 2;
		public const int ISRNO_0 = 0;
		public const int ISRNO_1 = 1;
		public const int ISRNO_2 = 2;
		public const int ISRNO_3 = 3;
		public const int ISRNO_4 = 4;
		public const int ISRNO_5 = 5;
		public const int ISRNO_6 = 6;
		public const int ISRNO_7 = 7;
		public const int ISR_CANMODE_ALL = 2;
		public const int ISR_CANID_ALL = 0;
		public const int CANBaud_5K = 5000;
		public const int CANBaud_10K = 10000;
		public const int CANBaud_20K = 20000;
		public const int CANBaud_40K = 40000;
		public const int CANBaud_50K = 50000;
		public const int CANBaud_80K = 80000;
		public const int CANBaud_100K = 100000;
		public const int CANBaud_125K = 125000;
		public const int CANBaud_200K = 200000;
		public const int CANBaud_250K = 250000;
		public const int CANBaud_400K = 400000;
		public const int CANBaud_500K = 500000;
		public const int CANBaud_600K = 600000;
		public const int CANBaud_800K = 800000;
		public const int CANBaud_1000K = 1000000;
		public const int DLC_6 = 6;
		public const int DLC_5 = 5;
		public const int ISR_CANPORT_ALL = 0;
		public const int DLC_3 = 3;
		public const int DEV_ModName_Err = 1;
		public const int DLC_4 = 4;
		public const int DEV_PortNotExist_Err = 3;
		public const int DEV_PortInUse_Err = 4;
		public const int DEV_PortNotOpen_Err = 5;
		public const int CAN_ConfigFail_Err = 6;
		public const int CAN_HARDWARE_Err = 7;
		public const int CAN_PortNo_Err = 8;
		public const int CAN_FIDLength_Err = 9;
		public const int CAN_DevDisconnect_Err = 10;
		public const int CAN_TimeOut_Err = 11;
		public const int CAN_ConfigCmd_Err = 12;
		public const int CAN_ConfigBusy_Err = 13;
		public const int CAN_RxBufEmpty = 14;
		public const int CAN_TxBufFull = 15;
		public const int DEV_ModNotExist_Err = 2;
		public const int CAN_HWSendTimerNo_Err = 17;
		public const int DLC_2 = 2;
		public const int DLC_1 = 1;
		public const int DLC_0 = 0;
		public const int RTR_1 = 1;
		public const int CAN_UserDefISRNo_Err = 16;
		public const int MODE_29BIT = 1;
		public const int MODE_11BIT = 0;
		public const int RTR_0 = 0;
		public const int I7565H1 = 1;
		public const int COMM_MODE = 1;
		public const int CONFIG_MODE = 0;
		public const int CAN2 = 2;
		public const int CAN1 = 1;
		public const int I7565H2 = 2;

		#endregion
	}
}
