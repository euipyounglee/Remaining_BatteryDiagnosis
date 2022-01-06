using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class LocalConfigVM : BindableBase
	{
		/// <summary>
		/// version
		/// </summary>
		private string m_Version = "2020.05.10.0437";
		public string Version
		{
			get
			{
				return m_Version;
			}
			set
			{
				m_Version = value;
				RaisePropertyChanged("Version");
			}
		}

		/// <summary>
		/// test type
		/// </summary>
		private string m_BatteryTestType = "MODULE";
		public string BatteryTestType
		{
			get
			{
				return m_BatteryTestType;
			}
			set
			{
				m_BatteryTestType = value;
				RaisePropertyChanged("BatteryTestType");
			}
		}

		/// <summary>
		/// database config
		/// </summary>
		private DatabaseConfigVM m_DatabaseConfig;
		public DatabaseConfigVM DatabaseConfig
		{
			get
			{
				if (m_DatabaseConfig == null)
				{
					m_DatabaseConfig = new DatabaseConfigVM();
				}
				return m_DatabaseConfig;
			}
			set
			{
				m_DatabaseConfig = value;
				RaisePropertyChanged("DatabaseConfig");
			}
		}

		/// <summary>
		/// rest api config
		/// </summary>
		private RestApiConfigVM m_RestApiConfig;
		public RestApiConfigVM RestApiConfig
		{
			get
			{
				if (m_RestApiConfig == null)
				{
					m_RestApiConfig = new RestApiConfigVM();
				}
				return m_RestApiConfig;
			}
			set
			{
				m_RestApiConfig = value;
				RaisePropertyChanged("RestApiConfig");
			}
		}

		/// <summary>
		/// engine config
		/// </summary>
		private EngineConfigVM m_EngineConfig;
		public EngineConfigVM EngineConfig
		{
			get
			{
				if (m_EngineConfig == null)
				{
					m_EngineConfig = new EngineConfigVM();
				}
				return m_EngineConfig;
			}
			set
			{
				m_EngineConfig = value;
				RaisePropertyChanged("EngineConfig");
			}
		}

		/// <summary>
		/// channel 1
		/// </summary>
		private ChannelConfigVM m_Channel1;
		public ChannelConfigVM Channel1
		{
			get
			{
				if (m_Channel1 == null)
				{
					m_Channel1 = new ChannelConfigVM();
				}
				return m_Channel1;
			}
			set
			{
				m_Channel1 = value;
				RaisePropertyChanged("Channel1");
			}
		}

		/// <summary>
		/// channel 2
		/// </summary>
		private ChannelConfigVM m_Channel2;
		public ChannelConfigVM Channel2
		{
			get
			{
				if (m_Channel2 == null)
				{
					m_Channel2 = new ChannelConfigVM();
				}
				return m_Channel2;
			}
			set
			{
				m_Channel2 = value;
				RaisePropertyChanged("Channel2");
			}
		}
	}
}
