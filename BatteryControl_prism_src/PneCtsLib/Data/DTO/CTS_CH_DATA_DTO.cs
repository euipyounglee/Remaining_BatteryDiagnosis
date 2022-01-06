using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_CH_DATA_DTO
	{
		#region property

		public byte ChNo { get; set; }

		public byte ChState { get; set; }

		public byte ChStepType { get; set; }

		public byte ChMode { get; set; }

		public byte ChDataSelect { get; set; }

		public byte ChCode { get; set; }

		public byte ChStepNo { get; set; }

		public byte ChGradeCode { get; set; }

		public int Voltage { get; set; }

		public int Current { get; set; }

		public int ChargeAh { get; set; }

		public int DisChargeAh { get; set; }

		public int Capacitance { get; set; }

		public int Watt { get; set; }

		public int ChargeWh { get; set; }

		public int DisChargeWh { get; set; }

		public uint StepDay { get; set; }

		public uint StepTime { get; set; }

		public uint TotalDay { get; set; }

		public uint TotalTime { get; set; }

		public int Impedance { get; set; }

		public byte ChReservedCmd { get; set; }

		public byte ChCommState { get; set; }

		public byte ChOutputState { get; set; }

		public byte ChInputState { get; set; }

		public short AuxCount { get; set; }

		public short CanCount { get; set; }

		public uint TotalCycleNum { get; set; }

		public uint CurrentCycleNum { get; set; }

		public uint AccCycleGroupNum1 { get; set; }

		public uint AccCycleGroupNum2 { get; set; }

		public uint AccCycleGroupNum3 { get; set; }

		public uint AccCycleGroupNum4 { get; set; }

		public uint AccCycleGroupNum5 { get; set; }

		public uint MultiCycleGroupNum1 { get; set; }

		public uint MultiCycleGroupNum2 { get; set; }

		public uint MultiCycleGroupNum3 { get; set; }

		public uint MultiCycleGroupNum4 { get; set; }

		public uint MultiCycleGroupNum5 { get; set; }

		public int AvgVoltage { get; set; }

		public int AvgCurrent { get; set; }

		public int SaveSequence { get; set; }

		public uint CVDay { get; set; }

		public uint CVTime { get; set; }

		public int[] SyncTime { get; set; }

		public int VoltageInput { get; set; }

		public int VoltagePower { get; set; }

		public int VoltageBus { get; set; }

		public byte UsingChamber { get; set; }

		public byte RecordTimeNo { get; set; }

		public byte OutMuxUse { get; set; }

		public byte OutMuxBackup { get; set; }

		public int[] Reserved { get; set; }

		#endregion

		#region method

		public CTS_CH_DATA_DTO(CTS_CH_DATA src)
		{
			ChNo = src.chNo;
			ChState = src.chState;
			ChStepType = src.chStepType;
			ChMode = src.chMode;
			ChDataSelect = src.chDataSelect;
			ChCode = src.chCode;
			ChStepNo = src.chStepNo;
			ChGradeCode = src.chGradeCode;
			Voltage = src.lVoltage;
			Current = src.lCurrent;
			ChargeAh = src.lChargeAh;
			DisChargeAh = src.lDisChargeAh;
			Capacitance = src.lCapacitance;
			Watt = src.lWatt;
			ChargeWh = src.lChargeWh;
			DisChargeWh = src.lDisChargeWh;
			StepDay = src.ulStepDay;
			StepTime = src.ulStepTime;
			TotalDay = src.ulTotalDay;
			TotalTime = src.ulTotalTime;
			Impedance = src.lImpedance;
			ChReservedCmd = src.chReservedCmd;
			ChCommState = src.chCommState;
			ChOutputState = src.chOutputState;
			ChInputState = src.chInputState;
			AuxCount = src.nAuxCount;
			CanCount = src.nCanCount;
			TotalCycleNum = src.nTotalCycleNum;
			CurrentCycleNum = src.nCurrentCycleNum;
			AccCycleGroupNum1 = src.nAccCycleGroupNum1;
			AccCycleGroupNum2 = src.nAccCycleGroupNum2;
			AccCycleGroupNum3 = src.nAccCycleGroupNum3;
			AccCycleGroupNum4 = src.nAccCycleGroupNum4;
			AccCycleGroupNum5 = src.nAccCycleGroupNum5;
			MultiCycleGroupNum1 = src.nMultiCycleGroupNum1;
			MultiCycleGroupNum2 = src.nMultiCycleGroupNum2;
			MultiCycleGroupNum3 = src.nMultiCycleGroupNum3;
			MultiCycleGroupNum4 = src.nMultiCycleGroupNum4;
			MultiCycleGroupNum5 = src.nMultiCycleGroupNum5;
			AvgVoltage = src.lAvgVoltage;
			AvgCurrent = src.lAvgCurrent;
			SaveSequence = src.lSaveSequence;
			CVDay = src.ulCVDay;
			CVTime = src.ulCVTime;

			SyncTime = new int[src.lSyncTime.Length];
			Array.Copy(src.lSyncTime, 0, SyncTime, 0, src.lSyncTime.Length);

			VoltageInput = src.lVoltage_Input;
			VoltagePower = src.lVoltage_Power;
			VoltageBus = src.lVoltage_Bus;
			UsingChamber = src.cUsingChamber;
			RecordTimeNo = src.cRecordTimeNo;
			OutMuxUse = src.cOutMuxUse;
			OutMuxBackup = src.cOutMuxBackup;

			Reserved = new int[src.lreserved.Length];
			Array.Copy(src.lreserved, 0, Reserved, 0, src.lreserved.Length);
		}
		
		/// <summary>
		/// state to string
		/// </summary>
		/// <returns></returns>
		public string StateToString()
		{
			switch (ChState)
			{
				case Constants.PS_STATE_LINE_OFF: return "disconnected";
				case Constants.PS_STATE_LINE_ON: return "connected";
				case Constants.PS_STATE_READY: return "ready";
				case Constants.PS_STATE_IDLE: return "idle";
				case Constants.PS_STATE_STANDBY: return "standby";
				case Constants.PS_STATE_RUN:
					switch (ChStepType)
					{
						case Constants.PS_STEP_NONE: return "run";
						case Constants.PS_STEP_CHARGE: return "Charge";
						case Constants.PS_STEP_DISCHARGE: return "Discharge";
						case Constants.PS_STEP_REST: return "Rest";
						case Constants.PS_STEP_OCV: return "OCV";
						case Constants.PS_STEP_IMPEDANCE: return "Impedance";
						case Constants.PS_STEP_END: return "End";
						case Constants.PS_STEP_LOOP: return "Loop";
						case Constants.PS_STEP_PATTERN: return "Pattern";
						default: return "run";
					} // end switch
				case Constants.PS_STATE_PAUSE: return "pause";
				case Constants.PS_STATE_MAINTENANCE: return "maintenance";
				case Constants.PS_STATE_END: return "Complete";
				default: return "disconnected";
			} // end switch
		}

		#endregion
	}
}
