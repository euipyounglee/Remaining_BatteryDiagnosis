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
	public class tbl_task_run : QueryManager
	{
		public List<tbl_task_run_DTO> GetAll()
		{
			string query = "select a.run_id, a.INSPCT_SN from tbl_task_run a order by a.run_id asc ";
			return Get<tbl_task_run_DTO>(query);
		}

		public tbl_task_run_DTO Get(string runId)
		{
			string query = $"select * from tbl_task_run where run_id = '{runId}' ";
			var result = Get<tbl_task_run_DTO>(query);

			return result.Count > 0 ? result.ElementAt(0) : null;
		}

		public void Insert(tbl_task_run_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("insert into tbl_task_run (");
			sb.Append("run_id, channel, step, evaluation_dt, completed_dt, ");
			sb.Append("BTRY_CODE, BTRY_TY, MARK_CODE, MAKR_DESC, MODEL_CODE, ");
			sb.Append("MODL_DESC, CONFIG, TYPE_P, TYPE_S, VLTGE, ");
			sb.Append("CPCTY, MUMM_VLTGE, MXMM_VLTGE, MUMM_VLTGE_LIMIT, BTRY_ENERGY, ");
			sb.Append("task_id, task_name, task_type, barcode, INSPCT_SN ");
			sb.Append(") ");
			sb.Append("values (");
			sb.Append("@run_id, @channel, @step, @evaluation_dt, @completed_dt, ");
			sb.Append("@BTRY_CODE, @BTRY_TY, @MARK_CODE, @MAKR_DESC, @MODEL_CODE, ");
			sb.Append("@MODL_DESC, @CONFIG, @TYPE_P, @TYPE_S, @VLTGE, ");
			sb.Append("@CPCTY, @MUMM_VLTGE, @MXMM_VLTGE, @MUMM_VLTGE_LIMIT, @BTRY_ENERGY, ");
			sb.Append("@task_id, @task_name, @task_type, @barcode, @INSPCT_SN ");
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@channel", MySqlDbType.Byte) { Value = dto.channel });
			parameters.Add(new MySqlParameter("@step", MySqlDbType.VarString) { Value = dto.step });
			parameters.Add(new MySqlParameter("@evaluation_dt", MySqlDbType.DateTime) { Value = dto.evaluation_dt });
			parameters.Add(new MySqlParameter("@completed_dt", MySqlDbType.DateTime) { Value = dto.completed_dt });

			parameters.Add(new MySqlParameter("@BTRY_CODE", MySqlDbType.VarString) { Value = dto.BTRY_CODE });
			parameters.Add(new MySqlParameter("@BTRY_TY", MySqlDbType.VarString) { Value = dto.BTRY_TY });
			parameters.Add(new MySqlParameter("@MARK_CODE", MySqlDbType.VarString) { Value = dto.MARK_CODE });
			parameters.Add(new MySqlParameter("@MAKR_DESC", MySqlDbType.VarString) { Value = dto.MAKR_DESC });
			parameters.Add(new MySqlParameter("@MODEL_CODE", MySqlDbType.VarString) { Value = dto.MODEL_CODE });

			parameters.Add(new MySqlParameter("@MODL_DESC", MySqlDbType.VarString) { Value = dto.MODL_DESC });
			parameters.Add(new MySqlParameter("@CONFIG", MySqlDbType.VarString) { Value = dto.CONFIG });
			parameters.Add(new MySqlParameter("@TYPE_P", MySqlDbType.Int32) { Value = dto.TYPE_P });
			parameters.Add(new MySqlParameter("@TYPE_S", MySqlDbType.Int32) { Value = dto.TYPE_S });
			parameters.Add(new MySqlParameter("@VLTGE", MySqlDbType.Float) { Value = dto.VLTGE });

			parameters.Add(new MySqlParameter("@CPCTY", MySqlDbType.Float) { Value = dto.CPCTY });
			parameters.Add(new MySqlParameter("@MUMM_VLTGE", MySqlDbType.Float) { Value = dto.MUMM_VLTGE });
			parameters.Add(new MySqlParameter("@MXMM_VLTGE", MySqlDbType.Float) { Value = dto.MXMM_VLTGE });
			parameters.Add(new MySqlParameter("@MUMM_VLTGE_LIMIT", MySqlDbType.Float) { Value = dto.MUMM_VLTGE_LIMIT });
			parameters.Add(new MySqlParameter("@BTRY_ENERGY", MySqlDbType.Float) { Value = dto.BTRY_ENERGY });

			parameters.Add(new MySqlParameter("@task_id", MySqlDbType.Int32) { Value = dto.task_id });
			parameters.Add(new MySqlParameter("@task_name", MySqlDbType.VarString) { Value = dto.task_name });
			parameters.Add(new MySqlParameter("@task_type", MySqlDbType.VarString) { Value = dto.task_type });
			parameters.Add(new MySqlParameter("@barcode", MySqlDbType.VarString) { Value = dto.barcode });
			parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = dto.INSPCT_SN });

			Set(query, parameters);
		}

		public void UpdateBatteryInfo(tbl_task_run_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_task_run set ");
			sb.Append("BTRY_CODE = @BTRY_CODE, ");
			sb.Append("BTRY_TY = @BTRY_TY, ");
			sb.Append("MARK_CODE = @MARK_CODE, ");
			sb.Append("MAKR_DESC = @MAKR_DESC, ");
			sb.Append("MODEL_CODE = @MODEL_CODE, ");
			sb.Append("MODL_DESC = @MODL_DESC, ");
			sb.Append("CONFIG = @CONFIG, ");
			sb.Append("TYPE_P = @TYPE_P, ");
			sb.Append("TYPE_S = @TYPE_S, ");
			sb.Append("VLTGE = @VLTGE, ");
			sb.Append("CPCTY = @CPCTY, ");
			sb.Append("MUMM_VLTGE = @MUMM_VLTGE, ");
			sb.Append("MXMM_VLTGE = @MXMM_VLTGE, ");
			sb.Append("MUMM_VLTGE_LIMIT = @MUMM_VLTGE_LIMIT, ");
			sb.Append("BTRY_ENERGY = @BTRY_ENERGY, ");
			sb.Append("barcode = @barcode ");
			sb.Append("where run_id = @run_id ");
			
			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@BTRY_CODE", MySqlDbType.VarString) { Value = dto.BTRY_CODE });
			parameters.Add(new MySqlParameter("@BTRY_TY", MySqlDbType.VarString) { Value = dto.BTRY_TY });
			parameters.Add(new MySqlParameter("@MARK_CODE", MySqlDbType.VarString) { Value = dto.MARK_CODE });
			parameters.Add(new MySqlParameter("@MAKR_DESC", MySqlDbType.VarString) { Value = dto.MAKR_DESC });
			parameters.Add(new MySqlParameter("@MODEL_CODE", MySqlDbType.VarString) { Value = dto.MODEL_CODE });

			parameters.Add(new MySqlParameter("@MODL_DESC", MySqlDbType.VarString) { Value = dto.MODL_DESC });
			parameters.Add(new MySqlParameter("@CONFIG", MySqlDbType.VarString) { Value = dto.CONFIG });
			parameters.Add(new MySqlParameter("@TYPE_P", MySqlDbType.Int32) { Value = dto.TYPE_P });
			parameters.Add(new MySqlParameter("@TYPE_S", MySqlDbType.Int32) { Value = dto.TYPE_S });
			parameters.Add(new MySqlParameter("@VLTGE", MySqlDbType.Float) { Value = dto.VLTGE });

			parameters.Add(new MySqlParameter("@CPCTY", MySqlDbType.Float) { Value = dto.CPCTY });
			parameters.Add(new MySqlParameter("@MUMM_VLTGE", MySqlDbType.Float) { Value = dto.MUMM_VLTGE });
			parameters.Add(new MySqlParameter("@MXMM_VLTGE", MySqlDbType.Float) { Value = dto.MXMM_VLTGE });
			parameters.Add(new MySqlParameter("@MUMM_VLTGE_LIMIT", MySqlDbType.Float) { Value = dto.MUMM_VLTGE_LIMIT });
			parameters.Add(new MySqlParameter("@BTRY_ENERGY", MySqlDbType.Float) { Value = dto.BTRY_ENERGY });

			parameters.Add(new MySqlParameter("@barcode", MySqlDbType.VarString) { Value = dto.barcode });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			
			Set(query, parameters);
		}

		public void UpdateStepInfo(tbl_task_run_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_task_run set step = @step where run_id = @run_id ");
			
			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@step", MySqlDbType.VarString) { Value = dto.step });
			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			
			Set(query, parameters);
		}

		public void UpdateTaskInfo(tbl_task_run_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_task_run set ");
			sb.Append("task_id = @task_id, ");
			sb.Append("task_name = @task_name, ");
			sb.Append("task_type = @task_type ");
			sb.Append("where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@task_id", MySqlDbType.Int32) { Value = dto.task_id });
			parameters.Add(new MySqlParameter("@task_name", MySqlDbType.VarString) { Value = dto.task_name });
			parameters.Add(new MySqlParameter("@task_type", MySqlDbType.VarString) { Value = dto.task_type });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}

		public void UpdateTaskCompleted(tbl_task_run_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_task_run set completed_dt = @completed_dt where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@completed_dt", MySqlDbType.DateTime) { Value = dto.completed_dt });
			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}

		public void UpdateExtraInfo(string runId, int INSPCT_SN)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_task_run set INSPCT_SN = @INSPCT_SN where run_id = @run_id ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = INSPCT_SN });
			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = runId });

			Set(query, parameters);
		}

	}
}
