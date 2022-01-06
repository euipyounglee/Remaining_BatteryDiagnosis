using ACIRLib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class ACIRMeasureDataAckVM : BindableBase
	{
		#region property

		/// <summary>
		/// dto
		/// </summary>
		private MeasureDataAckDTO m_Dto;
		public MeasureDataAckDTO Dto
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
		/// 0x01: 팩
		/// 0x00: 모듈
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
		/// hz
		/// </summary>
		public float Hz
		{
			get
			{
				return Dto.Hz;
			}
			set
			{
				Dto.Hz = value;
				RaisePropertyChanged("Hz");
			}
		}

		/// <summary>
		/// re
		/// </summary>
		public float Re
		{
			get
			{
				return Dto.Re;
			}
			set
			{
				Dto.Re = value;
				RaisePropertyChanged("Re");
			}
		}

		/// <summary>
		/// im
		/// </summary>
		public float Im
		{
			get
			{
				return Dto.Im;
			}
			set
			{
				Dto.Im = value;
				RaisePropertyChanged("Im");
			}
		}

		#endregion

		#region method

		public ACIRMeasureDataAckVM(MeasureDataAckDTO dto)
		{
			Dto = dto;
		}

		#endregion
	}
}
