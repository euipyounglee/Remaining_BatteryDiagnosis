using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class pg_btry_srvive_evl_check_DTO : AResultSet
	{
		public int INSPCT_SN { get; set; }

		public int STEP_SN { get; set; }

		public string MODE { get; set; }

		public string CHRG { get; set; }

		public string CND { get; set; }

		public string EXPECT_TIME { get; set; }

		public string PROGRS_SITTN { get; set; }

		public string PROGRS_STTUS { get; set; }
	}
}
