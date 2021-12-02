using BatteryDashBoard.Defines;
//using RestApiLib.Defines;
//using SharedLib.Core;
//using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Prism.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace BatteryDashBoard.View.Channel
{
	public class ConnectBatteryViewModel : AChannelBaseViewModel
	{
		#region property

		/// <summary>
		/// 배터리 타입
		/// </summary>
		public string BatteryTestType
		{
			get
			{
				//System.Console.WriteLine($"[{ChannelIndex}] BatteryTestType, {SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex + 1).ToString() }");
				//return (SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex).ToString());

				string retChannelBatteryTestType = "";// SharedPreferences.Instance.LocalConfig.BatteryTestType;

				//Border b = FindVisualParentByName<Border>(this, "border1");
				//DependencyObject parent = FindVisualParent<UserControl>(BatteryDashBoard.View.Channel.ConnectBatteryView);

                //byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
#if false 
				if (ChannelIndex == 0)
				{
					// channel #1
					retChannelBatteryTestType += "0";
				}
				else
				{
					// channel #2
					retChannelBatteryTestType += "1";
				}
#endif
				//System.Console.WriteLine($"[{ChannelIndex}] BatteryTestType, {SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex + 1).ToString() }");
				//return (SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex + 1).ToString());


				System.Console.WriteLine($"[{ChannelIndex}] BatteryTestType ==> {retChannelBatteryTestType}");
				return retChannelBatteryTestType;
			}
		}


		/// <summary>
		/// question 1
		/// </summary>
		private bool m_CBQuestion1A;
		public bool CBQuestion1A
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion1A get, value = {m_CBQuestion1A}");
				return m_CBQuestion1A;
			}
			set
			{
				m_CBQuestion1A = value;
			
				//RaisePropertyChanged("CBQuestion1A");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion1A set, value = {value}");
				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.MSD_DISCON    - MSD 분리 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.MSD_DISCON}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}

					//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (M) EVAL_STEP.VOLTAGE_01   - 종단부 전압 zero 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_01}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{ManualVoltageInput1A}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}

		private bool m_CBQuestion1B;
		public bool CBQuestion1B
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] m_CBQuestion1 get, value = {m_CBQuestion1B}");
				return m_CBQuestion1B;
			}
			set
			{
				m_CBQuestion1B = value;

				//RaisePropertyChanged("CBQuestion1B");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] m_CBQuestion1 set, value = {value}");
				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.MSD_DISCON    - MSD 분리 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.MSD_DISCON}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}

					//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (M) EVAL_STEP.VOLTAGE_01   - 종단부 전압 zero 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_01}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{ManualVoltageInput1B}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}

		/// <summary>
		/// question 2
		/// </summary>
		private bool m_CBQuestion2A;
		private bool m_CBQuestion2B;
		public bool CBQuestion2A
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion2A get, value = {m_CBQuestion2A}");
				return m_CBQuestion2A;
			}
			set
			{
				m_CBQuestion2A = value;
				//RaisePropertyChanged("CBQuestion2A");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion2A set, value = {value}");
				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.VOLTAGE_01   - 종단부 전압 zero 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_01}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{ManualVoltageInput2A}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}

					//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (M) EVAL_STEP.BIBMS_CONN, BI 연결 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.BIBMS_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
				//hannelVMInstance.CurrentProgressState = ProgressStateTypes.SelectInspection;
			}
		}

		public bool CBQuestion2B
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion2B get, value = {m_CBQuestion2B}");
				return m_CBQuestion2B;
			}
			set
			{
				m_CBQuestion2B = value;
				//RaisePropertyChanged("CBQuestion2B");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion2B set, value = {value}");
				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.VOLTAGE_01   - 종단부 전압 zero 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_01}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{ManualVoltageInput2B}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}

					//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (M) EVAL_STEP.BIBMS_CONN, BI 연결 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.BIBMS_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
				//hannelVMInstance.CurrentProgressState = ProgressStateTypes.SelectInspection;
			}
		}

		/// <summary>
		/// manual voltage input
		/// </summary>
		private string m_ManualVoltageInput1A = "0.0";
		public string ManualVoltageInput1A
		{
			get
			{
				return m_ManualVoltageInput1A;
			}
			set
			{
				m_ManualVoltageInput1A = value;
				//RaisePropertyChanged("ManualVoltageInput1A");

                if (Double.Parse(m_ManualVoltageInput1A) < 1.0f)
                {
                    //if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType))
                    //{
                    //    CBQuestion2A = true;    // pack
                    //}
                    //else
                    //{
                    //    CBQuestion1A = true;    // module
                    //}
                }
                else
                {
                    //if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType))
                    //{
                    //    CBQuestion2A = false;    // pack
                    //}
                    //else
                    //{
                    //    CBQuestion1A = false;    // module
                    //}
                }
			}
		}

        public bool ValidateManualVoltageInput1A
        {
            get { return Double.Parse(m_ManualVoltageInput1A) < 1.0f; }
        }

        private string m_ManualVoltageInput1B = "0.0";
		public string ManualVoltageInput1B
		{
			get
			{
				return m_ManualVoltageInput1B;
			}
			set
			{
				m_ManualVoltageInput1B = value;
				//RaisePropertyChanged("ManualVoltageInput1B");

                if (Double.Parse(m_ManualVoltageInput1B) < 1.0f)
                {
                    CBQuestion1B = true;
                }
                else
                {
                    CBQuestion1B = false;
                }
			}
		}


        public bool ValidateManualVoltageInput1B
        {
            get { return Double.Parse(m_ManualVoltageInput1B) < 1.0f; }
        }

		/// <summary>
		/// question 3
		/// </summary>
		private bool m_CBQuestion3A;
		public bool CBQuestion3A
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion3A get, value = {m_CBQuestion3A}");
				return m_CBQuestion3A;
			}
			set
			{
				m_CBQuestion3A = value;
				//RaisePropertyChanged("CBQuestion3A");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion3A set, value = {value}");

				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.BIBMS_CONN - BI 연결 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.BIBMS_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}

					//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (M) EVAL_STEP.HV_CONN - HV Junction Box 연결
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.HV_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}

		private bool m_CBQuestion3B;
		public bool CBQuestion3B
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion3B get, value = {m_CBQuestion3B}");
				return m_CBQuestion3B;
			}
			set
			{
				m_CBQuestion3B = value;
				//RaisePropertyChanged("CBQuestion3B");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion3B set, value = {value}");

				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.BIBMS_CONN - BI 연결 확인
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.BIBMS_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}

					//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (M) EVAL_STEP.HV_CONN - HV Junction Box 연결
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.HV_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}

		/// <summary>
		/// question 4
		/// </summary>
		private bool m_CBQuestion4A;
		public bool CBQuestion4A
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion4A get, value = {m_CBQuestion4A}");
				return m_CBQuestion4A;
			}
			set
			{
				m_CBQuestion4A = value;
				//RaisePropertyChanged("CBQuestion4A");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion4A set, value = {value}");

				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.HV_CONN - HV Junction Box 연결
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.HV_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}

		private bool m_CBQuestion4B;
		public bool CBQuestion4B
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion4B get, value = {m_CBQuestion4B}");
				return m_CBQuestion4B;
			}
			set
			{
				m_CBQuestion4B = value;
				//RaisePropertyChanged("CBQuestion4B");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion4B set, value = {value}");

				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.HV_CONN - HV Junction Box 연결
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.HV_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}
		/// <summary>
		/// question 5
		/// </summary>
		private bool m_CBQuestion5A;
		public bool CBQuestion5A
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion5A get, value = {m_CBQuestion5A}");
				return m_CBQuestion5A;
			}
			set
			{
				m_CBQuestion5A = value;
				//RaisePropertyChanged("CBQuestion5A");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion5A set, value = {value}");

				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.MSD_CONN - MSD 연결
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.MSD_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}

		private bool m_CBQuestion5B;
		public bool CBQuestion5B
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion5B get, value = {m_CBQuestion5B}");
				return m_CBQuestion5B;
			}
			set
			{
				m_CBQuestion5B = value;
				//RaisePropertyChanged("CBQuestion5B");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion5B set, value = {value}");

				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : (P) EVAL_STEP.MSD_CONN - MSD 연결
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.MSD_CONN}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
				}
			}
		}

		/// <summary>
		/// question 6
		/// </summary>
		private bool m_CBQuestion6A;
		public bool CBQuestion6A
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion6A get, value = {m_CBQuestion6A}");
				return m_CBQuestion6A;
			}
			set
			{
				m_CBQuestion6A = value;
				//RaisePropertyChanged("CBQuestion6A");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion6A set, value = {value}");
			}
		}

		private bool m_CBQuestion6B;
		public bool CBQuestion6B
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion6B get, value = {m_CBQuestion6B}");
				return m_CBQuestion6B;
			}
			set
			{
				m_CBQuestion6B = value;
				//RaisePropertyChanged("CBQuestion6B");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion6B set, value = {value}");
			}
		}

		/// <summary>
		/// question 7
		/// </summary>
		private bool m_CBQuestion7A;
		public bool CBQuestion7A
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion7A get, value = {m_CBQuestion7A}");
				return m_CBQuestion7A;
			}
			set
			{
				m_CBQuestion7A = value;
				//RaisePropertyChanged("CBQuestion7A");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion7A set, value = {value}");

				// 다음 단계로
				if (value)
				{
					byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					// RestApi : (P) EVAL_STEP.VOLTAGE_02 - 종단부 전압 입력
					//var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//{
					//	STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_02}",
					//	CHANNEL = $"{ChannelNum}",
					//	INPUT_VALUE = $"{ManualVoltageInput2A}",
					//	LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//});

					IsEnabledGoToNext = true;
				}
			}
		}

		private bool m_CBQuestion7B;
		public bool CBQuestion7B
		{
			get
			{
				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion7B get, value = {m_CBQuestion7B}");
				return m_CBQuestion7B;
			}
			set
			{
				m_CBQuestion7B = value;
				//RaisePropertyChanged("CBQuestion7B");

				System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] CBQuestion7B set, value = {value}");

				// 다음 단계로
				if (value)
				{
					byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					// RestApi : (P) EVAL_STEP.VOLTAGE_02 - 종단부 전압 입력
					//var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//{
					//	STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_02}",
					//	CHANNEL = $"{ChannelNum}",
					//	INPUT_VALUE = $"{ManualVoltageInput2B}",
					//	LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//});

					IsEnabledGoToNext = true;
				}
			}
		}


		//--------------------------------------------------------------------------------------------------

		private void PublichReceivedMultimeterMeasureData1A(int ch, string data)
		{
			System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] PublichReceivedMultimeterMeasureData1A");

			ManualVoltageInput1A = data;
			//ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;
		}

		private void PublichReceivedMultimeterMeasureData1B(int ch, string data)
		{
			System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] PublichReceivedMultimeterMeasureData1B");

			ManualVoltageInput1B = data;
			//ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;
		}


		private void PublichReceivedMultimeterMeasureData2A(int ch, string data)
		{
			System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] PublichReceivedMultimeterMeasureData2A");

			ManualVoltageInput2A = data;
			//ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;
		}
		private void PublichReceivedMultimeterMeasureData2B(int ch, string data)
		{
			System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] PublichReceivedMultimeterMeasureData2B");

			ManualVoltageInput2B = data;
			//ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;
		}

		/// <summary>
		/// manual voltage input 2
		/// </summary>
		private string m_ManualVoltageInput2A = "0";
		public string ManualVoltageInput2A
		{
			get
			{
				return m_ManualVoltageInput2A;
			}
			set
			{
				m_ManualVoltageInput2A = value;
				//RaisePropertyChanged("ManualVoltageInput2A");

				try
				{
					float v = Convert.ToSingle(value);
					//IsQuestion7A_Enabled = v >= SelectedBatteryInfo.MUMM_VLTGE && v <= SelectedBatteryInfo.MXMM_VLTGE;
				}
				catch
				{
					IsQuestion7A_Enabled = false;
				}
			}
		}

		private string m_ManualVoltageInput2B = "0";
		public string ManualVoltageInput2B
		{
			get
			{
				return m_ManualVoltageInput2B;
			}
			set
			{
				m_ManualVoltageInput2B = value;
				//RaisePropertyChanged("ManualVoltageInput2B");

				try
				{
					float v = Convert.ToSingle(value);
					//IsQuestion7B_Enabled = v >= SelectedBatteryInfo.MUMM_VLTGE && v <= SelectedBatteryInfo.MXMM_VLTGE;
				}
				catch
				{
					IsQuestion7B_Enabled = false;
				}
			}
		}

		/// <summary>
		/// is question 7 enabled
		/// </summary>
		private bool m_IsQuestion7A_Enabled = false;
		public bool IsQuestion7A_Enabled
		{
			get
			{
				return m_IsQuestion7A_Enabled;
			}
			set
			{
				m_IsQuestion7A_Enabled = value;
				//RaisePropertyChanged("IsQuestion7A_Enabled");
			}
		}

		private bool m_IsQuestion7B_Enabled = false;
		public bool IsQuestion7B_Enabled
		{
			get
			{
				return m_IsQuestion7B_Enabled;
			}
			set
			{
				m_IsQuestion7B_Enabled = value;
				//RaisePropertyChanged("IsQuestion7B_Enabled");
			}
		}

		private bool m_IsPopup1A_Enabled = false;
		public bool IsPopup1A_Enabled
		{
			get
			{
				return m_IsPopup1A_Enabled;
			}
			set
			{
				m_IsPopup1A_Enabled = value;
				//RaisePropertyChanged("IsPopup1A_Enabled");
			}
		}

/*
        // unused
		private bool m_IsPopup1B_Enabled = false;
		public bool IsPopup1B_Enabled
		{
			get
			{
				return m_IsPopup1B_Enabled;
			}
			set
			{
				m_IsPopup1B_Enabled = value;
				RaisePropertyChanged("IsPopup1B_Enabled");
			}
		}
*/
		/// <summary>
		/// go to next
		/// </summary>
		private bool m_IsEnabledGoToNext = false;
		public bool IsEnabledGoToNext
		{
			get
			{
				return m_IsEnabledGoToNext;
			}
			set
			{
				m_IsEnabledGoToNext = value;
				//RaisePropertyChanged("IsEnabledGoToNext");
			}
		}


#endregion

#region command

		/// <summary>
		/// save channel config
		/// </summary>
		//public DelegateCommand BtnGetVoltageQuestion1Command
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{
		//			System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] BtnGetVoltageQuestion1Command");

		//			if (ChannelIndex == 0)
		//			{
		//				ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData1A;
		//				Devices.MultimeterInstance.SetInit();
		//				Devices.MultimeterInstance.GetVoltage();
		//			}
		//			else if (ChannelIndex == 1)
		//			{
		//				ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData1B;
		//				Devices.MultimeterInstance.SetInit();
		//				Devices.MultimeterInstance.GetVoltage();
		//			}
		//		});
		//	}
		//}


		//public DelegateCommand BtnGetVoltageQuestion4Command
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{
		//			System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] BtnGetVoltageQuestion4Command");

		//			if (ChannelIndex == 0)
		//			{
		//				ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData2A;
		//				Devices.MultimeterInstance.SetInit();
		//				Devices.MultimeterInstance.GetVoltage();
		//			}
		//			else if (ChannelIndex == 1)
		//			{
		//				ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData2B;
		//				Devices.MultimeterInstance.SetInit();
		//				Devices.MultimeterInstance.GetVoltage();
		//			}
		//		});
		//	}
		//}
#endregion // command

#region method

		public override void DoLoaded()
		{
			base.DoLoaded();

			System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] DoLoaded()");

			ManualVoltageInput1A = "0";
			//ManualVoltageInput1B = "0";
			ManualVoltageInput2A = "0";
			//ManualVoltageInput2B = "0";

			CBQuestion1A = false;
			CBQuestion2A = false;
			CBQuestion3A = false;
			CBQuestion4A = false;
			CBQuestion5A = false;
			CBQuestion6A = false;
			CBQuestion7A = false;

            ManualVoltageInput1B = "0";
            ManualVoltageInput2B = "0";

            CBQuestion1B = false;
			CBQuestion2B = false;
			CBQuestion3B = false;
			CBQuestion4B = false;
			CBQuestion5B = false;
			CBQuestion6B = false;
			CBQuestion7B = false;
		}

		public int getChannelNum()
        {
			if (ChannelIndex == 0)
			{
				// channel #1
				return 1;
			}
			else
			{
				// channel #2
				return 2;
			}
		}

		public static T FindVisualParent<T>(DependencyObject sender) where T : DependencyObject
		{
			if (sender == null)
			{
				return (null);
			}
			else if (VisualTreeHelper.GetParent(sender) is T)
			{
				return (VisualTreeHelper.GetParent(sender) as T);
			}
			else
			{
				DependencyObject parent = VisualTreeHelper.GetParent(sender);
				return (FindVisualParent<T>(parent));
			}
		}

		private T FindVisualParentByName<T>(FrameworkElement _Control, string _FindControlName) where T : FrameworkElement
		{
			T t = null; 
			DependencyObject obj = VisualTreeHelper.GetParent(_Control); //오브젝트검사 
			for (int i = 0; i < 100; i++) //최대 100개의 컨트롤 까지 검색 
			{ 
				string currentName = obj.GetValue(Control.NameProperty) as string; 
				if (currentName == _FindControlName) 
				{ 
					t = obj as T; 
					break; 
				} 
				obj = VisualTreeHelper.GetParent(obj); //1번 오브젝트 부모니깐 2번오브젝트 불러오기 계속 이런씩으로 검색 
				if (obj == null) //더이상 검사할 컨트롤 없다 빠져 나가자 ㅡ_ㅡ 
				{ 
					break; 
				} 
			} 
			return t; 
		}



#endregion // method
		}
}
