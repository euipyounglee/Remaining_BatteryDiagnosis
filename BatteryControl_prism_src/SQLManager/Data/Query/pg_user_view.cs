using SQLManager.Data.DTO;
using SQLManager.Helper;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.Query
{
	public class pg_user_view : QueryManager
	{
		public pg_user_view_DTO Login(string userId, string passwd)
		{
			string hashed = HashHelper.Sha512(passwd);
			string query = $"select * from pg_user_view a where a.USER_ID = '{userId}' and a.USER_PW = '{hashed}' ";

			var result =  Get<pg_user_view_DTO>(query);
			return result.Count == 0 ? null : result.ElementAt(0);
		}
	}
}
