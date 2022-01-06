using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class pg_btry_srvive_evl_input_DTO : AResultSet
	{
		public int INPUT_SN { get; set; }

		public string STEP_NO { get; set; }

		public string CHANNEL { get; set; }

		public string INPUT_VALUE { get; set; }

		public string INPUT_DT { get; set; }

		public string LOGIN_ID { get; set; }

		public pg_btry_srvive_evl_input_DTO()
		{
			INPUT_SN = 0;
			STEP_NO = "";
			CHANNEL = "";
			INPUT_VALUE = "";
			INPUT_DT = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}",
				DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
				DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
			LOGIN_ID = "";
		}
	}
}
