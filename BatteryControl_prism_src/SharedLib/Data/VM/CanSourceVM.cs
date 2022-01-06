using I7565H1Lib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class CanSourceVM : BindableBase
	{
		#region property

		private CanSourceDTO m_Dto;
		public CanSourceDTO Dto
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

		public DateTime LogDt
		{
			get
			{
				return Dto.LogDt;
			}
		}

		public byte Mode
		{
			get
			{
				return Dto.Mode;
			}
		}

		public byte RTR
		{
			get
			{
				return Dto.RTR;
			}
		}

		public byte DLC
		{
			get
			{
				return Dto.DLC;
			}
		}

		public string CanId
		{
			get
			{
				return Dto.CanId;
			}
		}

		public string MessageTime
		{
			get
			{
				return Dto.MessageTime;
			}
		}

		public string HexPrint
		{
			get
			{
				return Dto.HexPrint;
			}
		}

		#endregion

		#region method

		public CanSourceVM(CanSourceDTO dto)
		{
			Dto = dto;
		}

		#endregion

	}
}
