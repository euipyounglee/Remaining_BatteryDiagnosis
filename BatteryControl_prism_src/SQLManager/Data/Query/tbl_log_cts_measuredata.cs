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
	public class tbl_log_cts_measuredata : QueryManager
	{

		public tbl_log_cts_measuredata_DTO Get(string runId)
		{
			string query = $"select * from tbl_log_cts_measuredata where run_id = '{runId}' ";
			var result = Get<tbl_log_cts_measuredata_DTO>(query);

			return result.Count > 0 ? result.ElementAt(0) : null;
		}


		public void CleanAndInsert(tbl_log_cts_measuredata_DTO dto)
		{
			Delete(dto.run_id);
			Insert(dto);
		}


		private void Delete(string runId)
		{
			var sb = new StringBuilder();
			sb.Append("delete from tbl_log_cts_measuredata where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();
			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = runId });

			Set(query, parameters);
		}


		private void Insert(tbl_log_cts_measuredata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("insert into tbl_log_cts_measuredata (run_id, ");
			sb.Append("datetime_before_discharge, voltage_before_discharge, current_before_discharge, ");
			sb.Append("datetime_after_discharge, voltage_after_discharge, current_after_discharge, ");
            sb.Append("datetime_before_discharge2, voltage_before_discharge2, current_before_discharge2, ");
            sb.Append("datetime_after_discharge2, voltage_after_discharge2, current_after_discharge2, ");
            sb.Append("datetime_before_charge, voltage_before_charge, current_before_charge, ");
			sb.Append("datetime_after_charge, voltage_after_charge, current_after_charge, ");
            sb.Append("first_dt, voltage_first, current_first, ");
            sb.Append("last_dt, voltage_last, current_last");
			sb.Append(") ");
			sb.Append("values (@run_id, ");
			sb.Append("@datetime_before_discharge, @voltage_before_discharge, @current_before_discharge, ");
			sb.Append("@datetime_after_discharge, @voltage_after_discharge, @current_after_discharge, ");
            sb.Append("@datetime_before_discharge2, @voltage_before_discharge2, @current_before_discharge2, ");
            sb.Append("@datetime_after_discharge2, @voltage_after_discharge2, @current_after_discharge2, ");
            sb.Append("@datetime_before_charge, @voltage_before_charge, @current_before_charge, ");
			sb.Append("@datetime_after_charge, @voltage_after_charge, @current_after_charge, ");
			sb.Append("@first_dt, @voltage_first, @current_first, ");
            sb.Append("@last_dt, @voltage_last, @current_last");
            sb.Append(")");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			
			parameters.Add(new MySqlParameter("@datetime_before_discharge", MySqlDbType.DateTime) { Value = dto.datetime_before_discharge });
			parameters.Add(new MySqlParameter("@voltage_before_discharge", MySqlDbType.Float) { Value = dto.voltage_before_discharge });
			parameters.Add(new MySqlParameter("@current_before_discharge", MySqlDbType.Float) { Value = dto.current_before_discharge });

			parameters.Add(new MySqlParameter("@datetime_after_discharge", MySqlDbType.DateTime) { Value = dto.datetime_before_discharge });
			parameters.Add(new MySqlParameter("@voltage_after_discharge", MySqlDbType.Float) { Value = dto.voltage_after_discharge });
			parameters.Add(new MySqlParameter("@current_after_discharge", MySqlDbType.Float) { Value = dto.current_after_discharge });

            parameters.Add(new MySqlParameter("@datetime_before_discharge2", MySqlDbType.DateTime) { Value = dto.datetime_before_discharge2 });
            parameters.Add(new MySqlParameter("@voltage_before_discharge2", MySqlDbType.Float) { Value = dto.voltage_before_discharge2 });
            parameters.Add(new MySqlParameter("@current_before_discharge2", MySqlDbType.Float) { Value = dto.current_before_discharge2 });

            parameters.Add(new MySqlParameter("@datetime_after_discharge2", MySqlDbType.DateTime) { Value = dto.datetime_before_discharge2 });
            parameters.Add(new MySqlParameter("@voltage_after_discharge2", MySqlDbType.Float) { Value = dto.voltage_after_discharge2 });
            parameters.Add(new MySqlParameter("@current_after_discharge2", MySqlDbType.Float) { Value = dto.current_after_discharge2 });


            parameters.Add(new MySqlParameter("@datetime_before_charge", MySqlDbType.DateTime) { Value = dto.datetime_before_charge });
			parameters.Add(new MySqlParameter("@voltage_before_charge", MySqlDbType.Float) { Value = dto.voltage_before_charge });
			parameters.Add(new MySqlParameter("@current_before_charge", MySqlDbType.Float) { Value = dto.current_before_charge });

			parameters.Add(new MySqlParameter("@datetime_after_charge", MySqlDbType.DateTime) { Value = dto.datetime_after_charge });
			parameters.Add(new MySqlParameter("@voltage_after_charge", MySqlDbType.Float) { Value = dto.voltage_after_charge });
			parameters.Add(new MySqlParameter("@current_after_charge", MySqlDbType.Float) { Value = dto.current_after_charge });

            parameters.Add(new MySqlParameter("@first_dt", MySqlDbType.DateTime) { Value = dto.first_dt });
            parameters.Add(new MySqlParameter("@voltage_first", MySqlDbType.Float) { Value = dto.voltage_first });
            parameters.Add(new MySqlParameter("@current_first", MySqlDbType.Float) { Value = dto.current_first });

            parameters.Add(new MySqlParameter("@last_dt", MySqlDbType.DateTime) { Value = dto.last_dt });
			parameters.Add(new MySqlParameter("@voltage_last", MySqlDbType.Float) { Value = dto.voltage_last });
			parameters.Add(new MySqlParameter("@current_last", MySqlDbType.Float) { Value = dto.current_last });

			Set(query, parameters);
		}

		public void UpdateBeforeDischarge(tbl_log_cts_measuredata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_log_cts_measuredata set ");
			sb.Append("datetime_before_discharge = @datetime_before_discharge, ");
			sb.Append("voltage_before_discharge = @voltage_before_discharge, ");
			sb.Append("current_before_discharge = @current_before_discharge ");
			sb.Append("where run_id = @run_id ");
			
			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@datetime_before_discharge", MySqlDbType.DateTime) { Value = dto.datetime_before_discharge });
			parameters.Add(new MySqlParameter("@voltage_before_discharge", MySqlDbType.Float) { Value = dto.voltage_before_discharge });
			parameters.Add(new MySqlParameter("@current_before_discharge", MySqlDbType.Float) { Value = dto.current_before_discharge });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}

		public void UpdateAfterDischarge(tbl_log_cts_measuredata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_log_cts_measuredata set ");
			sb.Append("datetime_after_discharge = @datetime_after_discharge, ");
			sb.Append("voltage_after_discharge = @voltage_after_discharge, ");
			sb.Append("current_after_discharge = @current_after_discharge ");
			sb.Append("where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@datetime_after_discharge", MySqlDbType.DateTime) { Value = dto.datetime_after_discharge });
			parameters.Add(new MySqlParameter("@voltage_after_discharge", MySqlDbType.Float) { Value = dto.voltage_after_discharge });
			parameters.Add(new MySqlParameter("@current_after_discharge", MySqlDbType.Float) { Value = dto.current_after_discharge });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}


        public void UpdateBeforeDischarge2(tbl_log_cts_measuredata_DTO dto)
        {
            var sb = new StringBuilder();
            sb.Append("update tbl_log_cts_measuredata set ");
            sb.Append("datetime_before_discharge2 = @datetime_before_discharge2, ");
            sb.Append("voltage_before_discharge2 = @voltage_before_discharge2, ");
            sb.Append("current_before_discharge2 = @current_before_discharge2 ");
            sb.Append("where run_id = @run_id ");

            string query = sb.ToString();

            var parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@datetime_before_discharge2", MySqlDbType.DateTime) { Value = dto.datetime_before_discharge2 });
            parameters.Add(new MySqlParameter("@voltage_before_discharge2", MySqlDbType.Float) { Value = dto.voltage_before_discharge2 });
            parameters.Add(new MySqlParameter("@current_before_discharge2", MySqlDbType.Float) { Value = dto.current_before_discharge2 });

            parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

            Set(query, parameters);
        }

        public void UpdateAfterDischarge2(tbl_log_cts_measuredata_DTO dto)
        {
            var sb = new StringBuilder();
            sb.Append("update tbl_log_cts_measuredata set ");
            sb.Append("datetime_after_discharge2 = @datetime_after_discharge2, ");
            sb.Append("voltage_after_discharge2 = @voltage_after_discharge2, ");
            sb.Append("current_after_discharge2 = @current_after_discharge2 ");
            sb.Append("where run_id = @run_id ");

            string query = sb.ToString();

            var parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@datetime_after_discharge2", MySqlDbType.DateTime) { Value = dto.datetime_after_discharge2 });
            parameters.Add(new MySqlParameter("@voltage_after_discharge2", MySqlDbType.Float) { Value = dto.voltage_after_discharge2 });
            parameters.Add(new MySqlParameter("@current_after_discharge2", MySqlDbType.Float) { Value = dto.current_after_discharge2 });

            parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

            Set(query, parameters);
        }

        public void UpdateBeforeCharge(tbl_log_cts_measuredata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_log_cts_measuredata set ");
			sb.Append("datetime_before_charge = @datetime_before_charge, ");
			sb.Append("voltage_before_charge = @voltage_before_charge, ");
			sb.Append("current_before_charge = @current_before_charge ");
			sb.Append("where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@datetime_before_charge", MySqlDbType.DateTime) { Value = dto.datetime_before_charge });
			parameters.Add(new MySqlParameter("@voltage_before_charge", MySqlDbType.Float) { Value = dto.voltage_before_charge });
			parameters.Add(new MySqlParameter("@current_before_charge", MySqlDbType.Float) { Value = dto.current_before_charge });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}


		public void UpdateAfterCharge(tbl_log_cts_measuredata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_log_cts_measuredata set ");
			sb.Append("datetime_after_charge = @datetime_after_charge, ");
			sb.Append("voltage_after_charge = @voltage_after_charge, ");
			sb.Append("current_after_charge = @current_after_charge ");
			sb.Append("where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@datetime_after_charge", MySqlDbType.DateTime) { Value = dto.datetime_after_charge });
			parameters.Add(new MySqlParameter("@voltage_after_charge", MySqlDbType.Float) { Value = dto.voltage_after_charge });
			parameters.Add(new MySqlParameter("@current_after_charge", MySqlDbType.Float) { Value = dto.current_after_charge });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}


		public void UpdateLast(tbl_log_cts_measuredata_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_log_cts_measuredata set ");
			sb.Append("last_dt = @last_dt, ");
			sb.Append("voltage_last = @voltage_last, ");
			sb.Append("current_last = @current_last ");
			sb.Append("where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@last_dt", MySqlDbType.DateTime) { Value = dto.last_dt });
			parameters.Add(new MySqlParameter("@voltage_last", MySqlDbType.Float) { Value = dto.voltage_last });
			parameters.Add(new MySqlParameter("@current_last", MySqlDbType.Float) { Value = dto.current_last });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}


        public void UpdateFirst(tbl_log_cts_measuredata_DTO dto)
        {
            var sb = new StringBuilder();
            sb.Append("update tbl_log_cts_measuredata set ");
            sb.Append("first_dt = @first_dt, ");
            sb.Append("voltage_first = @voltage_first, ");
            sb.Append("current_first = @current_first ");
            sb.Append("where run_id = @run_id ");

            string query = sb.ToString();

            var parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@first_dt", MySqlDbType.DateTime) { Value = dto.first_dt });
            parameters.Add(new MySqlParameter("@voltage_first", MySqlDbType.Float) { Value = dto.voltage_first });
            parameters.Add(new MySqlParameter("@current_first", MySqlDbType.Float) { Value = dto.current_first });

            parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

            Set(query, parameters);
        }
    }
}
