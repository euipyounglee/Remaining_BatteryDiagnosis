using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class pg_btry_srvive_evl_end_DTO : AResultSet
	{
		public int INSPCT_SN { get; set; }

		public string INSPCT_REQRE_TIME { get; set; }

		public string INSPCT_END_DT { get; set; }

		public string INSPCT_GRAD_TY { get; set; }

		public float SOC { get; set; }

		public float SOP { get; set; }

		public float SOH { get; set; }

		public float SOB { get; set; }

	}
}
