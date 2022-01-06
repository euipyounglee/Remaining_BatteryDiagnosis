using SQLManager.Data.DTO;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.Query
{
	public class pg_brty_soc : QueryManager
	{
		/// <summary>
		/// get soc
		/// </summary>
		/// <param name="BTRY_CODE"></param>
		/// <param name="voltage">모든 스케쥴이 종료된 이후 충방전기의 전압값</param>
		/// <returns></returns>
		public float Calc(string BTRY_CODE, float voltage)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("WITH T AS( ");
			sb.Append("SELECT SOC as sSOC ");
			sb.Append(", VOLT as sVolt ");
			sb.Append(", LAG(SOC, 1) OVER(ORDER BY soc DESC) as eSOC ");
			sb.Append(", LAG(VOLT, 1) OVER(ORDER BY soc DESC) as eVolt ");
			sb.Append("FROM pg_brty_soc ");
			sb.Append($"WHERE BTRY_CODE = '{BTRY_CODE}' ");
			sb.Append(") ");
			sb.Append($"SELECT CASE WHEN MAX(sVolt) <= {voltage} THEN TRUNCATE(MAX(sSOC), 3 ) ");
			sb.Append($"WHEN MIN(sVolt) > {voltage} THEN TRUNCATE(MIN(sSOC), 3 ) ");
			sb.Append($"ELSE TRUNCATE((SELECT (eSOC -sSOC ) *({voltage} - sVolt) / (eVolt - sVolt) + sSOC ");
			sb.Append("FROM T ");
			sb.Append($"WHERE sVolt <= {voltage} and eVolt > {voltage} ), 3) ");
			sb.Append("END as SOC ");
			sb.Append("FROM T ");

			var result = Get<pg_brty_soc_DTO>(sb.ToString());
			return result.Count > 0 ? Convert.ToSingle(result.ElementAt(0).SOC) : 0.0f;
		}
	}
}
