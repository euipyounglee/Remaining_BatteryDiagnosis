using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BatteryGateway
{

	[StructLayout(LayoutKind.Sequential)]
	public struct CTS_CH_DATA
	{
		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chNo;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chState;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chStepType;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chMode;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chDataSelect;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chCode;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chStepNo;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chGradeCode;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lVoltage;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lCurrent;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lChargeAh;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lDisChargeAh;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lCapacitance;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lWatt;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lChargeWh;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lDisChargeWh;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 ulStepDay;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 ulStepTime;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 ulTotalDay;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 ulTotalTime;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lImpedance;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chReservedCmd;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chCommState;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chOutputState;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte chInputState;

		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 nAuxCount;

		[MarshalAs(UnmanagedType.I2, SizeConst = 1)]
		public Int16 nCanCount;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nTotalCycleNum;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nCurrentCycleNum;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nAccCycleGroupNum1;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nAccCycleGroupNum2;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nAccCycleGroupNum3;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nAccCycleGroupNum4;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nAccCycleGroupNum5;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nMultiCycleGroupNum1;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nMultiCycleGroupNum2;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nMultiCycleGroupNum3;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nMultiCycleGroupNum4;
		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 nMultiCycleGroupNum5;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lAvgVoltage;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lAvgCurrent;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lSaveSequence;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 ulCVDay;

		[MarshalAs(UnmanagedType.U4, SizeConst = 1)]
		public UInt32 ulCVTime;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public int[] lSyncTime;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lVoltage_Input;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lVoltage_Power;

		[MarshalAs(UnmanagedType.I4, SizeConst = 1)]
		public int lVoltage_Bus;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte cUsingChamber;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte cRecordTimeNo;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte cOutMuxUse;

		[MarshalAs(UnmanagedType.U1, SizeConst = 1)]
		public Byte cOutMuxBackup;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
		public int[] lreserved;
	}
}
