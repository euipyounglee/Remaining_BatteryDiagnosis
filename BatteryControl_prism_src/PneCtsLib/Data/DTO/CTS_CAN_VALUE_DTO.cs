using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_CAN_VALUE_DTO
	{
		#region property
		
		public CTS_CAN_VALUE Value { get; set; }
		
		#endregion

		#region method

		public CTS_CAN_VALUE_DTO(CTS_CAN_VALUE src)
		{
			Value = new CTS_CAN_VALUE
			{
				strVal = new sbyte[src.strVal.Length]
			};
			Array.Copy(src.strVal, 0, Value.strVal, 0, src.strVal.Length);
		}

		#endregion
	}
}
