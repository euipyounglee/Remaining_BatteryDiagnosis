//using BatteryDashBoard.Defines;
//using Prism.Commands;
//using Prism.Mvvm;
//using RestApiLib.Core;
//using RestApiLib.Data;
//using RestApiLib.Defines;
//using SharedLib.Core;
//using SharedLib.View;
//using SQLManager.Data;
//using SQLManager.Data.Query;
using BatteryDashBoard.Defines;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BatteryDashBoard
{
	public class MainViewModel //: BindableBase
	{
		#region property

		private const int HEARTBIT_DELAY = 1000 * 10;
		private const int TICK = 100;

		/// <summary>
		/// program running
		/// </summary>
		private bool IsProgramRunning { get; set; }


		// 1. delegate 선언
		private delegate void LoginCommand(int i);

		//private ICommand connectCommand;

		/// <summary>
		/// current workspace
		/// </summary>
		private WorkspaceTypes m_CurrentWorkspaceType;
		public WorkspaceTypes CurrentWorkspaceType
		{
			get
			{
				return m_CurrentWorkspaceType;
			}
			set
			{
				m_CurrentWorkspaceType = value;
				//RaisePropertyChanged("CurrentWorkspaceType");
			}
		}

		/// <summary>
		/// channels
		/// </summary>
		//public List<ChannelViewModel> Channels
		//{
		//	get
		//	{
		//		return SharedPreferences.Instance.ChannelVMs;
		//	}
		//}

		/// <summary>
		/// 타이틀
		/// </summary>
		//public string DisplayTitle
		//{
		//	get
		//	{
		//		return $"배터리 {SharedPreferences.Instance.LocalConfig.BatteryTestType} 성능평가 시스템";
		//	}
		//}

		#endregion

		#region property (login)

		/// <summary>
		/// login id
		/// </summary>
		private string m_LoginId = "";
		public string LoginId
		{
			get
			{
				return m_LoginId;
			}
			set
			{
				m_LoginId = value;
			//	RaisePropertyChanged("LoginId");
			}
		}

		/// <summary>
		/// login password
		/// </summary>
		public string LoginPassword { get; set; }

		#endregion




		#region command

		/// <summary>
		/// loaded
		/// </summary>
		//public DelegateCommand LoadedCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{
		//			IsProgramRunning = true;
		//			Task.Factory.StartNew(() =>
		//			{
		//				RestApi.SendHealthCheck(RestApiConfig.Instance.DeviceNo);

		//				int count = HEARTBIT_DELAY;
		//				while (IsProgramRunning)
		//				{
		//					if (count <= 0)
		//					{
		//						count = HEARTBIT_DELAY;

		//						RestApi.SendHealthCheck(RestApiConfig.Instance.DeviceNo);
		//					}
		//					else
		//					{
		//						count -= 100;
		//					}
		//					System.Threading.Thread.Sleep(100);
		//				} // end while
		//			});
		//		});
		//	}
		//}

		/// <summary>
		/// unloaded
		/// </summary>
		//public DelegateCommand UnloadedCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{
		//			IsProgramRunning = false;
		//		});
		//	}
		//}


		/// <summary>
		/// 로그인
		/// </summary>
		//public delegate LoginCommand
		//      {
		// // 2. delegate 인스턴스 생성
		//          LoginCommand run = new LoginCommand(RunThis);
		//}

		//public DelegateCommand LoginCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{

		//				if (LoginId == null || LoginId.Length == 0 || LoginPassword == null || LoginPassword.Length == 0)
		//				{
		//					MessageBox.Show("아이디 또는 비밀번호를 입력해주세요");
		//					return;
		//				}

		//				var result = new pg_user_view().Login(LoginId, LoginPassword);
		//				if (result == null)
		//				{
		//					MessageBox.Show("잘못된 아이디 또는 비밀번호입니다.");
		//					return;
		//				}

		//				SharedPreferences.Instance.LoginUser = result;


		//                  // RestApi : EVAL_STEP.LOGIN - 로그인
		//                  RestApiLib.Core.Helper.SendStep(RestApiLogTypes.Common, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
		//			{
		//				STEP_NO = $"{(int)EVAL_STEP.LOGIN}",
		//				LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
		//			});

		//			CurrentWorkspaceType = WorkspaceTypes.Main;
		//		});
		//	}
		//}


		//public ICommand ConnectCommand
		//{
		//	get
		//	{
		//		return (this.connectCommand) ??
		//			(this.connectCommand = new DelegateCommand(Connect));
		//	}
		//}


		public  void Connect()
		{

			CurrentWorkspaceType = WorkspaceTypes.Main;
			MessageBox.Show($"Connect");
		}


	
		//종료 버튼
		//public DelegateCommand<Window> ProgramExitCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand<Window>(delegate (Window window)
		//		{
		//			foreach (Window win in System.Windows.Application.Current.Windows)
		//			{
		//				if (win is MainWindow) continue;
		//				win.Close();
		//			}

		//			window.Close();
		//		});
		//	}
		//}


		#endregion

		#region method
		#endregion
	}
}
