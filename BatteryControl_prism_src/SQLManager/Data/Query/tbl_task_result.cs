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
	public class tbl_task_result : QueryManager
	{

		public void SaveOrUpdate(tbl_task_result_DTO dto)
		{
			if (exists(dto.run_id))
			{
				Update(dto);
			}
			else
			{
				Insert(dto);
			}
		}

		private bool exists(string runId)
		{
			string query = $"select * from tbl_task_result where run_id = '{runId}' ";
			var result = Get<tbl_task_result_DTO>(query);
			return result.Count > 0;
		}

		/// <summary>
		/// insert
		/// </summary>
		/// <param name="dto"></param>
		private void Insert(tbl_task_result_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("insert into tbl_task_result (");
			sb.Append("run_id, evaluation_dt, evaluation_type, time_required, grade, ");
			sb.Append("soc, soh, sob, sop, BTRY_CODE, ");
			sb.Append("BTRY_TY, MARK_CODE, MAKR_DESC, MODEL_CODE, MODL_DESC, ");
			sb.Append("CONFIG, create_dt, update_dt ");
			sb.Append(") ");
			sb.Append("values (");
			sb.Append("@run_id, @evaluation_dt, @evaluation_type, @time_required, @grade, ");
			sb.Append("@soc, @soh, @sob, @sop, @BTRY_CODE, ");
			sb.Append("@BTRY_TY, @MARK_CODE, @MAKR_DESC, @MODEL_CODE, @MODL_DESC, ");
			sb.Append("@CONFIG, NOW(), NOW() ");
			sb.Append(")");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });
			parameters.Add(new MySqlParameter("@evaluation_dt", MySqlDbType.DateTime) { Value = dto.evaluation_dt });
			parameters.Add(new MySqlParameter("@evaluation_type", MySqlDbType.VarString) { Value = dto.evaluation_type });
			parameters.Add(new MySqlParameter("@time_required", MySqlDbType.Time) { Value = dto.time_required });
			parameters.Add(new MySqlParameter("@grade", MySqlDbType.VarString) { Value = dto.grade });

			parameters.Add(new MySqlParameter("@soc", MySqlDbType.Float) { Value = dto.soc });
			parameters.Add(new MySqlParameter("@soh", MySqlDbType.Float) { Value = dto.soh });
			parameters.Add(new MySqlParameter("@sob", MySqlDbType.Float) { Value = dto.sob });
			parameters.Add(new MySqlParameter("@sop", MySqlDbType.Float) { Value = dto.sop });
			parameters.Add(new MySqlParameter("@BTRY_CODE", MySqlDbType.VarString) { Value = dto.BTRY_CODE });

			parameters.Add(new MySqlParameter("@BTRY_TY", MySqlDbType.VarString) { Value = dto.BTRY_TY });
			parameters.Add(new MySqlParameter("@MARK_CODE", MySqlDbType.VarString) { Value = dto.MARK_CODE });
			parameters.Add(new MySqlParameter("@MAKR_DESC", MySqlDbType.VarString) { Value = dto.MAKR_DESC });
			parameters.Add(new MySqlParameter("@MODEL_CODE", MySqlDbType.VarString) { Value = dto.MODEL_CODE });
			parameters.Add(new MySqlParameter("@MODL_DESC", MySqlDbType.VarString) { Value = dto.MODL_DESC });

			parameters.Add(new MySqlParameter("@CONFIG", MySqlDbType.VarString) { Value = dto.CONFIG });

			Set(query, parameters);
		}

		/// <summary>
		/// update
		/// </summary>
		/// <param name="dto"></param>
		private void Update(tbl_task_result_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("update tbl_task_result set ");
			sb.Append("evaluation_dt = @evaluation_dt, ");
			sb.Append("evaluation_type = @evaluation_type, ");
			sb.Append("time_required = @time_required, ");
			sb.Append("grade = @grade, ");
			sb.Append("soc = @soc, soh = @soh, sob = @sob, sop = @sop, ");
			sb.Append("BTRY_CODE = @BTRY_CODE, ");
			sb.Append("BTRY_TY = @BTRY_TY, ");
			sb.Append("MARK_CODE = @MARK_CODE, ");
			sb.Append("MAKR_DESC = @MAKR_DESC, ");
			sb.Append("MODEL_CODE = @MODEL_CODE, ");
			sb.Append("MODL_DESC = @MODL_DESC, ");
			sb.Append("CONFIG = @CONFIG, ");
			sb.Append("update_dt = NOW() ");
			sb.Append("where run_id = @run_id ");
			
			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@evaluation_dt", MySqlDbType.DateTime) { Value = dto.evaluation_dt });
			parameters.Add(new MySqlParameter("@evaluation_type", MySqlDbType.VarString) { Value = dto.evaluation_type });
			parameters.Add(new MySqlParameter("@time_required", MySqlDbType.Time) { Value = dto.time_required });
			parameters.Add(new MySqlParameter("@grade", MySqlDbType.VarString) { Value = dto.grade });

			parameters.Add(new MySqlParameter("@soc", MySqlDbType.Float) { Value = dto.soc });
			parameters.Add(new MySqlParameter("@soh", MySqlDbType.Float) { Value = dto.soh });
			parameters.Add(new MySqlParameter("@sob", MySqlDbType.Float) { Value = dto.sob });
			parameters.Add(new MySqlParameter("@sop", MySqlDbType.Float) { Value = dto.sop });
			
			parameters.Add(new MySqlParameter("@BTRY_CODE", MySqlDbType.VarString) { Value = dto.BTRY_CODE });
			parameters.Add(new MySqlParameter("@BTRY_TY", MySqlDbType.VarString) { Value = dto.BTRY_TY });
			parameters.Add(new MySqlParameter("@MARK_CODE", MySqlDbType.VarString) { Value = dto.MARK_CODE });
			parameters.Add(new MySqlParameter("@MAKR_DESC", MySqlDbType.VarString) { Value = dto.MAKR_DESC });
			parameters.Add(new MySqlParameter("@MODEL_CODE", MySqlDbType.VarString) { Value = dto.MODEL_CODE });
			parameters.Add(new MySqlParameter("@MODL_DESC", MySqlDbType.VarString) { Value = dto.MODL_DESC });
			parameters.Add(new MySqlParameter("@CONFIG", MySqlDbType.VarString) { Value = dto.CONFIG });

			parameters.Add(new MySqlParameter("@run_id", MySqlDbType.VarString) { Value = dto.run_id });

			Set(query, parameters);
		}
	}
}
