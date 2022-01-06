using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_task_DTO : AResultSet
	{
		public int task_id { get; set; }

		public string task_name { get; set; }

		public string task_type { get; set; }

		public int task_seq { get; set; }

        public int step_no { get; set; }

        public int sub_step_no { get; set; }

        public string task_detail_name { get; set; }

		public string device_cd { get; set; }

		public string device_name { get; set; }

		public string task_condition { get; set; }

		public int task_group { get; set; }

		public string task_tag { get; set; }

		public int time_expect { get; set; }

		public string file_path { get; set; }

		public string disabled { get; set; }

		public string visibility { get; set; }

		public string BTRY_CODE { get; set; }
	}
}
