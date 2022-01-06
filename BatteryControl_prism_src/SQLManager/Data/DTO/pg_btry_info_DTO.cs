using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class pg_btry_info_DTO : AResultSet
	{
		public uint NO { get; set; }

		public string BTRY_CODE { get; set; }

		public string BTRY_TY { get; set; }

		public string MARK_CODE { get; set; }

		public string MAKR_DESC { get; set; }

		public string MODEL_CODE { get; set; }

		public string MODL_DESC { get; set; }

		public string CONFIG { get; set; }

		public int TYPE_P { get; set; }

		public int TYPE_S { get; set; }

        public float CELL_MIN_VLTGE { get; set; }

        public float CELL_MAX_VLTGE { get; set; }

        public float VLTGE { get; set; }

		public float CPCTY { get; set; }

		public float MUMM_VLTGE { get; set; }

		public float MXMM_VLTGE { get; set; }

		public float MUMM_VLTGE_LIMIT { get; set; }

		public float BTRY_ENERGY { get; set; }

		public float COEFF_A { get; set; }

		public float COEFF_C { get; set; }

		public float COEFF_D { get; set; }

		public string CONSIST { get; set; }
	}
}
