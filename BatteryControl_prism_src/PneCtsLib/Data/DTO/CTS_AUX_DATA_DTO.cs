using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_AUX_DATA_DTO
	{
		#region property

		public short AuxChNo { get; set; }

		public byte AuxChType { get; set; }

		public byte AuxTempTableType { get; set; }

		public int Value { get; set; }

		#endregion

		#region method

		public CTS_AUX_DATA_DTO(CTS_AUX_DATA src)
		{
			AuxChNo = src.auxChNo;
			AuxChType = src.auxChType;
			AuxTempTableType = src.auxTempTableType;
			Value = src.lValue;
		}

		#endregion
	}
}
