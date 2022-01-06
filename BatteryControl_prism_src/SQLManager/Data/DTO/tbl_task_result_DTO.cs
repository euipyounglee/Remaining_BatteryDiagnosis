using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_task_result_DTO : AResultSet
	{
		public string run_id { get; set; }

		public DateTime evaluation_dt { get; set; }

		public string evaluation_type { get; set; }

		public TimeSpan time_required { get; set; }

		public string grade { get; set; }

		public float soc { get; set; }

		public float soh { get; set; }

		public float sob { get; set; }

		public float sop { get; set; }

		public string BTRY_CODE { get; set; }

		public string BTRY_TY { get; set; }

		public string MARK_CODE { get; set; }

		public string MAKR_DESC { get; set; }

		public string MODEL_CODE { get; set; }

		public string MODL_DESC { get; set; }

		public string CONFIG { get; set; }

		public DateTime create_dt { get; set; }

		public DateTime update_dt { get; set; }

	}
}
