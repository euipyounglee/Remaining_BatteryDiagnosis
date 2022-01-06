using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class pg_dev_info_DTO : AResultSet
	{
		public int DEV_SN { get; set; }

		public string DEV_TY { get; set; }

		public string DEV_NO { get; set; }

		public string PORTAL_DB_CONN { get; set; }

		public string PORTAL_REST_API { get; set; }

		public int FAST_TIME { get; set; }

		public int STANDARD_TIME { get; set; }

		public int PRECISION_TIME { get; set; }

		public int FAST_STEP { get; set; }

		public int STANDARD_STEP { get; set; }

		public int PRECISION_STEP { get; set; }
	}
}
