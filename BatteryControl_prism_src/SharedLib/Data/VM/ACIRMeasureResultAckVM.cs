using ACIRLib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class ACIRMeasureResultAckVM : BindableBase
	{
		#region property

		/// <summary>
		/// dto
		/// </summary>
		private MeasureResultAckDTO m_Dto;
		public MeasureResultAckDTO Dto
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
		/// 1~16: 셀
		/// 0x00: 전체
		/// </summary>
		public byte BatteryType
		{
			get
			{
				return Dto.BatteryType;
			}
			set
			{
				Dto.BatteryType = value;
				RaisePropertyChanged("BatteryType");
			}
		}

		/// <summary>
		/// ng:0, ok:1
		/// </summary>
		public byte Value
		{
			get
			{
				return Dto.Value;
			}
			set
			{
				Dto.Value = value;
				RaisePropertyChanged("Value");
			}
		}

		/// <summary>
		/// step no
		/// </summary>
		public byte StepNo
		{
			get
			{
				return Dto.StepNo;
			}
			set
			{
				Dto.StepNo = value;
				RaisePropertyChanged("StepNo");
			}
		}

		/// <summary>
		/// current no
		/// </summary>
		public byte CurrentNo
		{
			get
			{
				return Dto.CurrentNo;
			}
			set
			{
				Dto.CurrentNo = value;
				RaisePropertyChanged("CurrentNo");
			}
		}

		/// <summary>
		/// total no
		/// single: 2
		/// spectrum(6 point) : 19
		/// </summary>
		public byte TotalNo
		{
			get
			{
				return Dto.TotalNo;
			}
			set
			{
				Dto.TotalNo = value;
				RaisePropertyChanged("TotalNo");
			}
		}

		/// <summary>
		/// mode
		/// 0x00: single
		/// 0x01: spectrum
		/// </summary>
		public byte Mode
		{
			get
			{
				return Dto.Mode;
			}
			set
			{
				Dto.Mode = value;
				RaisePropertyChanged("Mode");
			}
		}

		/// <summary>
		/// 전압
		/// </summary>
		public float Voltage
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
		/// 온도
		/// </summary>
		public float Temp
		{
			get
			{
				return Dto.Temp;
			}
			set
			{
				Dto.Temp = value;
				RaisePropertyChanged("Temp");
			}
		}

		/// <summary>
		/// acir
		/// </summary>
		public float ACIR
		{
			get
			{
				return Dto.ACIR;
			}
			set
			{
				Dto.ACIR = value;
				RaisePropertyChanged("ACIR");
			}
		}

		/// <summary>
		/// rs
		/// </summary>
		public float Rs
		{
			get
			{
				return Dto.Rs;
			}
			set
			{
				Dto.Rs = value;
				RaisePropertyChanged("Rs");
			}
		}

		/// <summary>
		/// rp
		/// </summary>
		public float Rp
		{
			get
			{
				return Dto.Rp;
			}
			set
			{
				Dto.Rp = value;
				RaisePropertyChanged("Rp");
			}
		}

		#endregion

		#region method

		public ACIRMeasureResultAckVM(MeasureResultAckDTO dto)
		{
			Dto = dto;
		}

		#endregion
	}
}
