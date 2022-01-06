using MySql.Data.MySqlClient;
using SQLManager.Data.DTO;
using SQLManager.Defines;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data.Query
{
	public class tbl_log_app : QueryManager
	{
		#region single-ton

		/// <summary>
		/// instance
		/// </summary>
		private static tbl_log_app m_Instance;
		public static tbl_log_app Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new tbl_log_app();
				}
				return m_Instance;
			}
		}

		/// <summary>
		/// private constructor
		/// </summary>
		private tbl_log_app()
		{

		}

		#endregion

		#region method

		/// <summary>
		/// select
		/// </summary>
		/// <returns></returns>
		public List<tbl_log_app_DTO> Get()
		{
			string query = $"select * from tbl_log_app order by log_dt desc ";
			return Get<tbl_log_app_DTO>(query);
		}

		/// <summary>
		/// insert
		/// </summary>
		/// <param name="dto"></param>
		public void Insert(LogDev logDevice, string msg, LogLevels logLevel = LogLevels.I)
		{
			var dto = new tbl_log_app_DTO
			{
				log_dt = DateTime.Now,
				device_cd = $"{logDevice}",
				log_level = $"{logLevel}",
				log_msg = msg
			};

			var sb = new StringBuilder();
			sb.Append("insert into tbl_log_app (log_dt, log_level, device_cd, log_msg) ");
			sb.Append("values (@log_dt, @log_level, @device_cd, @log_msg)");

			string query = sb.ToString();

			var parameters = new List<MySqlParameter>();

			parameters.Add(new MySqlParameter("@log_dt", MySqlDbType.DateTime) { Value = dto.log_dt });
			parameters.Add(new MySqlParameter("@log_level", MySqlDbType.String) { Value = dto.log_level });
			parameters.Add(new MySqlParameter("@device_cd", MySqlDbType.String) { Value = dto.device_cd });
			parameters.Add(new MySqlParameter("@log_msg", MySqlDbType.String) { Value = dto.log_msg });
			
			Set(query, parameters);
		}

		#endregion
	}
}
