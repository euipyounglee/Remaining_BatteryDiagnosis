using SQLManager.Data.DTO;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.Query
{
	public class tbl_code : QueryManager
	{
		public List<tbl_code_DTO> Get()
		{
			string query = $"select * from tbl_code where major_cd = 'SCHEMA_VERSION' ";
			return Get<tbl_code_DTO>(query);
		}
	}
}
