using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIBoxLib.Data.DTO
{
	public class BIBoxConfigurationDTO : ABIBoxBaseDTO
	{
		#region property

		public byte ModuleCount { get; set; }

		public byte CellCount { get; set; }

		public byte TempType { get; set; }

		public byte TempCount { get; set; }

		#endregion

		#region method

		public BIBoxConfigurationDTO(uint canId, byte[] data) : base(canId)
		{
			ModuleCount = data[1];
			CellCount = data[2];
			TempType = data[3];
			TempCount = data[4];
		}

		#endregion
	}
}
