//using BaseLib.Defines;
//using BaseLib.Pubsub;
//using BatteryDashBoard.Defines;
//using Prism.Commands;
//using Prism.Mvvm;
//using RelayBoxLib.Defines;
//using RestApiLib.Defines;
//using SharedLib.Core;
//using SharedLib.Defines;
//using SQLManager.Data;
//using SQLManager.Data.Query;
//using SQLManager.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDashBoard.View.Channel
{
	public class CheckConnectionViewModel : AChannelBaseViewModel
	{

		private const string INSULATION_TEST_BEGIN   = "절연저항을 확인 중 입니다.";
		private const string INSULATION_TEST_FAILURE = "절연저항을 확인해 주시기 바랍니다.";

		/// <summary>
		/// 장비연결 검사 진행률
		/// </summary>
		private double m_CurrentCheckDeviceProgressValue = 0;
		public double CurrentCheckDeviceProgressValue
		{
			get
			{
				return m_CurrentCheckDeviceProgressValue;
			}
			set
			{
				m_CurrentCheckDeviceProgressValue = value;
				//RaisePropertyChanged("CurrentCheckDeviceProgressValue");
			}
		}

		/// <summary>
		/// current device state
		/// </summary>
		//private ConnectionStates m_CurrentDeviceState = ConnectionStates.Disconnected;
		//public ConnectionStates CurrentDeviceState
		//{
		//	get
		//	{
		//		return m_CurrentDeviceState;
		//	}
		//	set
		//	{
		//		m_CurrentDeviceState = value;
		//		RaisePropertyChanged("CurrentDeviceState");
		//	}
		//}

		/// <summary>
		/// is checking
		/// </summary>
		private bool m_IsCheckingDeviceConnection = true;
		public bool IsCheckingDeviceConnection
		{
			get
			{
				return m_IsCheckingDeviceConnection;
			}
			set
			{
				m_IsCheckingDeviceConnection = value;
				//RaisePropertyChanged("IsCheckingDeviceConnection");
			}
		}

		/// <summary>
		/// current check device
		/// </summary>
		//private LogDev m_CurrentCheckDevice;
		//public LogDev CurrentCheckDevice
		//{
		//	get
		//	{
		//		return m_CurrentCheckDevice;
		//	}
		//	set
		//	{
		//		m_CurrentCheckDevice = value;
		//		RaisePropertyChanged("CurrentCheckDevice");
		//	}
		//}

		/// <summary>
		/// warning
		/// </summary>
		private string m_WarningMessage = "";
		public string WarningMessage
		{
			get
			{
				return m_WarningMessage;
			}
			set
			{
				m_WarningMessage = value;
				//RaisePropertyChanged("WarningMessage");
			}
		}

		/// <summary>
		/// insulation test value
		/// </summary>
		private string m_InsulationTestValue = "";
		public string InsulationTestValue
		{
			get
			{
				return m_InsulationTestValue;
			}
			set
			{
				m_InsulationTestValue = value;
				//RaisePropertyChanged("InsulationTestValue");
			}
		}


		//#region command

		/// <summary>
		/// TODO re-connect
		/// </summary>
		//public DelegateCommand ReconnectDeviceCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{

		//		});
		//	}
		//}

		/// <summary>
		/// restart check device
		/// </summary>
		//public DelegateCommand RestartCheckDeviceCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{
		//			DoCheckDevice();
		//		});
		//	}
		//}

		//#endregion

		//#region method

		/// <summary>
		/// loaded
		/// </summary>
		public override void DoLoaded()
		{
			byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
			// RestApi : EVAL_STEP.CHECK_CONN - 장비연결 시작
			//var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
			//RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
			//{
			//	STEP_NO = $"{(int)EVAL_STEP.CHECK_CONN}",
			//	CHANNEL = $"{ChannelNum}",
			//	INPUT_VALUE = "",
			//	LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
			//});

			//var now = DateTime.Now;
			//ChannelVMInstance.TaskRunInfo = new SQLManager.Data.DTO.tbl_task_run_DTO
			//{
			//	run_id = string.Format("{0:D4}{1:D2}{2:D2}{3:D2}{4:D2}{5:D2}_{6}_{7}", 
			//		now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second,
			//		ChannelNum,
			//		Guid.NewGuid()),
			//	BTRY_CODE = "",
			//	channel = ChannelNum,
			//	step = $"{ChannelVMInstance.CurrentProgressState}"
			//};
			//new tbl_task_run().Insert(ChannelVMInstance.TaskRunInfo);

			//DoCheckDevice();
		}

		/// <summary>
		/// do check device connection
		/// </summary>
		//private async void DoCheckDevice()
		//{
			// 장비 검사 큐 구성
			//var queue = new Queue<LogDev>();

			//// 디버깅 모드 시 검사할 장비 추가 안함
			//{
   //             queue.Enqueue(LogDev.Cycler);
   //             queue.Enqueue(LogDev.ST5520);
			//	queue.Enqueue(LogDev.RELAY_BOX);
   //             queue.Enqueue(LogDev.ACIR);
   //             queue.Enqueue(LogDev.BI_BOX);
   //             queue.Enqueue(LogDev.DS3678);
			//	queue.Enqueue(LogDev.MULTIMETER);
			//}

            // TODO 멀티미터 추후 추가
            //queue.Enqueue(LogDev.MULTIMETER);

   //         CurrentCheckDeviceProgressValue = 0;
			////CurrentDeviceState = ConnectionStates.Disconnected;
			//IsCheckingDeviceConnection = true;

			//Console.Out.WriteLine("[1] 장비 연결 검사 준비");

			//await Task.Run(() =>
			//{
			//	Console.Out.WriteLine("[2] 장비 연결 검사 시작");

			//	while (queue.Count > 0)
			//	{
			//		CurrentCheckDevice = queue.Dequeue();
			//		CurrentCheckDeviceProgressValue = (502 / 7) * (7 - queue.Count);
			//		CurrentDeviceState = ConnectionStates.Disconnected;

			//		switch (CurrentCheckDevice)
			//		{
			//			// 1. 절연저항시험기
			//			case LogDev.ST5520:
			//				if (ChannelConfig.ST5520ComPort != null && ChannelConfig.ST5520ComPort.Length > 0)
			//				{
			//					CurrentDeviceState = DoConnect(Devices.ST5520Instance, ChannelConfig.ST5520ComPort);
			//				}
			//				break;

			//			// 2. 릴레이 컨트롤러
			//			case LogDev.RELAY_BOX:
			//				if (ChannelConfig.RelayControllerComPort != null && ChannelConfig.RelayControllerComPort.Length > 0)
			//				{
			//					CurrentDeviceState = DoConnect(Devices.RelayBoxInstance, ChannelConfig.RelayControllerComPort);
			//				}
			//				break;

			//			// 3. ACIA
			//			case LogDev.ACIR:
			//				CurrentDeviceState = DoConnect(Devices.ACIRInstance, ChannelConfig.ACIRHostInfo);
			//				break;

			//			// 4. BI 컨트롤러
			//			case LogDev.BI_BOX:
			//				if (ChannelConfig.BIBoxComPort != null && ChannelConfig.BIBoxComPort.Length > 0)
			//				{
			//					CurrentDeviceState = DoConnect(Devices.BIBoxInstance, ChannelConfig.BIBoxComPort);
			//				}
			//				break;

			//			// 5. 충방전기
			//			case LogDev.Cycler:
			//				if (!DoConnectCTS(Devices.PNECTSInstance))
			//				{
			//					IsCheckingDeviceConnection = false;
			//					CurrentDeviceState = ConnectionStates.Disconnected;
			//					return;
			//				}
			//				break;

			//			// 6. 바코드 스캐너
			//			case LogDev.DS3678:
			//				if (ChannelConfig.BarcodeScannerComPort != null && ChannelConfig.BarcodeScannerComPort.Length > 0)
			//				{
			//					CurrentDeviceState = DoConnect(Devices.BarcodeScannerInstance, ChannelConfig.BarcodeScannerComPort);
			//				}
			//				break;

			//			// 7. 멀티미터
			//			case LogDev.MULTIMETER:
			//				CurrentDeviceState = DoConnect(Devices.MultimeterInstance, ChannelConfig.MultimeterHostInfo);
			//				break;

			//			default:
			//				CurrentDeviceState = ConnectionStates.Disconnected;
			//				break;
			//		} // end switch

			//		if (CurrentDeviceState != ConnectionStates.Connected)
			//		{
			//			break;
			//		}
			//		else
			//		{
			//			System.Threading.Thread.Sleep(1000);
			//		}

			//	} // while

			//	if (CurrentDeviceState == ConnectionStates.Connected)
			//	{
			//		ChannelVMInstance.CurrentProgressState = ProgressStateTypes.SelectBattery;
			//	}
			//	else
			//	{
			//		IsCheckingDeviceConnection = false;
			//	}
			//});

			//{
			//	ChannelVMInstance.CurrentProgressState = ProgressStateTypes.SelectBattery;
			//}

		//	Console.Out.WriteLine("[3] 장비 연결 검사 종료");
		//}

		/// <summary>
		/// try connect
		/// </summary>
		/// <param name="core"></param>
		//private ConnectionStates DoConnect<T>(ISubscribableBase<T> core, T data)
		//{
		//	if (core.ConnectionState == ConnectionStates.Disconnected)
		//	{
		//		if (!core.Connect(data))
		//		{
		//			return ConnectionStates.Disconnected;
		//		}
		//	}
		//	else if (core.ConnectionState == ConnectionStates.Connected)
		//	{
		//		return core.ConnectionState;
		//	}


  //          // ConnectionStates.Connecting 처리
  //          int timeout = 1000 * 10;
		//	while (timeout > 0)
		//	{
		//		if (core.ConnectionState == ConnectionStates.Connected)
		//		{
		//			return core.ConnectionState;
		//		}
		//		else
		//		{
		//			timeout -= 10;
		//			System.Threading.Thread.Sleep(10);
		//		}
		//	} // end while

		//	return ConnectionStates.Disconnected;
		//}

		/// <summary>
		/// try to connect relay
		/// 
		/// Off
		/// 
		/// Plus Mode
		/// 절연저항기 (+) 연결
		/// 절연저항기 측정
		/// 절연저항기(+) 분리
		/// 
		/// Minus Mode
		/// 절연저항기(-) 연결
		/// 절연저항기 측정
		/// 절연저항기(-) 분리
		/// 
		/// Off
		/// </summary>
		/// <param name="core"></param>
		/// <param name="comPort"></param>
		//private bool DoConnectRelayBox(RelayBoxLib.Core.Core core, string comPort)      // unused
		//{
		//	if (core.ConnectionState == ConnectionStates.Disconnected)
		//	{
		//		core.Connect(comPort);
		//	}
		//	CurrentDeviceState = core.ConnectionState;
		//	while (CurrentDeviceState == ConnectionStates.Connecting)
		//	{
		//		System.Threading.Thread.Sleep(10);
		//		CurrentDeviceState = core.ConnectionState;
		//	} // end while

		//	if (core.ConnectionState != ConnectionStates.Connected)
		//	{
		//		return false;
		//	}

		//	try
		//	{
		//		byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

		//		// 1. start ST5520 test
		//		var result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StartTest);

		//		// 2. Set Off
		//		if (!core.SetMode(ChannelNum, RelayBoxModes.Off))
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 3. measure (overflow)
		//		var measureResult = Devices.ST5520Instance.Measure(true);
		//		if (measureResult == null || !measureResult.Result)
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 4. Set Plus Mode
		//		if (core.SetMode(ChannelNum, RelayBoxModes.Plus))
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 5. measure (not overflow)
		//		measureResult = Devices.ST5520Instance.Measure(true);
		//		if (measureResult == null || !measureResult.Result)
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 6. Set Minus Mode
		//		if (core.SetMode(ChannelNum, RelayBoxModes.Minus))
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 7. measure (not overflow)
		//		measureResult = Devices.ST5520Instance.Measure(true);
		//		if (measureResult == null || !measureResult.Result)
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 8. Set Off
		//		if (core.SetMode(ChannelNum, RelayBoxModes.Off))
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 9. measure (overflow)
		//		measureResult = Devices.ST5520Instance.Measure(true);
		//		if (measureResult == null || !measureResult.Result)
		//		{
		//			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//			return false;
		//		}

		//		// 10. stop st5520 test
		//		result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);

		//		return true;
		//	}
		//	catch (Exception e)
		//	{
		//		Console.Out.WriteLine(e.ToString());

		//		var result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
		//		return false;
		//	}
		//}

		/// <summary>
		/// try to connect pns cts
		/// </summary>
		/// <param name="core"></param>
		//private bool DoConnectCTS(PneCtsLib.Core.Core core)
		//{
			//if (core.ConnectionState == ConnectionStates.Disconnected)
			//{
			//	core.Connect();
			//}

			//CurrentDeviceState = core.ConnectionState;

			//while (CurrentDeviceState == ConnectionStates.Connecting)
			//{
			//	System.Threading.Thread.Sleep(10);
			//	CurrentDeviceState = core.ConnectionState;
			//} // end while

			//if (core.ConnectionState == ConnectionStates.Connected)
			//{
#if false
                // 절연저항 검사
                if (!TestCtsInsulation())
				{
					return false;
				}

				//// 비절연 검사
				if (!TestCtsNonInsulation())
				{
					return false;
				}
#endif
			//	return true;
			//} // end if

			//return false;
		//}

		/// <summary>
		/// 절연 검사
		/// </summary>
		private bool TestCtsInsulation()
		{
#if false
            var result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StartTest);

			// TODO 충방전기 절연 api

			// 절연 검사
			System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				WarningMessage = $"[절연저항] {INSULATION_TEST_BEGIN}";
			});

			int voltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum);   // ChannelNum --> ChannelIndex
			var measureResult = Devices.ST5520Instance.Measure(true);
			if (measureResult == null || !measureResult.Result || voltage > 1)
			{
				System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
				{
					WarningMessage = $"[절연저항] {INSULATION_TEST_FAILURE}";
				});

				result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
				return false;
			}

			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
#else
            // 절연 검사
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
            {
                WarningMessage = $"[충방전기 절연] {INSULATION_TEST_BEGIN}";
            });

			byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
//			float voltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum) / 1000000.0f;  // ChannelNum --> ChannelIndex
//            if (voltage > 1.0f)
//            {
//                System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
//                {
//                    WarningMessage = $"[충방전기 절연] {INSULATION_TEST_FAILURE}";
//                });
//                return false;
//            }
#endif
            return true;
		}

		/// <summary>
		/// 비절연 검사
		/// </summary>
		/// <returns></returns>
		private bool TestCtsNonInsulation()
		{
#if false
			var result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StartTest);

			// TODO 비절연 api

			// 비절연 검사
			System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			{
				WarningMessage = $"[비절연] {INSULATION_TEST_BEGIN}";
			});

			int voltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum);        // ChannelNum --> ChannelIndex
			var measureResult = Devices.ST5520Instance.Measure(false);
			if (measureResult == null || !measureResult.Result || voltage < 1)
			{
				System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
				{
					WarningMessage = $"[비절연] {INSULATION_TEST_FAILURE}";
				});

				result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
				return false;
			}

			result = Devices.ST5520Instance.SendNonQueryCommand(ST5520Lib.Defines.NonQueryCommand.StopTest);
#else
            // 비절연 검사
            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
            {
                WarningMessage = $"[충방전기 비절연] {INSULATION_TEST_BEGIN}";
            });

			byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
			//float voltage = Devices.PNECTSInstance.CtsData.GetVoltage(ChannelNum) / 1000000.0f;     // ChannelNum --> ChannelIndex
   //         if (voltage < 1)
   //         {
   //             System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
   //             {
   //                 WarningMessage = $"[충방전기 비절연] {INSULATION_TEST_FAILURE}";
   //             });
   //             return false;
   //         }
#endif
            return true;
        }


	}
}
