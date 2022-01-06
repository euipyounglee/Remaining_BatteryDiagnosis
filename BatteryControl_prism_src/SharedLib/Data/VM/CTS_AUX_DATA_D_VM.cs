using PneCtsLib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class CTS_AUX_DATA_D_VM : BindableBase
	{
		#region property

		/// <summary>
		/// Dto
		/// </summary>
		private CTS_AUX_DATA_D_DTO m_Dto;
		public CTS_AUX_DATA_D_DTO Dto
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
		public short ChNo
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
		/// Aux Type (0:온도, 1:전압, 2:온도(써미스터))
		/// </summary>
		public short ChType
		{
			get
			{
				return Dto.ChType;
			}
			set
			{
				Dto.ChType = value;
				RaisePropertyChanged("ChType");
			}
		}

		/// <summary>
		/// Aux 값
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

		public CTS_AUX_DATA_D_VM(CTS_AUX_DATA_D_DTO dto)
		{
			Dto = dto;
		}

		#endregion
	}
}
