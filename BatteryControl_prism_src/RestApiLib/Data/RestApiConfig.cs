using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiLib.Data
{
	public class RestApiConfig
	{
		#region property

		/// <summary>
		/// 로그 파일 경로
		/// </summary>
		public string LogFilePath { get; set; }

		/// <summary>
		/// Portal의 REST API에 대한 Base Address
		/// </summary>
		public string PortBaseUri { get; set; }

		/// <summary>
		/// 검사장비 종류: P = Pack 검사장비, M = Module 검사장비
		/// </summary>
		public string DeviceType = "P";

		/// <summary>
		/// 검사장비 번호: Pack = 1 ~ 9, Module = 10 ~ 30
		/// </summary>
		public string DeviceNo = "1";

		#endregion

		#region single-ton

		/// <summary>
		/// rest api config instance
		/// </summary>
		private static RestApiConfig m_Instance;
		public static RestApiConfig Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new RestApiConfig();
					m_Instance.Init();
				}
				return m_Instance;
			}
		}

		/// <summary>
		/// private constructor
		/// </summary>
		private RestApiConfig()
		{
			LogFilePath = $@"{AppDomain.CurrentDomain.BaseDirectory}\log\REST_API";
			if (!System.IO.Directory.Exists(LogFilePath))
			{
				System.IO.Directory.CreateDirectory(LogFilePath);
			}
		}

		private void Init()
		{
			//PortBaseUri = "http://192.168.0.22:8080/jtp-battery-collection/btrySrviveEvl/";
            PortBaseUri = "http://collect.gbtp.cion.co.kr/btrySrviveEvl/";
            DeviceType = "P";
			DeviceNo = "1";
		}

		#endregion
	}
}
