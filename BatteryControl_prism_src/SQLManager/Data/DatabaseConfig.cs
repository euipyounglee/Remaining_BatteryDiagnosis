using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.Data
{
	public class DatabaseConfig
	{
		#region variables

		/// <summary>
		/// schema version
		/// </summary>
		public const string SCHEMA_VERSION = "2.0.10";

		#endregion

		#region property

		/// <summary>
		/// server
		/// </summary>
		public string Server { get; set; }

        /// <summary>
        /// port number
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// database
        /// </summary>
        public string Database { get; set; }
		
		/// <summary>
		/// user id
		/// </summary>
		public string UserId { get; set; }
		
		/// <summary>
		/// password
		/// </summary>
		public string Password { get; set; }
		
		/// <summary>
		/// connection credential
		/// </summary>
		public string ConnectionCredential
		{
			get
			{
                if (Port == null || Port.Trim().Equals("") )
                {
                    Port = "3306";
                }
                return QueryManager.BuildConnectionCredential(Server, Port, Database, UserId, Password);
			}
		}

		#endregion

		#region single-ton

		/// <summary>
		/// instance
		/// </summary>
		private static DatabaseConfig m_Instance;
		public static DatabaseConfig Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new DatabaseConfig();
					m_Instance.Init();
				}
				return m_Instance;
			}
		}

		/// <summary>
		/// private constructor
		/// </summary>
		private DatabaseConfig()
		{
		}

		//데이타 베이스 초기화
		private void Init()
		{
			Server = "localhost";
			Database = "btry_eval"; // DB 설치후 이미 테이블이 존재 해야 한다.
            UserId = "root"; // MySQL 처음 설치 - 아이디
            Password = "1234"; //MySQL 처음 설치 비밀번호

        }

#endregion

	}
}
