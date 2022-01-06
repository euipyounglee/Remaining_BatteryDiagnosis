using MySql.Data.MySqlClient;
using SQLManager.Data.DTO;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.Query
{
	public class tbl_log_cts_chdata : QueryManager
	{
		public List<tbl_log_cts_chdata_DTO> Get(string runId, int taskSeq)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("select * from tbl_log_cts_chdata a ");
			sb.Append($"where a.run_id = '{runId}' and a.task_seq = {taskSeq} ");
			sb.Append("order by a.log_dt asc ");

			return Get<tbl_log_cts_chdata_DTO>(sb.ToString());
		}

        public tbl_log_cts_chdata_DTO GetLastData(string runId, int task_seq)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from tbl_log_cts_chdata b ");
            sb.Append("where b.log_dt = ( ");
            sb.Append("select max(a.log_dt) from tbl_log_cts_chdata a ");
            sb.Append($"where a.run_id = '{runId}' and a.task_seq = task_seq ");
            sb.Append(") ");

            var result = Get<tbl_log_cts_chdata_DTO>(sb.ToString());
            return result.Count > 0 ? result.ElementAt(0) : null;
        }

        public tbl_log_cts_chdata_DTO GetLastData(string runId, string btry_code, int task_id, int step_no)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("select * from tbl_log_cts_chdata b ");
			sb.Append("where b.log_dt = ( ");
			sb.Append("select max(a.log_dt) from tbl_log_cts_chdata a ");
			sb.Append($"where a.run_id = '{runId}' and a.task_seq = ( ");
            sb.Append("       select task_seq from tbl_task_detail ");
            sb.Append($"       where task_id = {task_id} and step_no = {step_no} and BTRY_CODE = '{btry_code}' ");
            sb.Append("       ) ");
			sb.Append(") ");

			var result = Get<tbl_log_cts_chdata_DTO>(sb.ToString());
			return result.Count > 0 ? result.ElementAt(0) : null;
		}

		public float GetCurrentAfterDischarge(string runId, int taskSeq)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("select * from tbl_log_cts_chdata a ");
			sb.Append($"where a.run_id = '{runId}' and a.task_seq = {taskSeq} ");
			sb.Append("order by a.log_dt desc limit 1 ");

			var result = Get<tbl_log_cts_chdata_DTO>(sb.ToString());
			return result.Count > 0 ? result.ElementAt(0).current / 1000000.0f : 0f;
		}

		/// <summary>
		/// insert
		/// </summary>
		/// <param name="dto"></param>
		public void Insert(tbl_log_cts_chdata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("insert into tbl_log_cts_chdata (");
			sb.Append("log_dt, chno, state, step_type, mode, ");
			sb.Append("data_select, code, stepno, grade_code, voltage, ");
			sb.Append("current, charge_ah, discharge_ah, capacitance, watt, ");
			sb.Append("charge_wh, discharge_wh, step_day, step_time, total_day, ");
			sb.Append("total_time, impedance, reserved_cmd, comm_state, output_state, ");
			sb.Append("input_state, aux_count, can_count, total_cycle_num, current_cycle_num, ");
			sb.Append("acc_cycle_group_num1, acc_cycle_group_num2, acc_cycle_group_num3, acc_cycle_group_num4, acc_cycle_group_num5, ");
			sb.Append("multi_cycle_group_num1, multi_cycle_group_num2, multi_cycle_group_num3, multi_cycle_group_num4, multi_cycle_group_num5, ");
			sb.Append("ave_voltage, ave_current, save_sequence, cv_day, cv_time, ");
			sb.Append("sync_time1, sync_time2, voltage_input, voltage_power, voltage_bus, ");
			sb.Append("using_chamber, record_time_no, out_mux_use, out_mux_backup, ");
			sb.Append("run_id, task_id, task_seq ");
			sb.Append(") ");

			sb.Append("values (NOW(6), @chno, @state, @step_type, @mode, ");
			sb.Append("@data_select, @code, @stepno, @grade_code, @voltage, ");
			sb.Append("@current, @charge_ah, @discharge_ah, @capacitance, @watt, ");
			sb.Append("@charge_wh, @discharge_wh, @step_day, @step_time, @total_day, ");
			sb.Append("@total_time, @impedance, @reserved_cmd, @comm_state, @output_state, ");
			sb.Append("@input_state, @aux_count, @can_count, @total_cycle_num, @current_cycle_num, ");
			sb.Append("@acc_cycle_group_num1, @acc_cycle_group_num2, @acc_cycle_group_num3, @acc_cycle_group_num4, @acc_cycle_group_num5, ");
			sb.Append("@multi_cycle_group_num1, @multi_cycle_group_num2, @multi_cycle_group_num3, @multi_cycle_group_num4, @multi_cycle_group_num5, ");
			sb.Append("@ave_voltage, @ave_current, @save_sequence, @cv_day, @cv_time, ");
			sb.Append("@sync_time1, @sync_time2, @voltage_input, @voltage_power, @voltage_bus, ");
			sb.Append("@using_chamber, @record_time_no, @out_mux_use, @out_mux_backup, ");
			sb.Append("@run_id, @task_id, @task_seq ");
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@chno", MySqlDbType.UByte) { Value = dto.chno });
			parameters.Add(new MySqlParameter("@state", MySqlDbType.UByte) { Value = dto.state });
			parameters.Add(new MySqlParameter("@step_type", MySqlDbType.UByte) { Value = dto.step_type });
			parameters.Add(new MySqlParameter("@mode", MySqlDbType.UByte) { Value = dto.mode });

			parameters.Add(new MySqlParameter("@data_select", MySqlDbType.UByte) { Value = dto.data_select });
			parameters.Add(new MySqlParameter("@code", MySqlDbType.UByte) { Value = dto.code });
			parameters.Add(new MySqlParameter("@stepno", MySqlDbType.UByte) { Value = dto.stepno });
			parameters.Add(new MySqlParameter("@grade_code", MySqlDbType.UByte) { Value = dto.grade_code });
			parameters.Add(new MySqlParameter("@voltage", MySqlDbType.Int32) { Value = dto.voltage });

			parameters.Add(new MySqlParameter("@current", MySqlDbType.Int32) { Value = dto.current });
			parameters.Add(new MySqlParameter("@charge_ah", MySqlDbType.Int32) { Value = dto.charge_ah });
			parameters.Add(new MySqlParameter("@discharge_ah", MySqlDbType.Int32) { Value = dto.discharge_ah });
			parameters.Add(new MySqlParameter("@capacitance", MySqlDbType.Int32) { Value = dto.capacitance });
			parameters.Add(new MySqlParameter("@watt", MySqlDbType.Int32) { Value = dto.watt });

			parameters.Add(new MySqlParameter("@charge_wh", MySqlDbType.Int32) { Value = dto.charge_wh });
			parameters.Add(new MySqlParameter("@discharge_wh", MySqlDbType.Int32) { Value = dto.discharge_wh });
			parameters.Add(new MySqlParameter("@step_day", MySqlDbType.Int32) { Value = dto.step_day });
			parameters.Add(new MySqlParameter("@step_time", MySqlDbType.Int32) { Value = dto.step_time });
			parameters.Add(new MySqlParameter("@total_day", MySqlDbType.Int32) { Value = dto.total_day });

			parameters.Add(new MySqlParameter("@total_time", MySqlDbType.Int32) { Value = dto.total_time });
			parameters.Add(new MySqlParameter("@impedance", MySqlDbType.Int32) { Value = dto.impedance });
			parameters.Add(new MySqlParameter("@reserved_cmd", MySqlDbType.UByte) { Value = dto.reserved_cmd });
			parameters.Add(new MySqlParameter("@comm_state", MySqlDbType.UByte) { Value = dto.comm_state });
			parameters.Add(new MySqlParameter("@output_state", MySqlDbType.UByte) { Value = dto.output_state });

			parameters.Add(new MySqlParameter("@input_state", MySqlDbType.UByte) { Value = dto.input_state });
			parameters.Add(new MySqlParameter("@aux_count", MySqlDbType.Int16) { Value = dto.aux_count });
			parameters.Add(new MySqlParameter("@can_count", MySqlDbType.Int16) { Value = dto.can_count });
			parameters.Add(new MySqlParameter("@total_cycle_num", MySqlDbType.Int32) { Value = dto.total_cycle_num });
			parameters.Add(new MySqlParameter("@current_cycle_num", MySqlDbType.Int32) { Value = dto.current_cycle_num });

			parameters.Add(new MySqlParameter("@acc_cycle_group_num1", MySqlDbType.Int32) { Value = dto.acc_cycle_group_num1 });
			parameters.Add(new MySqlParameter("@acc_cycle_group_num2", MySqlDbType.Int32) { Value = dto.acc_cycle_group_num2 });
			parameters.Add(new MySqlParameter("@acc_cycle_group_num3", MySqlDbType.Int32) { Value = dto.acc_cycle_group_num3 });
			parameters.Add(new MySqlParameter("@acc_cycle_group_num4", MySqlDbType.Int32) { Value = dto.acc_cycle_group_num4 });
			parameters.Add(new MySqlParameter("@acc_cycle_group_num5", MySqlDbType.Int32) { Value = dto.acc_cycle_group_num5 });

			parameters.Add(new MySqlParameter("@multi_cycle_group_num1", MySqlDbType.Int32) { Value = dto.multi_cycle_group_num1 });
			parameters.Add(new MySqlParameter("@multi_cycle_group_num2", MySqlDbType.Int32) { Value = dto.multi_cycle_group_num2 });
			parameters.Add(new MySqlParameter("@multi_cycle_group_num3", MySqlDbType.Int32) { Value = dto.multi_cycle_group_num3 });
			parameters.Add(new MySqlParameter("@multi_cycle_group_num4", MySqlDbType.Int32) { Value = dto.multi_cycle_group_num4 });
			parameters.Add(new MySqlParameter("@multi_cycle_group_num5", MySqlDbType.Int32) { Value = dto.multi_cycle_group_num5 });

			parameters.Add(new MySqlParameter("@ave_voltage", MySqlDbType.Int32) { Value = dto.ave_voltage });
			parameters.Add(new MySqlParameter("@ave_current", MySqlDbType.Int32) { Value = dto.ave_current });
			parameters.Add(new MySqlParameter("@save_sequence", MySqlDbType.Int32) { Value = dto.save_sequence });
			parameters.Add(new MySqlParameter("@cv_day", MySqlDbType.Int32) { Value = dto.cv_day });
			parameters.Add(new MySqlParameter("@cv_time", MySqlDbType.Int32) { Value = dto.cv_time });

			parameters.Add(new MySqlParameter("@sync_time1", MySqlDbType.Int32) { Value = dto.sync_time1 });
			parameters.Add(new MySqlParameter("@sync_time2", MySqlDbType.Int32) { Value = dto.sync_time2 });
			parameters.Add(new MySqlParameter("@voltage_input", MySqlDbType.Int32) { Value = dto.voltage_input });
			parameters.Add(new MySqlParameter("@voltage_power", MySqlDbType.Int32) { Value = dto.voltage_power });
			parameters.Add(new MySqlParameter("@voltage_bus", MySqlDbType.Int32) { Value = dto.voltage_bus });

			parameters.Add(new MySqlParameter("@using_chamber", MySqlDbType.UByte) { Value = dto.using_chamber });
			parameters.Add(new MySqlParameter("@record_time_no", MySqlDbType.UByte) { Value = dto.record_time_no });
			parameters.Add(new MySqlParameter("@out_mux_use", MySqlDbType.UByte) { Value = dto.out_mux_use });
			parameters.Add(new MySqlParameter("@out_mux_backup", MySqlDbType.UByte) { Value = dto.out_mux_backup });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@task_id", MySqlDbType.Int32) { Value = dto.task_id });
			parameters.Add(new MySqlParameter("@task_seq", MySqlDbType.Int32) { Value = dto.task_seq });

			Set(query, parameters);
		}

	}
}
