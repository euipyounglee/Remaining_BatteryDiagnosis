using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Data.DTO
{
	public class BIBoxModuleVoltageDTO : ABIBoxBaseDTO
	{
		#region property

		public short PackVoltage { get; set; }

		#endregion

		#region method

		public BIBoxModuleVoltageDTO(uint canId, byte[] data) : base(canId)
		{
			PackVoltage = ToShort(data[0], data[1]);
		}

		#endregion
	}
}
