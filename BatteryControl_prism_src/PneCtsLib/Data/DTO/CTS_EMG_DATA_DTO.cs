using PneCtsLib.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PneCtsLib.Data.DTO
{
	public class CTS_EMG_DATA_DTO
	{
		#region property

		public short Code { get; set; }

		public short Value { get; set; }

		public int Reserved { get; set; }

		public byte[] Name { get; set; }

		#endregion

		#region method

		public CTS_EMG_DATA_DTO(CTS_EMG_DATA src)
		{
			Code = src.Code;
			Value = src.Value;
			Reserved = src.reserved;

			Name = new byte[src.szName.Length];
			Array.Copy(src.szName, 0, Name, 0, src.szName.Length);
		}

		#endregion
	}
}
