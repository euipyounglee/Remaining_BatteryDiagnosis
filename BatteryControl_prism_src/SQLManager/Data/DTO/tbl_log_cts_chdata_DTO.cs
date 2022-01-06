using Dibier.mssql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.DTO
{
	public class tbl_log_cts_chdata_DTO : AResultSet
	{
		#region property

		public DateTime log_dt { get; set; }

		public string run_id { get; set; }

		public int task_id { get; set; }

		public int task_seq { get; set; }

		public byte chno { get; set; }

		public byte state { get; set; }

		public byte step_type { get; set; }

		public byte mode { get; set; }

		public byte data_select { get; set; }

		public byte code { get; set; }

		public byte stepno { get; set; }

		public byte grade_code { get; set; }

		public int voltage { get; set; }

		public int current { get; set; }

		public int charge_ah { get; set; }

		public int discharge_ah { get; set; }

		public int capacitance { get; set; }

		public int watt { get; set; }

		public int charge_wh { get; set; }

		public int discharge_wh { get; set; }

		public uint step_day { get; set; }

		public uint step_time { get; set; }

		public uint total_day { get; set; }

		public uint total_time { get; set; }

		public int impedance { get; set; }

		public byte reserved_cmd { get; set; }

		public byte comm_state { get; set; }

		public byte output_state { get; set; }

		public byte input_state { get; set; }

		public short aux_count { get; set; }

		public short can_count { get; set; }

		public uint total_cycle_num { get; set; }

		public uint current_cycle_num { get; set; }

		public uint acc_cycle_group_num1 { get; set; }

		public uint acc_cycle_group_num2 { get; set; }

		public uint acc_cycle_group_num3 { get; set; }

		public uint acc_cycle_group_num4 { get; set; }

		public uint acc_cycle_group_num5 { get; set; }

		public uint multi_cycle_group_num1 { get; set; }

		public uint multi_cycle_group_num2 { get; set; }

		public uint multi_cycle_group_num3 { get; set; }

		public uint multi_cycle_group_num4 { get; set; }

		public uint multi_cycle_group_num5 { get; set; }

		public int ave_voltage { get; set; }

		public int ave_current { get; set; }

		public int save_sequence { get; set; }

		public uint cv_day { get; set; }

		public uint cv_time { get; set; }

		public int sync_time1 { get; set; }
		public int sync_time2 { get; set; }

		public int voltage_input { get; set; }

		public int voltage_power { get; set; }

		public int voltage_bus { get; set; }

		public byte using_chamber { get; set; }

		public byte record_time_no { get; set; }

		public byte out_mux_use { get; set; }

		public byte out_mux_backup { get; set; }

		#endregion
	}
}
