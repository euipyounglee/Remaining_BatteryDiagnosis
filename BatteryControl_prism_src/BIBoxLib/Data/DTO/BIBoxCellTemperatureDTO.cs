using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Data.DTO
{
	public class BIBoxCellTemperatureDTO : ABIBoxBaseDTO
	{
		#region property

		public short Temperature1 { get; set; }

		public short Temperature2 { get; set; }

		#endregion

		#region method

		public BIBoxCellTemperatureDTO(uint canId, byte[] data) : base(canId)
		{
			Temperature1 = ToShort(data[0], data[1]);
			Temperature2 = ToShort(data[2], data[3]);
		}

		#endregion
	}
}
