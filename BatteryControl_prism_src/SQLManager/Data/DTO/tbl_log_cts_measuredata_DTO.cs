using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_log_cts_measuredata_DTO : AResultSet
	{
		public string run_id { get; set; }

		public DateTime datetime_before_discharge { get; set; }
		public float voltage_before_discharge { get; set; }
		public float current_before_discharge { get; set; }

		public DateTime datetime_after_discharge { get; set; }
		public float voltage_after_discharge { get; set; }
		public float current_after_discharge { get; set; }

        public DateTime datetime_before_discharge2 { get; set; }
        public float voltage_before_discharge2 { get; set; }
        public float current_before_discharge2 { get; set; }

        public DateTime datetime_after_discharge2 { get; set; }
        public float voltage_after_discharge2 { get; set; }
        public float current_after_discharge2 { get; set; }

        public DateTime datetime_before_charge { get; set; }
		public float voltage_before_charge { get; set; }
		public float current_before_charge { get; set; }

		public DateTime datetime_after_charge { get; set; }
		public float voltage_after_charge { get; set; }
		public float current_after_charge { get; set; }

        public DateTime first_dt { get; set; }
        public float voltage_first { get; set; }
        public float current_first { get; set; }

        public DateTime last_dt { get; set; }
		public float voltage_last { get; set; }
		public float current_last { get; set; }
	}
}
