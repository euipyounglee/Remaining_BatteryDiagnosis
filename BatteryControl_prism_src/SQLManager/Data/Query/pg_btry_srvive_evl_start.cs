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
	public class pg_btry_srvive_evl_start : QueryManager
	{
		/// <summary>
		/// insert
		/// </summary>
		/// <param name="dto"></param>
		/// <returns></returns>
		public int Insert(pg_btry_srvive_evl_start_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("INSERT INTO pg_btry_srvive_evl_start(");
			sb.Append("INSPCT_BTRY, INSPCT_MAKR, INSPCT_MODL, INSPCT_CONFIG, BRCD_NO, ");
			sb.Append("VHCLE_MAKR_TY, VHCLE_MODL_TY, VLTGE, CPCTY, COMPOSITION, ");
			sb.Append("MUMM_VLTGE, MXMM_VLTGE, BTRY_ENERGY, BTRY_CODE, EQPMN_IP, ");
			sb.Append("CHANNEL, CNNC_EQPMN, INSPCT_TY, INSPCT_BEGIN_DT, INSPCT_EXPECT_END_DT, ");
			sb.Append("INSPCT_COMPT_STEP, LOGIN_ID ");
			sb.Append(") ");
			sb.Append("VALUES (");
			sb.Append("@INSPCT_BTRY, @INSPCT_MAKR, @INSPCT_MODL, @INSPCT_CONFIG, @BRCD_NO, ");
			sb.Append("@VHCLE_MAKR_TY, @VHCLE_MODL_TY, @VLTGE, @CPCTY, @COMPOSITION, ");
			sb.Append("@MUMM_VLTGE, @MXMM_VLTGE, @BTRY_ENERGY, @BTRY_CODE, @EQPMN_IP, ");
			sb.Append("@CHANNEL, @CNNC_EQPMN, @INSPCT_TY, @INSPCT_BEGIN_DT, @INSPCT_EXPECT_END_DT, ");
			sb.Append("@INSPCT_COMPT_STEP, @LOGIN_ID ");
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@INSPCT_BTRY", MySqlDbType.VarString) { Value = dto.INSPCT_BTRY });
			parameters.Add(new MySqlParameter("@INSPCT_MAKR", MySqlDbType.VarString) { Value = dto.INSPCT_MAKR });
			parameters.Add(new MySqlParameter("@INSPCT_MODL", MySqlDbType.VarString) { Value = dto.INSPCT_MODL });
			parameters.Add(new MySqlParameter("@INSPCT_CONFIG", MySqlDbType.VarString) { Value = dto.INSPCT_CONFIG });
			parameters.Add(new MySqlParameter("@BRCD_NO", MySqlDbType.VarString) { Value = dto.BRCD_NO });

			parameters.Add(new MySqlParameter("@VHCLE_MAKR_TY", MySqlDbType.VarString) { Value = dto.VHCLE_MAKR_TY });
			parameters.Add(new MySqlParameter("@VHCLE_MODL_TY", MySqlDbType.VarString) { Value = dto.VHCLE_MODL_TY });
			parameters.Add(new MySqlParameter("@VLTGE", MySqlDbType.Float) { Value = dto.VLTGE });
			parameters.Add(new MySqlParameter("@CPCTY", MySqlDbType.Float) { Value = dto.CPCTY });
			parameters.Add(new MySqlParameter("@COMPOSITION", MySqlDbType.VarString) { Value = dto.COMPOSITION });

			parameters.Add(new MySqlParameter("@MUMM_VLTGE", MySqlDbType.Float) { Value = dto.MUMM_VLTGE });
			parameters.Add(new MySqlParameter("@MXMM_VLTGE", MySqlDbType.Float) { Value = dto.MXMM_VLTGE });
			parameters.Add(new MySqlParameter("@BTRY_ENERGY", MySqlDbType.Float) { Value = dto.BTRY_ENERGY });
			parameters.Add(new MySqlParameter("@BTRY_CODE", MySqlDbType.VarString) { Value = dto.BTRY_CODE });
			parameters.Add(new MySqlParameter("@EQPMN_IP", MySqlDbType.VarString) { Value = dto.EQPMN_IP });

			parameters.Add(new MySqlParameter("@CHANNEL", MySqlDbType.VarString) { Value = dto.CHANNEL });
			parameters.Add(new MySqlParameter("@CNNC_EQPMN", MySqlDbType.VarString) { Value = dto.CNNC_EQPMN });
			parameters.Add(new MySqlParameter("@INSPCT_TY", MySqlDbType.VarString) { Value = dto.INSPCT_TY });
			parameters.Add(new MySqlParameter("@INSPCT_BEGIN_DT", MySqlDbType.VarString) { Value = dto.INSPCT_BEGIN_DT });
			parameters.Add(new MySqlParameter("@INSPCT_EXPECT_END_DT", MySqlDbType.VarString) { Value = dto.INSPCT_EXPECT_END_DT });

			parameters.Add(new MySqlParameter("@INSPCT_COMPT_STEP", MySqlDbType.Int32) { Value = dto.INSPCT_COMPT_STEP });
			parameters.Add(new MySqlParameter("@LOGIN_ID", MySqlDbType.VarString) { Value = dto.LOGIN_ID });

			return SetAndResultLastId(query, parameters);
		}
	}
}
