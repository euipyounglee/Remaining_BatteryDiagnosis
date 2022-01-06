using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_CAN_DATA_DTO
	{
		#region property

		public byte CanType { get; set; }

		public byte DataType { get; set; }

		public short FunctionDivision { get; set; }

		public CTS_CAN_VALUE_DTO CanVal { get; set; }

		#endregion

		#region method

		public CTS_CAN_DATA_DTO(CTS_CAN_DATA src)
		{
			CanType = src.canType;
			DataType = src.data_type;
			FunctionDivision = src.function_division;
			CanVal = new CTS_CAN_VALUE_DTO(src.canVal);
		}

		#endregion
	}
}
