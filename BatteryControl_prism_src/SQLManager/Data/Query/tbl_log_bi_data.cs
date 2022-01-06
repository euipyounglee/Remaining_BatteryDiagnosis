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
	public class tbl_log_bi_data : QueryManager
	{
		/// <summary>
		/// get
		/// </summary>
		/// <param name="runId"></param>
		/// <param name="taskId"></param>
		/// <param name="taskSeq"></param>
		/// <param name="mbmsId"></param>
		/// <returns></returns>
		public tbl_log_bi_data_DTO Get(string runId, int taskId, int taskSeq, byte mbmsId)
		{
			var sb = new StringBuilder();
			sb.Append("select * from tbl_log_bi_data ");
			sb.Append($"where run_id = '{runId}' and  task_id = {taskId} and task_seq = {taskSeq} and mbms_id = {mbmsId} ");

			string query = sb.ToString();

			var result = Get<tbl_log_bi_data_DTO>(query);
			return result.Count == 0 ? null : result.ElementAt(0);
		}

		/// <summary>
		/// get logs
		/// </summary>
		/// <param name="runId"></param>
		/// <param name="taskSeq"></param>
		/// <returns></returns>
		public List<tbl_log_bi_data_DTO> Get(string runId, int taskSeq)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append($"select * from tbl_log_bi_data a where a.run_id = '{runId}' and a.task_seq = {taskSeq} ");
			sb.Append("order by a.mbms_id ");

			return Get<tbl_log_bi_data_DTO>(sb.ToString());
		}

		/// <summary>
		/// insert
		/// </summary>
		/// <param name="dto"></param>
		public bool Insert(tbl_log_bi_data_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("insert into tbl_log_bi_data (");
			sb.Append("log_dt, run_id, task_id, task_seq, mbms_id, ");
			sb.Append("can_id, voltage1, voltage2, voltage3, voltage4, ");
			sb.Append("voltage5, voltage6, voltage7, voltage8, voltage9, ");
			sb.Append("voltage10, voltage11, voltage12, voltage13, voltage14, ");
			sb.Append("voltage15, temperature1, temperature2 ");
			sb.Append(") ");
			sb.Append("values (");
			sb.Append("@log_dt, @run_id, @task_id, @task_seq, @mbms_id, ");
			sb.Append("@can_id, @voltage1, @voltage2, @voltage3, @voltage4, ");
			sb.Append("@voltage5, @voltage6, @voltage7, @voltage8, @voltage9, ");
			sb.Append("@voltage10, @voltage11, @voltage12, @voltage13, @voltage14, ");
			sb.Append("@voltage15, @temperature1, @temperature2 ");
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@log_dt", MySqlDbType.DateTime) { Value = dto.log_dt });
			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@task_id", MySqlDbType.Int32) { Value = dto.task_id });
			parameters.Add(new MySqlParameter("@task_seq", MySqlDbType.Int32) { Value = dto.task_seq });
			parameters.Add(new MySqlParameter("@mbms_id", MySqlDbType.Byte) { Value = dto.mbms_id });

			parameters.Add(new MySqlParameter("@can_id", MySqlDbType.UInt32) { Value = dto.can_id });
			parameters.Add(new MySqlParameter("@voltage1", MySqlDbType.Int16) { Value = dto.voltage1 });
			parameters.Add(new MySqlParameter("@voltage2", MySqlDbType.Int16) { Value = dto.voltage2 });
			parameters.Add(new MySqlParameter("@voltage3", MySqlDbType.Int16) { Value = dto.voltage3 });
			parameters.Add(new MySqlParameter("@voltage4", MySqlDbType.Int16) { Value = dto.voltage4 });

			parameters.Add(new MySqlParameter("@voltage5", MySqlDbType.Int16) { Value = dto.voltage5 });
			parameters.Add(new MySqlParameter("@voltage6", MySqlDbType.Int16) { Value = dto.voltage6 });
			parameters.Add(new MySqlParameter("@voltage7", MySqlDbType.Int16) { Value = dto.voltage7 });
			parameters.Add(new MySqlParameter("@voltage8", MySqlDbType.Int16) { Value = dto.voltage8 });
			parameters.Add(new MySqlParameter("@voltage9", MySqlDbType.Int16) { Value = dto.voltage9 });

			parameters.Add(new MySqlParameter("@voltage10", MySqlDbType.Int16) { Value = dto.voltage10 });
			parameters.Add(new MySqlParameter("@voltage11", MySqlDbType.Int16) { Value = dto.voltage11 });
			parameters.Add(new MySqlParameter("@voltage12", MySqlDbType.Int16) { Value = dto.voltage12 });
			parameters.Add(new MySqlParameter("@voltage13", MySqlDbType.Int16) { Value = dto.voltage13 });
			parameters.Add(new MySqlParameter("@voltage14", MySqlDbType.Int16) { Value = dto.voltage14 });

			parameters.Add(new MySqlParameter("@voltage15", MySqlDbType.Int16) { Value = dto.voltage15 });
			parameters.Add(new MySqlParameter("@temperature1", MySqlDbType.Int16) { Value = dto.temperature1 });
			parameters.Add(new MySqlParameter("@temperature2", MySqlDbType.Int16) { Value = dto.temperature2 });

            try
            {
                Set(query, parameters);
                return true;
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
            return false;
		}

		/// <summary>
		/// update
		/// </summary>
		/// <param name="dto"></param>
		public void Update(tbl_log_bi_data_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_log_bi_data set ");
			sb.Append("voltage1 = @voltage1, voltage2 = @voltage2, voltage3 = @voltage3, ");
			sb.Append("voltage4 = @voltage4, voltage5 = @voltage5, voltage6 = @voltage6, ");
			sb.Append("voltage7 = @voltage7, voltage8 = @voltage8, voltage9 = @voltage9, ");
			sb.Append("voltage10 = @voltage10, voltage11 = @voltage11, voltage12 = @voltage12, ");
			sb.Append("voltage13 = @voltage13, voltage14 = @voltage14, voltage15 = @voltage15, ");
			sb.Append("temperature1 = @temperature1, temperature2 = @temperature2 ");
			sb.Append("where run_id = @run_id and task_id = @task_id and task_seq = @task_seq and mbms_id = @mbms_id ");
			
			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@voltage1", MySqlDbType.Int16) { Value = dto.voltage1 });
			parameters.Add(new MySqlParameter("@voltage2", MySqlDbType.Int16) { Value = dto.voltage2 });
			parameters.Add(new MySqlParameter("@voltage3", MySqlDbType.Int16) { Value = dto.voltage3 });
			parameters.Add(new MySqlParameter("@voltage4", MySqlDbType.Int16) { Value = dto.voltage4 });

			parameters.Add(new MySqlParameter("@voltage5", MySqlDbType.Int16) { Value = dto.voltage5 });
			parameters.Add(new MySqlParameter("@voltage6", MySqlDbType.Int16) { Value = dto.voltage6 });
			parameters.Add(new MySqlParameter("@voltage7", MySqlDbType.Int16) { Value = dto.voltage7 });
			parameters.Add(new MySqlParameter("@voltage8", MySqlDbType.Int16) { Value = dto.voltage8 });
			parameters.Add(new MySqlParameter("@voltage9", MySqlDbType.Int16) { Value = dto.voltage9 });

			parameters.Add(new MySqlParameter("@voltage10", MySqlDbType.Int16) { Value = dto.voltage10 });
			parameters.Add(new MySqlParameter("@voltage11", MySqlDbType.Int16) { Value = dto.voltage11 });
			parameters.Add(new MySqlParameter("@voltage12", MySqlDbType.Int16) { Value = dto.voltage12 });
			parameters.Add(new MySqlParameter("@voltage13", MySqlDbType.Int16) { Value = dto.voltage13 });
			parameters.Add(new MySqlParameter("@voltage14", MySqlDbType.Int16) { Value = dto.voltage14 });

			parameters.Add(new MySqlParameter("@voltage15", MySqlDbType.Int16) { Value = dto.voltage15 });
			parameters.Add(new MySqlParameter("@temperature1", MySqlDbType.Int16) { Value = dto.temperature1 });
			parameters.Add(new MySqlParameter("@temperature2", MySqlDbType.Int16) { Value = dto.temperature2 });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@task_id", MySqlDbType.Int32) { Value = dto.task_id });
			parameters.Add(new MySqlParameter("@task_seq", MySqlDbType.Int32) { Value = dto.task_seq });
			parameters.Add(new MySqlParameter("@mbms_id", MySqlDbType.Byte) { Value = dto.mbms_id });

			Set(query, parameters);
		}

        public void UpdateTemperature(tbl_log_bi_data_DTO dto)
        {
            var sb = new StringBuilder();
            sb.Append("update tbl_log_bi_data set ");
            sb.Append("temperature1 = @temperature1, temperature2 = @temperature2 ");
            sb.Append("where run_id = @run_id and task_id = @task_id and task_seq = @task_seq and mbms_id = @mbms_id ");

            string query = sb.ToString();

            var parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@temperature1", MySqlDbType.Int16) { Value = dto.temperature1 });
            parameters.Add(new MySqlParameter("@temperature2", MySqlDbType.Int16) { Value = dto.temperature2 });

            parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
            parameters.Add(new MySqlParameter("@task_id", MySqlDbType.Int32) { Value = dto.task_id });
            parameters.Add(new MySqlParameter("@task_seq", MySqlDbType.Int32) { Value = dto.task_seq });
            parameters.Add(new MySqlParameter("@mbms_id", MySqlDbType.Byte) { Value = dto.mbms_id });

            Set(query, parameters);
        }


    }
}
