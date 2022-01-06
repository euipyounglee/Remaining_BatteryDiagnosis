using PneCtsLib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class CTS_AUX_DATA_VM : BindableBase
	{
		#region property

		/// <summary>
		/// Dto
		/// </summary>
		private CTS_AUX_DATA_DTO m_Dto;
		public CTS_AUX_DATA_DTO Dto
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
		/// Aux Channel 번호
		/// </summary>
		public short AuxChNo
		{
			get
			{
				return Dto.AuxChNo;
			}
			set
			{
				Dto.AuxChNo = value;
				RaisePropertyChanged("AuxChNo");
			}
		}

		/// <summary>
		/// Aux Type (0:온도, 1:전압, 2:온도(써미스터))
		/// </summary>
		public byte AuxChType
		{
			get
			{
				return Dto.AuxChType;
			}
			set
			{
				Dto.AuxChType = value;
				RaisePropertyChanged("AuxChType");
			}
		}

		/// <summary>
		/// 써미스터 온도 테이블(장비마다 설정 값이 달라 장비에서 확인 후 설정)
		/// </summary>
		public byte AuxTempTableType
		{
			get
			{
				return Dto.AuxTempTableType;
			}
			set
			{
				Dto.AuxTempTableType = value;
				RaisePropertyChanged("AuxTempTableType");
			}
		}

		/// <summary>
		/// Aux 값 (온도/써미스터: 1000 == 1℃, Voltage: uV)
		/// </summary>
		public int Value
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

		#endregion

		#region method

		public CTS_AUX_DATA_VM(CTS_AUX_DATA_DTO dto)
		{
			Dto = dto;
		}

		#endregion
	}
}
