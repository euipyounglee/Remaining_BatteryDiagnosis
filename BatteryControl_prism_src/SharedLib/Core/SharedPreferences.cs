using I7565H1Lib.Core;
using PneCtsLib.Data.DTO;
using Prism.Mvvm;
using SharedLib.Data;
using SharedLib.Data.VM;
using SharedLib.View;
using SQLManager.Data;
using SQLManager.Data.DTO;
using SQLManager.Data.Query;
using SQLManager.Defines;
using SQLManager.MySQL.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SharedLib.Core
{
	public class SharedPreferences : BindableBase
	{
		#region single-ton

		private SharedPreferences()
		{

		}


		private static SharedPreferences m_Instance;
		public static SharedPreferences Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new SharedPreferences();
					m_Instance.Init();
				}
				return m_Instance;
			}
		}

		private void Init()
		{
			// 채널정보
			ChannelVMs = new List<ChannelViewModel>();
			ChannelVMs.Add(new ChannelViewModel
			{
				ChannelIndex = 0     //ChannelIndex = 1
			});
			ChannelVMs.Add(new ChannelViewModel
			{
				ChannelIndex = 1     //ChannelIndex = 2
			});

			SelectedBatteryMap = new Dictionary<byte, pg_btry_info_VM>();


			SelectedBatteryMap.Add(0, new pg_btry_info_VM(new pg_btry_info_DTO()));
			SelectedBatteryMap.Add(1, new pg_btry_info_VM(new pg_btry_info_DTO()));

			BatteryBarcodeMap = new Dictionary<byte, string>();


			BatteryBarcodeMap.Add(0, "");
			BatteryBarcodeMap.Add(1, "");
			
			// pre-load
			PreLoad();
		}

		#endregion

		#region property

		private LocalConfigVM m_LocalConfig;
		public LocalConfigVM LocalConfig
		{
			get
			{
				return m_LocalConfig;
			}
			set
			{
				m_LocalConfig = value;
				RaisePropertyChanged("LocalConfig");
			}
		}

		public List<ChannelDevice> ChannelDevices { get; set; }
		
		public List<ChannelViewModel> ChannelVMs { get; set; }

		private pg_user_view_DTO m_LoginUser = new pg_user_view_DTO();
		public pg_user_view_DTO LoginUser
		{
			get
			{
				return m_LoginUser;
			}
			set
			{
				m_LoginUser = value;
				RaisePropertyChanged("LoginUser");
			}
		}

		public Dictionary<byte, pg_btry_info_VM> SelectedBatteryMap { get; set; }

		public Dictionary<byte, string> BatteryBarcodeMap { get; set; }

		#endregion

		#region method (pre-load)

		private void PreLoad()
		{
			CheckVersionOfConfig();


			CheckScheduleDirectory();


			CheckLogDirectories();


			CheckDatabaseConfig();


			CheckSchemaVersion();


			ChannelDevices = new List<ChannelDevice>();
			ChannelDevices.Add(new ChannelDevice(typeof(VCI_SDK_FOR_BICH1), 1));
			ChannelDevices.Add(new ChannelDevice(typeof(VCI_SDK_FOR_BICH2), 2));
		}

		private void CheckSchemaVersion()
		{
			var dtos = new tbl_code().Get();
			if (dtos.Count == 0 || !DatabaseConfig.SCHEMA_VERSION.Equals(dtos.ElementAt(0).minor_cd))
			{
				MessageBox.Show("데이터베이스 스키마 업데이트가 필요합니다.");
				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}

		private void CheckVersionOfConfig()
		{
			LocalConfig = XmlHelper.Load();


			XmlHelper.Save(LocalConfig);
		}

		private void CheckLogDirectories()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			basePath += "log";
			CreateDirectoryIfNotExists(basePath);

			// ACIR
			CreateDirectoryIfNotExists($@"{basePath}\{LogDev.ACIR}");

			// BI Box
			CreateDirectoryIfNotExists($@"{basePath}\{LogDev.BI_BOX}");

			// Relay Box
			CreateDirectoryIfNotExists($@"{basePath}\{LogDev.RELAY_BOX}");

			// multimeter
			CreateDirectoryIfNotExists($@"{basePath}\{LogDev.MULTIMETER}");

			// pne cts
			CreateDirectoryIfNotExists($@"{basePath}\{LogDev.Cycler}");

			// st5520
			CreateDirectoryIfNotExists($@"{basePath}\{LogDev.ST5520}");

			// ds3678
			CreateDirectoryIfNotExists($@"{basePath}\{LogDev.DS3678}");
		}

		private void CheckScheduleDirectory()
		{
			string basePath = $@"C:\gbtp2020\PNE_schedule";
			CreateDirectoryIfNotExists(basePath);
		}

		private void CreateDirectoryIfNotExists(string path)
		{
			if (!System.IO.Directory.Exists(path))
			{
				System.IO.Directory.CreateDirectory(path);
			}
		}

		private void CheckDatabaseConfig()
		{
			if (!new QueryManager().VerifyConnection())
			{
				MessageBox.Show("데이터베이스에 연결할 수 없습니다.");

				System.Diagnostics.Process.GetCurrentProcess().Kill();
			}
		}

		#endregion

		#region method (post-load)

		public void PostLoad()
		{

		}

		#endregion

	}
}
