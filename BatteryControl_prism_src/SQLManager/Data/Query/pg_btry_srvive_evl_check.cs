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
	public class pg_btry_srvive_evl_check : QueryManager
	{
		public void Insert(pg_btry_srvive_evl_check_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("INSERT INTO pg_btry_srvive_evl_check(");
			sb.Append("INSPCT_SN, STEP_SN, MODE, CHRG, CND, ");
			sb.Append("EXPECT_TIME, PROGRS_SITTN, PROGRS_STTUS ");
			sb.Append(") ");
			sb.Append("VALUES (");
			sb.Append("@INSPCT_SN, @STEP_SN, @MODE, @CHRG, @CND, ");
			sb.Append("@EXPECT_TIME, @PROGRS_SITTN, @PROGRS_STTUS ");
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = dto.INSPCT_SN });
			parameters.Add(new MySqlParameter("@STEP_SN", MySqlDbType.Int32) { Value = dto.STEP_SN });
			parameters.Add(new MySqlParameter("@MODE", MySqlDbType.VarString) { Value = dto.MODE });
			parameters.Add(new MySqlParameter("@CHRG", MySqlDbType.VarString) { Value = dto.CHRG });
			parameters.Add(new MySqlParameter("@CND", MySqlDbType.VarString) { Value = dto.CND });

			parameters.Add(new MySqlParameter("@EXPECT_TIME", MySqlDbType.VarString) { Value = dto.EXPECT_TIME });
			parameters.Add(new MySqlParameter("@PROGRS_SITTN", MySqlDbType.VarString) { Value = dto.PROGRS_SITTN });
			parameters.Add(new MySqlParameter("@PROGRS_STTUS", MySqlDbType.VarString) { Value = dto.PROGRS_STTUS });
			
			Set(query, parameters);
		}
	}
}
