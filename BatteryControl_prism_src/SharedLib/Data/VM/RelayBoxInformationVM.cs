using Prism.Mvvm;
using RelayBoxLib.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class RelayBoxInformationVM : BindableBase
	{
		#region property

		/// <summary>
		/// data source
		/// </summary>
		private RelayBoxInformationDTO m_Dto;
		public RelayBoxInformationDTO Dto
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

		public byte RelayCh1ModeState
		{
			get
			{
				return Dto.RelayCh1ModeState;
			}
			set
			{
				if (Dto.RelayCh1ModeState != value)
				{
					Dto.RelayCh1ModeState = value;
					RaisePropertyChanged("RelayCh1ModeState");
				}
			}
		}

		public byte RelayCh2ModeState
		{
			get
			{
				return Dto.RelayCh2ModeState;
			}
			set
			{
				if (Dto.RelayCh2ModeState != value)
				{
					Dto.RelayCh2ModeState = value;
					RaisePropertyChanged("RelayCh2ModeState");
				}
			}
		}

		public bool Ch1RelayG
		{
			get
			{
				return Dto.Ch1RelayG;
			}
			set
			{
				if (Dto.Ch1RelayG != value)
				{
					Dto.Ch1RelayG = value;
					RaisePropertyChanged("Ch1RelayG");
				}
			}
		}

		public bool Ch1RelayP
		{
			get
			{
				return Dto.Ch1RelayP;
			}
			set
			{
				if (Dto.Ch1RelayP != value)
				{
					Dto.Ch1RelayP = value;
					RaisePropertyChanged("Ch1RelayP");
				}
			}
		}

		public bool Ch1RelayM
		{
			get
			{
				return Dto.Ch1RelayM;
			}
			set
			{
				if (Dto.Ch1RelayM != value)
				{
					Dto.Ch1RelayM = value;
					RaisePropertyChanged("Ch1RelayM");
				}
			}
		}

		public bool Ch1CmdCheck
		{
			get
			{
				return Dto.Ch1CmdCheck;
			}
			set
			{
				if (Dto.Ch1CmdCheck != value)
				{
					Dto.Ch1CmdCheck = value;
					RaisePropertyChanged("Ch1CmdCheck");
				}
			}
		}

		public bool Ch1ModeSetError
		{
			get
			{
				return Dto.Ch1ModeSetError;
			}
			set
			{
				if (Dto.Ch1ModeSetError != value)
				{
					Dto.Ch1ModeSetError = value;
					RaisePropertyChanged("Ch1ModeSetError");
				}
			}
		}

		public bool Ch2RelayG
		{
			get
			{
				return Dto.Ch2RelayG;
			}
			set
			{
				if (Dto.Ch2RelayG != value)
				{
					Dto.Ch2RelayG = value;
					RaisePropertyChanged("Ch2RelayG");
				}
			}
		}

		public bool Ch2RelayP
		{
			get
			{
				return Dto.Ch2RelayP;
			}
			set
			{
				if (Dto.Ch2RelayP != value)
				{
					Dto.Ch2RelayP = value;
					RaisePropertyChanged("Ch2RelayP");
				}
			}
		}

		public bool Ch2RelayM
		{
			get
			{
				return Dto.Ch2RelayM;
			}
			set
			{
				if (Dto.Ch2RelayM != value)
				{
					Dto.Ch2RelayM = value;
					RaisePropertyChanged("Ch2RelayM");
				}
			}
		}

		public bool Ch2CmdCheck
		{
			get
			{
				return Dto.Ch2CmdCheck;
			}
			set
			{
				if (Dto.Ch2CmdCheck != value)
				{
					Dto.Ch2CmdCheck = value;
					RaisePropertyChanged("Ch2CmdCheck");
				}
			}
		}

		public bool Ch2ModeSetError
		{
			get
			{
				return Dto.Ch2ModeSetError;
			}
			set
			{
				if (Dto.Ch2ModeSetError != value)
				{
					Dto.Ch2ModeSetError = value;
					RaisePropertyChanged("Ch2ModeSetError");
				}
			}
		}

		#endregion

		#region method

		public RelayBoxInformationVM(RelayBoxInformationDTO dto)
		{
			Dto = dto;
		}

		public void SetValues(byte[] data)
		{
			RelayCh1ModeState = data[0];
			RelayCh2ModeState = data[1];

			Ch1RelayG = (data[4] & 0x01) == 0x01;
			Ch1RelayP = (data[4] & 0x02) == 0x02;
			Ch1RelayM = (data[4] & 0x04) == 0x04;
			Ch1CmdCheck = (data[4] & 0x10) == 0x10;
			Ch1ModeSetError = (data[4] & 0x20) == 0x20;

			Ch2RelayG = (data[5] & 0x01) == 0x01;
			Ch2RelayP = (data[5] & 0x02) == 0x02;
			Ch2RelayM = (data[5] & 0x04) == 0x04;
			Ch2CmdCheck = (data[5] & 0x10) == 0x10;
			Ch2ModeSetError = (data[5] & 0x20) == 0x20;
		}

		#endregion
	}
}
