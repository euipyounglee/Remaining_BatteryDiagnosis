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
	public class pg_btry_srvive_evl_end : QueryManager
	{
		public void Insert(pg_btry_srvive_evl_end_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("INSERT INTO pg_btry_srvive_evl_end (");
			sb.Append("INSPCT_SN, INSPCT_REQRE_TIME, INSPCT_END_DT, INSPCT_GRAD_TY, SOC, ");
			sb.Append("SOP, SOH, SOB ) ");
			sb.Append("VALUES (");
			sb.Append("@INSPCT_SN, @INSPCT_REQRE_TIME, @INSPCT_END_DT, @INSPCT_GRAD_TY, @SOC, ");
			sb.Append("@SOP, @SOH, @SOB ");
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = dto.INSPCT_SN });
			parameters.Add(new MySqlParameter("@INSPCT_REQRE_TIME", MySqlDbType.VarString) { Value = dto.INSPCT_REQRE_TIME });
			parameters.Add(new MySqlParameter("@INSPCT_END_DT", MySqlDbType.VarString) { Value = dto.INSPCT_END_DT });
			parameters.Add(new MySqlParameter("@INSPCT_GRAD_TY", MySqlDbType.VarString) { Value = dto.INSPCT_GRAD_TY });
			parameters.Add(new MySqlParameter("@SOC", MySqlDbType.Float) { Value = dto.SOC });

			parameters.Add(new MySqlParameter("@SOP", MySqlDbType.Float) { Value = dto.SOP });
			parameters.Add(new MySqlParameter("@SOH", MySqlDbType.Float) { Value = dto.SOH });
			parameters.Add(new MySqlParameter("@SOB", MySqlDbType.Float) { Value = dto.SOB });

			Set(query, parameters);
		}
	}
}
