using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Data.DTO
{
	public class BIBoxCellVoltageDTO : ABIBoxBaseDTO
	{
		#region property

		public byte Seq { get; set; }

		public short Cell1 { get; set; }

		public short Cell2 { get; set; }

		public short Cell3 { get; set; }

		#endregion

		#region method

		public BIBoxCellVoltageDTO(uint canId, byte[] data) : base(canId)
		{
			Seq = (byte)(data[7] - 0x50);
			Seq *= 3;
			Seq -= 2;

			Cell1 = ToShort(data[0], data[1]);
			Cell2 = ToShort(data[2], data[3]);
			Cell3 = ToShort(data[4], data[5]);
		}

		#endregion
	}
}
