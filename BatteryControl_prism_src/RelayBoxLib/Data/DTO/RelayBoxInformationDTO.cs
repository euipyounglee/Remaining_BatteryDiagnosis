using RelayBoxLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelayBoxLib.Data.DTO
{
	public class RelayBoxInformationDTO
	{
		#region property

		public byte RelayCh1ModeState { get; set; }

		public byte RelayCh2ModeState { get; set; }

		public bool Ch1RelayG { get; set; }

		public bool Ch1RelayP { get; set; }

		public bool Ch1RelayM { get; set; }

		public bool Ch1CmdCheck { get; set; }

		public bool Ch1ModeSetError { get; set; }

		public bool Ch2RelayG { get; set; }

		public bool Ch2RelayP { get; set; }

		public bool Ch2RelayM { get; set; }

		public bool Ch2CmdCheck { get; set; }

		public bool Ch2ModeSetError { get; set; }

		#endregion

		#region constructor

		public RelayBoxInformationDTO(byte[] data)
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

		#region method

		public RelayBoxModes GetCurrentMode(byte channel)
		{
			if (channel == 1)
			{
				if (Ch1RelayP) return RelayBoxModes.Plus;
				if (Ch1RelayM) return RelayBoxModes.Minus;
                if (Ch1CmdCheck || Ch1ModeSetError ) return RelayBoxModes.Error;
            }
			else
			{
				if (Ch2RelayP) return RelayBoxModes.Plus;
				if (Ch2RelayM) return RelayBoxModes.Minus;
                if (Ch2CmdCheck || Ch2ModeSetError) return RelayBoxModes.Error;
            }
			return RelayBoxModes.Off;
		}

		#endregion
	}
}
