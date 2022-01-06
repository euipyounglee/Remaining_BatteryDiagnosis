using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_log_bi_data_DTO : AResultSet
	{
		public DateTime log_dt { get; set; }

		public string run_id { get; set; }

		public int task_id { get; set; }

		public int task_seq { get; set; }

		public byte mbms_id { get; set; }

		public uint can_id { get; set; }

		public short voltage1 { get; set; }

		public short voltage2 { get; set; }

		public short voltage3 { get; set; }

		public short voltage4 { get; set; }

		public short voltage5 { get; set; }

		public short voltage6 { get; set; }

		public short voltage7 { get; set; }

		public short voltage8 { get; set; }

		public short voltage9 { get; set; }

		public short voltage10 { get; set; }

		public short voltage11 { get; set; }

		public short voltage12 { get; set; }

		public short voltage13 { get; set; }

		public short voltage14 { get; set; }

		public short voltage15 { get; set; }

		public short temperature1 { get; set; }

		public short temperature2 { get; set; }

	}
}
