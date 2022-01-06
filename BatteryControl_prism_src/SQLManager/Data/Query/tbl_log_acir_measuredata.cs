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
	public class tbl_log_acir_measuredata : QueryManager
	{
		public List<tbl_log_acir_measuredata_DTO> Get(string runId, int taskSeq)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"select * from tbl_log_acir_measuredata a where a.run_id = '{runId}' and a.task_seq = {taskSeq} ");
			sb.Append("order by a.hz desc ");

			return Get<tbl_log_acir_measuredata_DTO>(sb.ToString());
		}

		public void Insert(tbl_log_acir_measuredata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("insert into tbl_log_acir_measuredata (");
			sb.Append("log_dt, hostname, port, type, value, step_no, current_no, ");
			sb.Append("total_no, mode, voltage, temperature, hz, ");
			sb.Append("re, im, reserved, reserved2, ");
			sb.Append("run_id, task_id, task_seq ");
			sb.Append(")");

			sb.Append("values (");
			sb.Append("@log_dt, @hostname, @port, @type, @value, @step_no, @current_no, ");
			sb.Append("@total_no, @mode, @voltage, @temperature, @hz, ");
			sb.Append("@re, @im, @reserved, @reserved2, ");
			sb.Append("@run_id, @task_id, @task_seq ");
			sb.Append(")");

			string query = sb.ToString();
			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@log_dt", MySqlDbType.DateTime) { Value = dto.log_dt });
			parameters.Add(new MySqlParameter("@hostname", MySqlDbType.VarString) { Value = dto.hostname });
			parameters.Add(new MySqlParameter("@port", MySqlDbType.UInt16) { Value = dto.port });
			parameters.Add(new MySqlParameter("@type", MySqlDbType.UByte) { Value = dto.type });
			parameters.Add(new MySqlParameter("@value", MySqlDbType.UByte) { Value = dto.value });
			parameters.Add(new MySqlParameter("@step_no", MySqlDbType.UByte) { Value = dto.step_no });
			parameters.Add(new MySqlParameter("@current_no", MySqlDbType.UByte) { Value = dto.current_no });

			parameters.Add(new MySqlParameter("@total_no", MySqlDbType.UByte) { Value = dto.total_no });
			parameters.Add(new MySqlParameter("@mode", MySqlDbType.UByte) { Value = dto.mode });
			parameters.Add(new MySqlParameter("@voltage", MySqlDbType.Float) { Value = dto.voltage });
			parameters.Add(new MySqlParameter("@temperature", MySqlDbType.Float) { Value = dto.temperature });
			parameters.Add(new MySqlParameter("@hz", MySqlDbType.Float) { Value = dto.hz });

			parameters.Add(new MySqlParameter("@re", MySqlDbType.Float) { Value = dto.re });
			parameters.Add(new MySqlParameter("@im", MySqlDbType.Float) { Value = dto.im });
			parameters.Add(new MySqlParameter("@reserved", MySqlDbType.UInt32) { Value = dto.reserved });
			parameters.Add(new MySqlParameter("@reserved2", MySqlDbType.UInt32) { Value = dto.reserved2 });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@task_id", MySqlDbType.Int32) { Value = dto.task_id });
			parameters.Add(new MySqlParameter("@task_seq", MySqlDbType.Int32) { Value = dto.task_seq });

			Set(query, parameters);
		}
	}
}
