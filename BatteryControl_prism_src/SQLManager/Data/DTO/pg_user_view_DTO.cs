using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class pg_user_view_DTO : AResultSet
	{
		public string USER_ID { get; set; }

		public string USER_NM { get; set; }

		public string USER_PW { get; set; }

		public string EMAIL { get; set; }

		public string TELNO { get; set; }

		public string ROLE { get; set; }

		public string EVAL_QUICK_AT { get; set; }

		public string EVAL_STD_AT { get; set; }

		public string EVAL_DETAIL_AT { get; set; }

		public string SAFE_AT { get; set; }

		public string OCV_AT { get; set; }
	}
}
