using SQLManager.Data.DTO;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.Query
{
	public class pg_btry_info : QueryManager
	{
		public List<pg_btry_info_DTO> GetMakers(string BTRY_TY)
		{
			string query = $"select distinct a.MARK_CODE, a.MAKR_DESC from pg_btry_info a where a.BTRY_TY = '{BTRY_TY}' order by a.MAKR_DESC ";
			return Get<pg_btry_info_DTO>(query);
		}

		public List<pg_btry_info_DTO> GetModels(string BTRY_TY, string MARK_CODE)
		{
			string query = $"select distinct a.MARK_CODE, a.MAKR_DESC, a.MODEL_CODE, a.MODL_DESC from pg_btry_info a where a.BTRY_TY = '{BTRY_TY}' and a.MARK_CODE = '{MARK_CODE}' order by a.MODL_DESC ";
			return Get<pg_btry_info_DTO>(query);
		}

		public List<pg_btry_info_DTO> GetConfigs(string BTRY_TY, string MARK_CODE, string MODEL_CODE)
		{
			string query = $"select distinct a.MARK_CODE, a.MAKR_DESC, a.MODEL_CODE, a.MODL_DESC, a.CONFIG, a.TYPE_P, a.TYPE_S from pg_btry_info a where a.BTRY_TY = '{BTRY_TY}' and a.MARK_CODE = '{MARK_CODE}' and a.MODEL_CODE = '{MODEL_CODE}' order by a.CONFIG ";
			return Get<pg_btry_info_DTO>(query);
		}

		public pg_btry_info_DTO Get(string BTRY_TY, string MARK_CODE, string MODEL_CODE, string CONFIG)
		{
			string query = $"select * from pg_btry_info a where a.BTRY_TY = '{BTRY_TY}' and a.MARK_CODE = '{MARK_CODE}' and a.MODEL_CODE = '{MODEL_CODE}' and a.CONFIG = '{CONFIG}' ";
			var result = Get<pg_btry_info_DTO>(query);
			if (result.Count > 0)
			{
				return result.ElementAt(0);
			}
			else
			{
				return new pg_btry_info_DTO();
			}
		}

		public pg_btry_info_DTO Get(string BTRY_CODE)
		{
			string query = $"select * from pg_btry_info a where a.BTRY_CODE = '{BTRY_CODE}' ";
			var result = Get<pg_btry_info_DTO>(query);
			return result.Count > 0 ? result.ElementAt(0) : null;
		}

	}
}
