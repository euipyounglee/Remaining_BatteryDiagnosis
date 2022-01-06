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
	public class pg_btry_srvive_evl_detail : QueryManager
	{
		public int Insert(pg_btry_srvive_evl_detail_DTO dto)
		{
			var sb = new StringBuilder();
			sb.Append("INSERT INTO pg_btry_srvive_evl_detail(");
			sb.Append("INSPCT_SN, STEP_NO, START_DT, END_DT, ACIA_V, ");
			sb.Append("ACIA_T, ACIA_ACIR, ACIA_RS, ACIA_RE, ACIA_RP, ");
			sb.Append("ACIA_CP, CYCLER_V, CYCLER_T, CYCLER_CURRENT, CYCLER_CAPACITY ");
			for (int i=1; i<=96; ++i)
			{
				sb.Append($", V{i}, T{i} ");
			}
			sb.Append(") ");
			sb.Append("VALUES (");
			sb.Append("@INSPCT_SN, @STEP_NO, @START_DT, @END_DT, @ACIA_V, ");
			sb.Append("@ACIA_T, @ACIA_ACIR, @ACIA_RS, @ACIA_RE, @ACIA_RP, ");
			sb.Append("@ACIA_CP, @CYCLER_V, @CYCLER_T, @CYCLER_CURRENT, @CYCLER_CAPACITY ");
			for (int i = 1; i <= 96; ++i)
			{
				sb.Append($", @V{i}, @T{i} ");
			}
			sb.Append(") ");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = dto.INSPCT_SN });
			parameters.Add(new MySqlParameter("@STEP_NO", MySqlDbType.VarString) { Value = dto.STEP_NO });
			parameters.Add(new MySqlParameter("@START_DT", MySqlDbType.VarString) { Value = dto.START_DT });
			parameters.Add(new MySqlParameter("@END_DT", MySqlDbType.VarString) { Value = dto.END_DT });
			parameters.Add(new MySqlParameter("@ACIA_V", MySqlDbType.Float) { Value = dto.ACIA_V });

			parameters.Add(new MySqlParameter("@ACIA_T", MySqlDbType.Float) { Value = dto.ACIA_T });
			parameters.Add(new MySqlParameter("@ACIA_ACIR", MySqlDbType.Float) { Value = dto.ACIA_ACIR });
			parameters.Add(new MySqlParameter("@ACIA_RS", MySqlDbType.Float) { Value = dto.ACIA_RS });
			parameters.Add(new MySqlParameter("@ACIA_RE", MySqlDbType.Float) { Value = dto.ACIA_RE });
			parameters.Add(new MySqlParameter("@ACIA_RP", MySqlDbType.Float) { Value = dto.ACIA_RP });

			parameters.Add(new MySqlParameter("@ACIA_CP", MySqlDbType.Float) { Value = dto.ACIA_CP });
			parameters.Add(new MySqlParameter("@CYCLER_V", MySqlDbType.Float) { Value = dto.CYCLER_V });
			parameters.Add(new MySqlParameter("@CYCLER_T", MySqlDbType.Float) { Value = dto.CYCLER_T });
			parameters.Add(new MySqlParameter("@CYCLER_CURRENT", MySqlDbType.Float) { Value = dto.CYCLER_CURRENT });
			parameters.Add(new MySqlParameter("@CYCLER_CAPACITY", MySqlDbType.Float) { Value = dto.CYCLER_CAPACITY });

			for (int i = 1; i <= 96; ++i)
			{
				parameters.Add(new MySqlParameter($"@V{i}", MySqlDbType.Float) 
				{ 
					Value = dto.GetType().GetProperty($"V{i}").GetValue(dto)
				});

				parameters.Add(new MySqlParameter($"@T{i}", MySqlDbType.Float)
				{
					Value = dto.GetType().GetProperty($"T{i}").GetValue(dto)
				});
			} // end for

			return SetAndResultLastId(query, parameters);
		}

        public int UpdateBIVoltage(pg_btry_srvive_evl_detail_DTO dto)
        {
            var sb = new StringBuilder();

            sb.Append(" UPDATE pg_btry_srvive_evl_detail SET ");
            for (int i = 1; i <= 96; ++i)
            {
                if (i > 1)
                {
                    sb.Append(",");
                }
                sb.Append($" V{i} = @V{i} ");
            }
            sb.Append(" WHERE INSPCT_SN = @INSPCT_SN and STEP_NO = @STEP_NO ");
            string query = sb.ToString();

            var parameters = new List<MySqlParameter>();

            for (int i = 1; i <= 96; ++i)
            {
                parameters.Add(new MySqlParameter($"@V{i}", MySqlDbType.Float)
                {
                    Value = dto.GetType().GetProperty($"V{i}").GetValue(dto)
                });
            } // end for
            parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = dto.INSPCT_SN });
            parameters.Add(new MySqlParameter("@STEP_NO", MySqlDbType.VarString) { Value = dto.STEP_NO });

            return SetAndResultLastId(query, parameters);
        }

        public int UpdateBITemperature(pg_btry_srvive_evl_detail_DTO dto)
        {
            var sb = new StringBuilder();

            sb.Append(" UPDATE pg_btry_srvive_evl_detail SET ");
            for (int i = 1; i <= 96; ++i)
            {
                if (i > 1)
                {
                    sb.Append(",");
                }
                sb.Append($" T{i} = @T{i} ");
            }
            sb.Append(" WHERE INSPCT_SN = @INSPCT_SN and STEP_NO = @STEP_NO ");
            string query = sb.ToString();

            var parameters = new List<MySqlParameter>();

            for (int i = 1; i <= 96; ++i)
            {
                parameters.Add(new MySqlParameter($"@T{i}", MySqlDbType.Float)
                {
                    Value = dto.GetType().GetProperty($"T{i}").GetValue(dto)
                });
            } // end for
            parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = dto.INSPCT_SN });
            parameters.Add(new MySqlParameter("@STEP_NO", MySqlDbType.VarString) { Value = dto.STEP_NO });

            return SetAndResultLastId(query, parameters);
        }

        public int PatchPortalCyclerVoltage(pg_btry_srvive_evl_detail_DTO dto)
        {
            var sb = new StringBuilder();

            sb.Append(" UPDATE pg_btry_srvive_evl_detail SET ");
            sb.Append(" CYCLER_V = @CYCLER_V ");
            sb.Append(" WHERE INSPCT_SN = @INSPCT_SN and STEP_NO = @STEP_NO ");
            string query = sb.ToString();

            var parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("@CYCLER_V", MySqlDbType.Float) { Value = dto.CYCLER_V });
            parameters.Add(new MySqlParameter("@INSPCT_SN", MySqlDbType.Int32) { Value = dto.INSPCT_SN });
            parameters.Add(new MySqlParameter("@STEP_NO", MySqlDbType.VarString) { Value = dto.STEP_NO });

            return SetAndResultLastId(query, parameters);
        }
    }
}
