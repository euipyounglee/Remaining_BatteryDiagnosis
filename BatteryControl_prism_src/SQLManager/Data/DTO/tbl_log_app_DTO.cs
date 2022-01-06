using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_log_app_DTO : AResultSet
	{
		public DateTime log_dt { get; set; }

		public string log_level { get; set; }

		public string device_cd { get; set; }

		public string log_msg { get; set; }
	}
}
