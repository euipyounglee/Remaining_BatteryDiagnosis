using PneCtsLib.Data.DTO;
using PneCtsLib.Defines;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class CTS_CAN_VALUE_VM : BindableBase
	{
		#region property

		/// <summary>
		/// Dto
		/// </summary>
		private CTS_CAN_VALUE_DTO m_Dto;
		public CTS_CAN_VALUE_DTO Dto
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
		/// value
		/// </summary>
		public CTS_CAN_VALUE Value
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

		public CTS_CAN_VALUE_VM(CTS_CAN_VALUE_DTO dto)
		{
			Dto = dto;
		}

		#endregion
	}
}
