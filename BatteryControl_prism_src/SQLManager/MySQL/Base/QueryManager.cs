using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dibier.mssql;
using SQLManager.Data;

namespace SQLManager.MySQL.Base
{
    public class QueryManager
    {
		/// <summary>
		/// connection credential
		/// </summary>
		private string Conn { get; set; }

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="server"></param>
		/// <param name="database"></param>
		/// <param name="userid"></param>
		/// <param name="passwd"></param>
		public QueryManager()
		{
			Conn = DatabaseConfig.Instance.ConnectionCredential;
		}

		/// <summary>
		/// build connection credential
		/// </summary>
		/// <param name="server"></param>
		/// <param name="database"></param>
		/// <param name="userid"></param>
		/// <param name="passwd"></param>
		/// <returns></returns>
		public static string BuildConnectionCredential(string server, string port, string database, string userid, string passwd)
		{
            return $"Server={server};Port={port};Database={database};Uid={userid};Pwd={passwd};Charset=utf8;persistsecurityinfo=True;SslMode=none";
		}

		/// <summary>
		/// verify connection
		/// </summary>
		/// <returns></returns>
		public bool VerifyConnection()
		{
			try
			{
				using (MySqlConnection conn = new MySqlConnection(Conn))
				{
					conn.Open();
					conn.Close();
				}

				return true;
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine(ex.ToString());
                Console.WriteLine(ex.ToString());
                return false;
			}
		}

		/// <summary>
		/// select
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		protected List<T> Get<T>(string query) where T : IResultSet, new()
        {
            List<T> rss = new List<T>();

            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        T rs = new T();
                        rs.Fetch(rdr);
                        rss.Add(rs);
                    } // end while
                }
                catch (Exception e)
                {
                    Console.Out.WriteLine(e.ToString());
                }
                finally
                {
                    conn.Close();
                }
                
            } // end using

            return rss;
        }

        /// <summary>
        /// insert, update, delete with parameters
        /// </summary>
        /// <param name="query"></param>
        protected void Set(string query, List<MySqlParameter> parameters)
        {
            using (MySqlConnection conn = new MySqlConnection(Conn))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
				
				foreach (var parameter in parameters)
				{
					cmd.Parameters.Add(parameter);
				}

                cmd.ExecuteNonQuery();

                conn.Close();
            } // end using
        }

		/// <summary>
		/// insert, update, delete without parameters
		/// </summary>
		/// <param name="query"></param>
		protected void Set(string query)
		{
			Set(query, new List<MySqlParameter>());
		}

		/// <summary>
		/// set and return last id
		/// </summary>
		/// <param name="query"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		protected int SetAndResultLastId(string query, List<MySqlParameter> parameters)
		{
			query += "; SELECT LAST_INSERT_ID(); ";

			using (MySqlConnection conn = new MySqlConnection(Conn))
			{
				conn.Open();
				MySqlCommand cmd = new MySqlCommand(query, conn);

				foreach (var parameter in parameters)
				{
					cmd.Parameters.Add(parameter);
				}

				int lastId = Convert.ToInt32(cmd.ExecuteScalar());

				conn.Close();

				return lastId;
			} // end using
		}

	}
}
