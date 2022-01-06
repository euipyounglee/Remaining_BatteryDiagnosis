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
	public class tbl_task_run_detail : QueryManager
	{
		/// <summary>
		/// get task run detail
		/// </summary>
		/// <param name="runId"></param>
		/// <returns></returns>
		public List<tbl_task_run_detail_DTO> Get(string runId)
		{
			string query = $"select * from tbl_task_run_detail a where a.run_id = '{runId}' ";
			return Get<tbl_task_run_detail_DTO>(query);
		}

		/// <summary>
		/// insert
		/// </summary>
		/// <param name="dto"></param>
		public void Insert(tbl_task_run_detail_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("insert into tbl_task_run_detail (");
			sb.Append("run_id, task_seq, step_no, sub_step_no, start_dt, state, ");
			sb.Append("task_detail_name, device_cd, device_name, task_condition, time_expect, ");
			sb.Append("file_path, disabled, task_group, task_tag ");
			sb.Append(") ");
			sb.Append("values (");
			sb.Append("@run_id, @task_seq, @step_no, @sub_step_no, @start_dt, @state, ");
			sb.Append("@task_detail_name, @device_cd, @device_name, @task_condition, @time_expect, ");
			sb.Append("@file_path, @disabled, @task_group, @task_tag ");
			sb.Append(")");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@task_seq", MySqlDbType.Int32) { Value = dto.task_seq });

            parameters.Add(new MySqlParameter("@step_no", MySqlDbType.Int32) { Value = dto.step_no });
            parameters.Add(new MySqlParameter("@sub_step_no", MySqlDbType.Int32) { Value = dto.sub_step_no });

            parameters.Add(new MySqlParameter("@start_dt", MySqlDbType.DateTime) { Value = dto.start_dt });
			parameters.Add(new MySqlParameter("@state", MySqlDbType.VarString) { Value = dto.state });

			parameters.Add(new MySqlParameter("@task_detail_name", MySqlDbType.VarString) { Value = dto.task_detail_name });
			parameters.Add(new MySqlParameter("@device_cd", MySqlDbType.VarString) { Value = dto.device_cd });
			parameters.Add(new MySqlParameter("@device_name", MySqlDbType.VarString) { Value = dto.device_name });
			parameters.Add(new MySqlParameter("@task_condition", MySqlDbType.VarString) { Value = dto.task_condition });
			parameters.Add(new MySqlParameter("@time_expect", MySqlDbType.Int32) { Value = dto.time_expect });

			parameters.Add(new MySqlParameter("@file_path", MySqlDbType.VarString) { Value = dto.file_path });
			parameters.Add(new MySqlParameter("@disabled", MySqlDbType.VarString) { Value = dto.disabled });
			parameters.Add(new MySqlParameter("@task_group", MySqlDbType.Int32) { Value = dto.task_group });
			parameters.Add(new MySqlParameter("@task_tag", MySqlDbType.VarString) { Value = dto.task_tag });

			Set(query, parameters);
		}

		/// <summary>
		/// update
		/// </summary>
		/// <param name="dto"></param>
		public void Update(tbl_task_run_detail_DTO dto)
		{
			var sb = new StringBuilder();
            sb.Append(" update tbl_task_run_detail ");
            sb.Append(" set end_dt = @end_dt, state = @state ");
			sb.Append(" where run_id = @run_id and task_seq = @task_seq ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@end_dt", MySqlDbType.DateTime) { Value = dto.end_dt });
			parameters.Add(new MySqlParameter("@state", MySqlDbType.VarString) { Value = dto.state });
			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@task_seq", MySqlDbType.Int32) { Value = dto.task_seq });
			
			Set(query, parameters);
		}

		/// <summary>
		/// delete task_run_detail
		/// </summary>
		/// <param name="runId"></param>
		public void Delete(string runId)
		{
			var sb = new StringBuilder();
			sb.Append("delete from tbl_task_run_detail where run_id = @run_id ");
			
			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = runId });
			
			Set(query, parameters);
		}

	}
}
