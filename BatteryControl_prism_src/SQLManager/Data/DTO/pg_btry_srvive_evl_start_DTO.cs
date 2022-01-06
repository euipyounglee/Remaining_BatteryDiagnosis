using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class pg_btry_srvive_evl_start_DTO : AResultSet
	{
		public int INSPCT_SN { get; set; }

		public string INSPCT_BTRY { get; set; }

		public string INSPCT_MAKR { get; set; }

		public string INSPCT_MODL { get; set; }

		public string INSPCT_CONFIG { get; set; }

		public string BRCD_NO { get; set; }

		public string VHCLE_MAKR_TY { get; set; }

		public string VHCLE_MODL_TY { get; set; }

        public float CELL_MIN_VLTGE { get; set; }

        public float CELL_MAX_VLTGE { get; set; }

		public float VLTGE { get; set; }

		public float CPCTY { get; set; }

		public string COMPOSITION { get; set; }

		public float MUMM_VLTGE { get; set; }

		public float MXMM_VLTGE { get; set; }

		public float BTRY_ENERGY { get; set; }

		public string BTRY_CODE { get; set; }

		public string EQPMN_IP { get; set; }

		public string CHANNEL { get; set; }

		public string CNNC_EQPMN { get; set; }

		public string INSPCT_TY { get; set; }

		public string INSPCT_BEGIN_DT { get; set; }

		public string INSPCT_EXPECT_END_DT { get; set; }

		public int INSPCT_COMPT_STEP { get; set; }

		public string LOGIN_ID { get; set; }

	}
}
