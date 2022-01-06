using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_log_acir_measureresult_DTO : AResultSet
	{
		public DateTime log_dt { get; set; }

		public string run_id { get; set; }

		public int task_id { get; set; }

		public int task_seq { get; set; }

		public string hostname { get; set; }

		public ushort port { get; set; }

		public byte type { get; set; }

		public byte value { get; set; }

		public byte step_no { get; set; }

		public byte current_no { get; set; }

		public byte total_no { get; set; }

		public byte mode { get; set; }

		public float voltage { get; set; }

		public float temperature { get; set; }

		public float acir { get; set; }

		public float rs { get; set; }

		public float rp { get; set; }

		public uint reserved { get; set; }

		public uint reserved2 { get; set; }
	}
}
