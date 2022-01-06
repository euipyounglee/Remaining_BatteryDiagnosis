using Prism.Mvvm;
using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class DatabaseConfigVM : BindableBase
	{
		#region property

		/// <summary>
		/// server
		/// </summary>
		public string Server
		{
			get
			{
				return DatabaseConfig.Instance.Server;
			}
			set
			{
				DatabaseConfig.Instance.Server = value;
				RaisePropertyChanged("Server");
			}
		}

        /// <summary>
        /// port
        /// </summary>
        public string Port
        {
            get
            {
                return DatabaseConfig.Instance.Port;
            }
            set
            {
                DatabaseConfig.Instance.Port = value;
                RaisePropertyChanged("Port");
            }
        }

        /// <summary>
        /// database
        /// </summary>
        public string Database
		{
			get
			{
				return DatabaseConfig.Instance.Database;
			}
			set
			{
				DatabaseConfig.Instance.Database = value;
				RaisePropertyChanged("Database");
			}
		}

		/// <summary>
		/// user id
		/// </summary>
		public string UserId
		{
			get
			{
				return DatabaseConfig.Instance.UserId;
			}
			set
			{
				DatabaseConfig.Instance.UserId = value;
				RaisePropertyChanged("UserId");
			}
		}

		/// <summary>
		/// password
		/// </summary>
		public string Password
		{
			get
			{
				return DatabaseConfig.Instance.Password;
			}
			set
			{
				DatabaseConfig.Instance.Password = value;
				RaisePropertyChanged("Password");
			}
		}

		#endregion
	}
}
