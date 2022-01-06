using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_code_DTO : AResultSet
	{
		public string major_cd { get; set; }

		public string minor_cd { get; set; }

		public string major_nm { get; set; }

		public string minor_nm { get; set; }

		public int sort_idx { get; set; }

		public DateTime create_dt { get; set; }

		public DateTime update_dt { get; set; }
	}
}
