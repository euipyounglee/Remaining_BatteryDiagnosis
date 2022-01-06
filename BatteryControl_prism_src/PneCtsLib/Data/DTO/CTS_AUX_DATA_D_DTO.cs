using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_AUX_DATA_D_DTO
	{
		#region property

		public short ChNo { get; set; }

		public short ChType { get; set; }

		public int Value { get; set; }

		#endregion

		#region method

		public CTS_AUX_DATA_D_DTO(CTS_AUX_DATA_D src)
		{
			ChNo = src.chNo;
			ChType = src.chType;
			Value = src.lValue;
		}

		#endregion
	}
}
