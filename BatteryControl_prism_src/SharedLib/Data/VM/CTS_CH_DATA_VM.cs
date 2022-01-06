using PneCtsLib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class CTS_CH_DATA_VM : BindableBase
	{
		#region property

		/// <summary>
		/// dto
		/// </summary>
		private CTS_CH_DATA_DTO m_Dto;
		public CTS_CH_DATA_DTO Dto
		{
			get
			{
				return m_Dto;
			}
			set
			{
				m_Dto = value;
				RaisePropertyChanged("Dto");
			}
		}

		/// <summary>
		/// Channel Number	(0-Base, ex. 1번 채널일 경우 1, 3번 채널일 경우 3)
		/// </summary>
		public byte ChNo
		{
			get
			{
				return Dto.ChNo;
			}
			set
			{
				Dto.ChNo = value;
				RaisePropertyChanged("ChNo");
			}
		}

		/// <summary>
		/// 채널 운용 상태 \ref define_state_ch
		/// </summary>
		public byte ChState
		{
			get
			{
				return Dto.ChState;
			}
			set
			{
				Dto.ChState = value;
				RaisePropertyChanged("ChState");
			}
		}

		/// <summary>
		/// 채널 동작 상태 \ref chStepType
		/// </summary>
		public byte ChStepType
		{
			get
			{
				return Dto.ChStepType;
			}
			set
			{
				Dto.ChStepType = value;
				RaisePropertyChanged("ChStepType");
			}
		}

		/// <summary>
		/// Step의 충전/방전 Mode (저항 및 전압 값을 일정하게 하여 충전/방전하기 위한 mode) \ref chMode
		/// </summary>
		public byte ChMode
		{
			get
			{
				return Dto.ChMode;
			}
			set
			{
				Dto.ChMode = value;
				RaisePropertyChanged("ChMode");
			}
		}

		/// <summary>
		/// data 저장 방식(0:For Display Data, 1:For Saving Data(이 값이 1일 경우 data 저장 시작))
		/// </summary>
		public byte ChDataSelect
		{
			get
			{
				return Dto.ChDataSelect;
			}
			set
			{
				Dto.ChDataSelect = value;
				RaisePropertyChanged("ChDataSelect");
			}
		}

		/// <summary>
		/// 채널 코드(정상 및 오류 상태) \ref ChannelCode
		/// </summary>
		public byte ChCode
		{
			get
			{
				return Dto.ChCode;
			}
			set
			{
				Dto.ChCode = value;
				RaisePropertyChanged("ChCode");
			}
		}

		/// <summary>
		/// 스텝 번호
		/// </summary>
		public byte ChStepNo
		{
			get
			{
				return Dto.ChStepNo;
			}
			set
			{
				Dto.ChStepNo = value;
				RaisePropertyChanged("ChStepNo");
			}
		}

		/// <summary>
		/// ATS 필요 없음, Grade Code
		/// </summary>
		public byte ChGradeCode
		{
			get
			{
				return Dto.ChGradeCode;
			}
			set
			{
				Dto.ChGradeCode = value;
				RaisePropertyChanged("ChGradeCode");
			}
		}

		/// <summary>
		/// 전압 (uV)
		/// </summary>
		public int Voltage
		{
			get
			{
				return Dto.Voltage;
			}
			set
			{
				Dto.Voltage = value;
				RaisePropertyChanged("Voltage");
			}
		}

		/// <summary>
		/// 전류 (uA)
		/// </summary>
		public int Current
		{
			get
			{
				return Dto.Current;
			}
			set
			{
				Dto.Current = value;
				RaisePropertyChanged("Current");
			}
		}

		/// <summary>
		/// 충전용량 (uA)
		/// </summary>
		public int ChargeAh
		{
			get
			{
				return Dto.ChargeAh;
			}
			set
			{
				Dto.ChargeAh = value;
				RaisePropertyChanged("ChargeAh");
			}
		}

		/// <summary>
		/// 방전용량 (uA)
		/// </summary>
		public int DisChargeAh
		{
			get
			{
				return Dto.DisChargeAh;
			}
			set
			{
				Dto.DisChargeAh = value;
				RaisePropertyChanged("DisChargeAh");
			}
		}

		/// <summary>
		/// Capacitance (uF)
		/// </summary>
		public int Capacitance
		{
			get
			{
				return Dto.Capacitance;
			}
			set
			{
				Dto.Capacitance = value;
				RaisePropertyChanged("Capacitance");
			}
		}

		/// <summary>
		/// Watt (mW)
		/// </summary>
		public int Watt
		{
			get
			{
				return Dto.Watt;
			}
			set
			{
				Dto.Watt = value;
				RaisePropertyChanged("Watt");
			}
		}

		/// <summary>
		/// Charge Wh (mW)
		/// </summary>
		public int ChargeWh
		{
			get
			{
				return Dto.ChargeWh;
			}
			set
			{
				Dto.ChargeWh = value;
				RaisePropertyChanged("ChargeWh");
			}
		}

		/// <summary>
		/// Discharge Wh (mW)
		/// </summary>
		public int DisChargeWh
		{
			get
			{
				return Dto.DisChargeWh;
			}
			set
			{
				Dto.DisChargeWh = value;
				RaisePropertyChanged("DisChargeWh");
			}
		}

		/// <summary>
		/// 스텝 진행 시간(day)
		/// </summary>
		public uint StepDay
		{
			get
			{
				return Dto.StepDay;
			}
			set
			{
				Dto.StepDay = value;
				RaisePropertyChanged("StepDay");
			}
		}

		/// <summary>
		/// 스텝 진행 시간 (10msec), sec = ulStepTime/100.0f
		/// </summary>
		public uint StepTime
		{
			get
			{
				return Dto.StepTime;
			}
			set
			{
				Dto.StepTime = value;
				RaisePropertyChanged("StepTime");
			}
		}

		/// <summary>
		/// 시험 진행 시간(day)
		/// </summary>
		public uint TotalDay
		{
			get
			{
				return Dto.TotalDay;
			}
			set
			{
				Dto.TotalDay = value;
				RaisePropertyChanged("TotalDay");
			}
		}

		/// <summary>
		/// 시험 진행 시간 (10msec), sec = ulTotalTime/100.0f
		/// </summary>
		public uint TotalTime
		{
			get
			{
				return Dto.TotalTime;
			}
			set
			{
				Dto.TotalTime = value;
				RaisePropertyChanged("TotalTime");
			}
		}

		/// <summary>
		/// DC 저항값 (uohm)
		/// </summary>
		public int Impedance
		{
			get
			{
				return Dto.Impedance;
			}
			set
			{
				Dto.Impedance = value;
				RaisePropertyChanged("Impedance");
			}
		}

		/// <summary>
		/// 커맨드 예약 상태 (0:None, 1:Stop, 2:Pause)
		/// </summary>
		public byte ChReservedCmd
		{
			get
			{
				return Dto.ChReservedCmd;
			}
			set
			{
				Dto.ChReservedCmd = value;
				RaisePropertyChanged("ChReservedCmd");
			}
		}

		/// <summary>
		/// 통신 상태 Bit LSB(0:정상, 1:이상) 1st bit:Aux 온도 통신 상태, 2nd bit:Aux 전압 통신 상태, 3rd bit:CAN Master 통신상태, 4th bit: CAN Slave 통신상태 (ex. bit : 0000 0001이면 Aux 온도 통신 상태 이상)
		/// </summary>
		public byte ChCommState
		{
			get
			{
				return Dto.ChCommState;
			}
			set
			{
				Dto.ChCommState = value;
				RaisePropertyChanged("ChCommState");
			}
		}

		/// <summary>
		/// ATS 필요 없음, 출력 상태 Bit LSB(0:정상, 1:이상) 1st bit:keyOn, 2nd bit:ChargeOn, 3rd bit:PackRealyOn
		/// </summary>
		public byte ChOutputState
		{
			get
			{
				return Dto.ChOutputState;
			}
			set
			{
				Dto.ChOutputState = value;
				RaisePropertyChanged("ChOutputState");
			}
		}

		/// <summary>
		/// ATS 필요 없음,입력 상태
		/// </summary>
		public byte ChInputState
		{
			get
			{
				return Dto.ChInputState;
			}
			set
			{
				Dto.ChInputState = value;
				RaisePropertyChanged("ChInputState");
			}
		}

		/// <summary>
		/// Aux 개수
		/// </summary>
		public short AuxCount
		{
			get
			{
				return Dto.AuxCount;
			}
			set
			{
				Dto.AuxCount = value;
				RaisePropertyChanged("AuxCount");
			}
		}

		/// <summary>
		/// Can 개수
		/// </summary>
		public short CanCount
		{
			get
			{
				return Dto.CanCount;
			}
			set
			{
				Dto.CanCount = value;
				RaisePropertyChanged("CanCount");
			}
		}

		/// <summary>
		/// 전체 Cycle 개수
		/// </summary>
		public uint TotalCycleNum
		{
			get
			{
				return Dto.TotalCycleNum;
			}
			set
			{
				Dto.TotalCycleNum = value;
				RaisePropertyChanged("TotalCycleNum");
			}
		}

		/// <summary>
		/// 현재 Cycle 수
		/// </summary>
		public uint CurrentCycleNum
		{
			get
			{
				return Dto.CurrentCycleNum;
			}
			set
			{
				Dto.CurrentCycleNum = value;
				RaisePropertyChanged("CurrentCycleNum");
			}
		}

		/// <summary>
		/// 그룹1 누적 Cycle 갯수
		/// </summary>
		public uint AccCycleGroupNum1
		{
			get
			{
				return Dto.AccCycleGroupNum1;
			}
			set
			{
				Dto.AccCycleGroupNum1 = value;
				RaisePropertyChanged("AccCycleGroupNum1");
			}
		}

		/// <summary>
		/// 그룹2 누적 Cycle 갯수
		/// </summary>
		public uint AccCycleGroupNum2
		{
			get
			{
				return Dto.AccCycleGroupNum2;
			}
			set
			{
				Dto.AccCycleGroupNum2 = value;
				RaisePropertyChanged("AccCycleGroupNum2");
			}
		}

		/// <summary>
		/// 그룹3 누적 Cycle 갯수
		/// </summary>
		public uint AccCycleGroupNum3
		{
			get
			{
				return Dto.AccCycleGroupNum3;
			}
			set
			{
				Dto.AccCycleGroupNum3 = value;
				RaisePropertyChanged("AccCycleGroupNum3");
			}
		}

		/// <summary>
		/// 그룹4 누적 Cycle 갯수
		/// </summary>
		public uint AccCycleGroupNum4
		{
			get
			{
				return Dto.AccCycleGroupNum4;
			}
			set
			{
				Dto.AccCycleGroupNum4 = value;
				RaisePropertyChanged("AccCycleGroupNum4");
			}
		}

		/// <summary>
		/// 그룹5 누적 Cycle 갯수
		/// </summary>
		public uint AccCycleGroupNum5
		{
			get
			{
				return Dto.AccCycleGroupNum5;
			}
			set
			{
				Dto.AccCycleGroupNum5 = value;
				RaisePropertyChanged("AccCycleGroupNum5");
			}
		}

		/// <summary>
		/// 그룹1 Multi Cycle 갯수
		/// </summary>
		public uint MultiCycleGroupNum1
		{
			get
			{
				return Dto.MultiCycleGroupNum1;
			}
			set
			{
				Dto.MultiCycleGroupNum1 = value;
				RaisePropertyChanged("MultiCycleGroupNum1");
			}
		}

		/// <summary>
		/// 그룹2 Multi Cycle 갯수
		/// </summary>
		public uint MultiCycleGroupNum2
		{
			get
			{
				return Dto.MultiCycleGroupNum2;
			}
			set
			{
				Dto.MultiCycleGroupNum2 = value;
				RaisePropertyChanged("MultiCycleGroupNum2");
			}
		}

		/// <summary>
		/// 그룹3 Multi Cycle 갯수
		/// </summary>
		public uint MultiCycleGroupNum3
		{
			get
			{
				return Dto.MultiCycleGroupNum3;
			}
			set
			{
				Dto.MultiCycleGroupNum3 = value;
				RaisePropertyChanged("MultiCycleGroupNum3");
			}
		}

		/// <summary>
		/// 그룹4 Multi Cycle 갯수
		/// </summary>
		public uint MultiCycleGroupNum4
		{
			get
			{
				return Dto.MultiCycleGroupNum4;
			}
			set
			{
				Dto.MultiCycleGroupNum4 = value;
				RaisePropertyChanged("MultiCycleGroupNum4");
			}
		}

		/// <summary>
		/// 그룹5 Multi Cycle 갯수
		/// </summary>
		public uint MultiCycleGroupNum5
		{
			get
			{
				return Dto.MultiCycleGroupNum5;
			}
			set
			{
				Dto.MultiCycleGroupNum5 = value;
				RaisePropertyChanged("MultiCycleGroupNum5");
			}
		}

		/// <summary>
		/// 현재 스텝 평균 전압 (uV)
		/// </summary>
		public int AvgVoltage
		{
			get
			{
				return Dto.AvgVoltage;
			}
			set
			{
				Dto.AvgVoltage = value;
				RaisePropertyChanged("AvgVoltage");
			}
		}

		/// <summary>
		/// 현재 스텝 평균 전류 (uA)
		/// </summary>
		public int AvgCurrent
		{
			get
			{
				return Dto.AvgCurrent;
			}
			set
			{
				Dto.AvgCurrent = value;
				RaisePropertyChanged("AvgCurrent");
			}
		}

		/// <summary>
		/// 모듈에서 저장하는 Data의 순서 번호
		/// </summary>
		public int SaveSequence
		{
			get
			{
				return Dto.SaveSequence;
			}
			set
			{
				Dto.SaveSequence = value;
				RaisePropertyChanged("SaveSequence");
			}
		}

		/// <summary>
		/// CV 시간(Day)
		/// </summary>
		public uint CVDay
		{
			get
			{
				return Dto.CVDay;
			}
			set
			{
				Dto.CVDay = value;
				RaisePropertyChanged("CVDay");
			}
		}

		/// <summary>
		/// CV 시간 (10msec), sec = ulCVTime/100.0f
		/// </summary>
		public uint CVTime
		{
			get
			{
				return Dto.CVTime;
			}
			set
			{
				Dto.CVTime = value;
				RaisePropertyChanged("CVTime");
			}
		}

		/// <summary>
		/// 현재 데이터에 대한 시간정보 YYYYMMDD HHMMSS.mmm 변환 가능한 시간 20080723 214055.123
		/// </summary>
		public int[] SyncTime
		{
			get
			{
				return Dto.SyncTime;
			}
			set
			{
				Dto.SyncTime = value;
				RaisePropertyChanged("SyncTime");
			}
		}

		/// <summary>
		/// ATS 필요 없음, 전압 입력
		/// </summary>
		public int VoltageInput
		{
			get
			{
				return Dto.VoltageInput;
			}
			set
			{
				Dto.VoltageInput = value;
				RaisePropertyChanged("VoltageInput");
			}
		}

		/// <summary>
		/// ATS 필요 없음, 전압 파워
		/// </summary>
		public int VoltagePower
		{
			get
			{
				return Dto.VoltagePower;
			}
			set
			{
				Dto.VoltagePower = value;
				RaisePropertyChanged("VoltagePower");
			}
		}

		/// <summary>
		/// ATS 필요 없음, 전압 버스
		/// </summary>
		public int VoltageBus
		{
			get
			{
				return Dto.VoltageBus;
			}
			set
			{
				Dto.VoltageBus = value;
				RaisePropertyChanged("VoltageBus");
			}
		}

		/// <summary>
		/// ATS 필요 없음, 챔버 연동
		/// </summary>
		public byte UsingChamber
		{
			get
			{
				return Dto.UsingChamber;
			}
			set
			{
				Dto.UsingChamber = value;
				RaisePropertyChanged("UsingChamber");
			}
		}

		/// <summary>
		/// ATS 필요 없음, (1 base : R1,R2,R3)
		/// </summary>
		public byte RecordTimeNo
		{
			get
			{
				return Dto.RecordTimeNo;
			}
			set
			{
				Dto.RecordTimeNo = value;
				RaisePropertyChanged("RecordTimeNo");
			}
		}

		/// <summary>
		/// ATS 필요 없음, 먹스 사용 (0:not use, 1:use)
		/// </summary>
		public byte OutMuxUse
		{
			get
			{
				return Dto.OutMuxUse;
			}
			set
			{
				Dto.OutMuxUse = value;
				RaisePropertyChanged("OutMuxUse");
			}
		}

		/// <summary>
		/// ATS 필요 없음, 먹스 설정 (0:open, 1:Mux A, 2:Mux B)
		/// </summary>
		public byte OutMuxBackup
		{
			get
			{
				return Dto.OutMuxBackup;
			}
			set
			{
				Dto.OutMuxBackup = value;
				RaisePropertyChanged("OutMuxBackup");
			}
		}

		/// <summary>
		/// 예비
		/// </summary>
		public int[] Reserved
		{
			get
			{
				return Dto.Reserved;
			}
			set
			{
				Dto.Reserved = value;
				RaisePropertyChanged("Reserved");
			}
		}

		/// <summary>
		/// is updated
		/// </summary>
		private bool m_IsUpdated;
		public bool IsUpdated
		{
			get
			{
				return m_IsUpdated;
			}
			set
			{
				m_IsUpdated = value;
				RaisePropertyChanged("IsUpdated");

				if (value)
				{
					TurnOffUpdateFlag();
				} // end if
			}
		}


		#endregion

		#region method

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="dto"></param>
		public CTS_CH_DATA_VM(CTS_CH_DATA_DTO dto)
		{
			Dto = dto;
		}

		/// <summary>
		/// turn off update flag after delay
		/// </summary>
		private async void TurnOffUpdateFlag()
		{
			await Task.Delay(200);
			IsUpdated = false;
		}

		#endregion
	}
}
