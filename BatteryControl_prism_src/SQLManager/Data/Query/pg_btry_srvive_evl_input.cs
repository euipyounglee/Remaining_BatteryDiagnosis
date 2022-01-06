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
	public class pg_btry_srvive_evl_input : QueryManager
	{
		public int Insert(pg_btry_srvive_evl_input_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("INSERT INTO pg_btry_srvive_evl_input (");
			sb.Append("STEP_NO, CHANNEL, INPUT_VALUE, INPUT_DT, LOGIN_ID ");
			sb.Append(") ");
			sb.Append("VALUES (");
			sb.Append("@STEP_NO, @CHANNEL, @INPUT_VALUE, @INPUT_DT, @LOGIN_ID ");
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@STEP_NO", MySqlDbType.VarString) { Value = dto.STEP_NO });
			parameters.Add(new MySqlParameter("@CHANNEL", MySqlDbType.VarString) { Value = dto.CHANNEL });
			parameters.Add(new MySqlParameter("@INPUT_VALUE", MySqlDbType.VarString) { Value = dto.INPUT_VALUE });
			parameters.Add(new MySqlParameter("@INPUT_DT", MySqlDbType.VarString) { Value = dto.INPUT_DT });
			parameters.Add(new MySqlParameter("@LOGIN_ID", MySqlDbType.VarString) { Value = dto.LOGIN_ID });

			return SetAndResultLastId(query, parameters);
		}
	}
}
