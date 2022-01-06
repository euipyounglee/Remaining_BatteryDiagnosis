using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_task_run_DTO : AResultSet
	{
		#region property

		public string run_id { get; set; }

		public int INSPCT_SN { get; set; }

		public byte channel { get; set; }

		public string step { get; set; }

		public DateTime evaluation_dt { get; set; }

		public DateTime completed_dt { get; set; }

		public string barcode { get; set; }

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

		public int task_id { get; set; }

		public string task_name { get; set; }

		public string task_type { get; set; }

		#endregion

		#region constructor

		public tbl_task_run_DTO()
		{
			run_id = "";
			INSPCT_SN = 0;
			channel = 0;
			step = "";
			evaluation_dt = DateTime.Now;

			BTRY_CODE = "";
			BTRY_TY = "";
			MARK_CODE = "";
			MAKR_DESC = "";
			MODEL_CODE = "";
			MODL_DESC = "";
			CONFIG = "";
			TYPE_P = 0;
			TYPE_S = 0;
			VLTGE = 0;
			CPCTY = 0;
			MUMM_VLTGE = 0;
			MXMM_VLTGE = 0;
			MUMM_VLTGE_LIMIT = 0;
			BTRY_ENERGY = 0;
			barcode = "";

			task_id = 0;
			task_name = "";
			task_type = "";
		}

		#endregion

	}
}
