using PneCtsLib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class CTS_CAN_DATA_VM : BindableBase
	{
		#region property

		/// <summary>
		/// Dto
		/// </summary>
		private CTS_CAN_DATA_DTO m_Dto;
		public CTS_CAN_DATA_DTO Dto
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
		/// CAN 타입 (0:master, 1:slave)
		/// </summary>
		public byte CanType
		{
			get
			{
				return Dto.CanType;
			}
			set
			{
				Dto.CanType = value;
				RaisePropertyChanged("CanType");
			}
		}

		/// <summary>
		/// DATA 타입 (0:signed, 1:unsigned, 2:string)
		/// </summary>
		public byte DataType
		{
			get
			{
				return Dto.DataType;
			}
			set
			{
				Dto.DataType = value;
				RaisePropertyChanged("DataType");
			}
		}

		/// <summary>
		/// Division Code
		/// </summary>
		public short FunctionDivision
		{
			get
			{
				return Dto.FunctionDivision;
			}
			set
			{
				Dto.FunctionDivision = value;
				RaisePropertyChanged("FunctionDivision");
			}
		}

		/// <summary>
		/// Can Value
		/// </summary>
		private CTS_CAN_VALUE_VM m_CanVal;
		public CTS_CAN_VALUE_VM CanVal
		{
			get
			{
				return m_CanVal;
			}
			set
			{
				m_CanVal = value;
				RaisePropertyChanged("CanVal");
			}
		}
		
		#endregion

		#region method

		public CTS_CAN_DATA_VM(CTS_CAN_DATA_DTO dto)
		{
			Dto = dto;
			CanVal = new CTS_CAN_VALUE_VM(dto.CanVal);
		}

		#endregion
	}
}
