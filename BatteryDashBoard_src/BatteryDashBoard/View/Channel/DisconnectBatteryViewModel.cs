using BatteryDashBoard.Defines;
//using Prism.Commands;
//using RestApiLib.Defines;
//using SharedLib.Core;
//using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDashBoard.View.Channel
{
	public class DisconnectBatteryViewModel : AChannelBaseViewModel
	{
		#region property

		/// <summary>
		/// 배터리 타입
		/// </summary>
		public string BatteryTestType
		{
			get
			{
				return "";// SharedPreferences.Instance.LocalConfig.BatteryTestType;
			}
		}

		/// <summary>
		/// question 1
		/// </summary>
		private bool m_DBQuestion1A;
		public bool DBQuestion1A
		{
			get
			{
				return m_DBQuestion1A;
			}
			set
			{
				m_DBQuestion1A = value;
				//RaisePropertyChanged("DBQuestion1A");

				//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
				//{
				//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

				//	// RestApi : (P) EVAL_STEP.MSD_DISCON_2 - MSD 분리 여부
				//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
				//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
				//	{
				//		STEP_NO = $"{(int)EVAL_STEP.MSD_DISCON_2}",
				//		CHANNEL = $"{ChannelNum}",
				//		INPUT_VALUE = $"{value}",
				//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
				//	});
				//}

				//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
				//{
				//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

				//	// RestApi : (M) EVAL_STEP.VOLTAGE_03 - 종단부 전압 측정, 배터리 분리
				//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
				//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
				//	{
				//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_03}",
				//		CHANNEL = $"{ChannelNum}",
				//		INPUT_VALUE = $"{ManualVoltageInput3A}",
				//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
				//	});
				//}

				//if (m_DBQuestion1A == false && ChannelIndex == 0)
				//{
				//	ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData3;
				//	Devices.MultimeterInstance.SetInit();
				//	Devices.MultimeterInstance.GetVoltage();
				//}
			}
		}

		private bool m_DBQuestion1B;
		public bool DBQuestion1B
		{
			get
			{
				return m_DBQuestion1B;
			}
			set
			{
				m_DBQuestion1B = value;
				//RaisePropertyChanged("DBQuestion1B");

				//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
				//{
				//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

				//	// RestApi : (P) EVAL_STEP.MSD_DISCON_2 - MSD 분리 여부
				//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
				//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
				//	{
				//		STEP_NO = $"{(int)EVAL_STEP.MSD_DISCON_2}",
				//		CHANNEL = $"{ChannelNum}",
				//		INPUT_VALUE = $"{value}",
				//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
				//	});
				//}

				//if (ProgramParameters.MODULE.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
				//{
				//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

				//	// RestApi : (M) EVAL_STEP.VOLTAGE_03 - 종단부 전압 측정, 배터리 분리
				//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
				//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
				//	{
				//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_03}",
				//		CHANNEL = $"{ChannelNum}",
				//		INPUT_VALUE = $"{ManualVoltageInput3B}",
				//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
				//	});
				//}

				//if (m_DBQuestion1B == false && ChannelIndex == 1)
				//{
				//	ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData3;
				//	Devices.MultimeterInstance.SetInit();
				//	Devices.MultimeterInstance.GetVoltage();
				//}
			}
		}

		/// <summary>
		/// question 2
		/// </summary>
		private bool m_DBQuestion2A;
		public bool DBQuestion2A
		{
			get
			{
				return m_DBQuestion2A;
			}
			set
			{
				m_DBQuestion2A = value;
				//RaisePropertyChanged("DBQuestion2A");

				//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
				//{
				//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

				//	// RestApi : (P) EVAL_STEP.VOLTAGE_03 - 종단부 전압 측정
				//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
				//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
				//	{
				//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_03}",
				//		CHANNEL = $"{ChannelNum}",
				//		INPUT_VALUE = $"{ManualVoltageInput3A}",
				//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
				//	});
				//}
			}
		}

		private bool m_DBQuestion2B;
		public bool DBQuestion2B
		{
			get
			{
				return m_DBQuestion2B;
			}
			set
			{
				m_DBQuestion2B = value;
				//RaisePropertyChanged("DBQuestion2B");

				//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
				//{
				//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

				//	// RestApi : (P) EVAL_STEP.VOLTAGE_03 - 종단부 전압 측정
				//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
				//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
				//	{
				//		STEP_NO = $"{(int)EVAL_STEP.VOLTAGE_03}",
				//		CHANNEL = $"{ChannelNum}",
				//		INPUT_VALUE = $"{ManualVoltageInput3B}",
				//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
				//	});
				//}
			}
		}


		/// <summary>
		/// question 3
		/// </summary>
		private bool m_DBQuestion3A;
		public bool DBQuestion3A
		{
			get
			{
				return m_DBQuestion3A;
			}
			set
			{
				m_DBQuestion3A = value;
				//RaisePropertyChanged("DBQuestion3A");

				if (value)
				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : EVAL_STEP.HV_DISCON  - HV Junction Box 분리
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.HV_DISCON}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}
					
					// 다음 단계로
					IsEnabledGoToNextA = true;
				}
			}
		}

		private bool m_DBQuestion3B;
		public bool DBQuestion3B
		{
			get
			{
				return m_DBQuestion3B;
			}
			set
			{
				m_DBQuestion3B = value;
				//RaisePropertyChanged("DBQuestion3B");

				if (value)
				{
					//if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType) && value)
					//{
					//	byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;

					//	// RestApi : EVAL_STEP.HV_DISCON  - HV Junction Box 분리
					//	var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
					//	RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
					//	{
					//		STEP_NO = $"{(int)EVAL_STEP.HV_DISCON}",
					//		CHANNEL = $"{ChannelNum}",
					//		INPUT_VALUE = $"{value}",
					//		LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
					//	});
					//}

					// 다음 단계로
					IsEnabledGoToNextB = true;
				}
			}
		}


		private void PublichReceivedMultimeterMeasureData3(int ch, string data)
		{
			if ( ch == 0 )
			{
				ManualVoltageInput3A = data;
			}
			else if ( ch == 1 )
            {
				ManualVoltageInput3B = data;
			}
			//ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;

		}


		/// <summary>
		/// manual voltage input 3
		/// </summary>
		private string m_ManualVoltageInput3A = "0.0";
		public string ManualVoltageInput3A
		{
			get
			{
				return m_ManualVoltageInput3A;
			}
			set
			{
				m_ManualVoltageInput3A = value;
				//RaisePropertyChanged("ManualVoltageInput3A");

                //if (Double.Parse(m_ManualVoltageInput3A) < 1.0f)
                //{
                //    if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType))
                //    {
                //        DBQuestion2A = true;    // pack
                //    }
                //    else
                //    {
                //        DBQuestion1A = true;    // module
                //    }
                //}
              //  else
              //  {
                    //if (ProgramParameters.PACK.Equals(SharedPreferences.Instance.LocalConfig.BatteryTestType))
                    //{
                    //    DBQuestion2A = false;   // pack
                    //}
                    //else
                    //{
                    //    DBQuestion1A = false;   // module
                    //}
               // }
			}
		}

		private string m_ManualVoltageInput3B = "0.0";
		public string ManualVoltageInput3B
		{
			get
			{
				return m_ManualVoltageInput3B;
			}
			set
			{
				m_ManualVoltageInput3B = value;
				//RaisePropertyChanged("ManualVoltageInput3B");

                if (Double.Parse(m_ManualVoltageInput3B) < 1.0f)
                {
                    DBQuestion2B = true;
                }
                else
                {
                    DBQuestion2B = false;
                }
			}
		}


        private void PublichReceivedMultimeterMeasureData3A(int ch, string data)
        {
            System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] PublichReceivedMultimeterMeasureData1A");

            ManualVoltageInput3A = data;
            //ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;
        }

        private void PublichReceivedMultimeterMeasureData3B(int ch, string data)
        {
            System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] PublichReceivedMultimeterMeasureData1B");

            ManualVoltageInput3B = data;
            //ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = null;
        }


        /// <summary>
        /// go to next
        /// </summary>
        private bool m_IsEnabledGoToNextA = false;
		public bool IsEnabledGoToNextA
		{
			get
			{
				return m_IsEnabledGoToNextA;
			}
			set
			{
				m_IsEnabledGoToNextA = value;
				//RaisePropertyChanged("IsEnabledGoToNextA");
			}
		}

        /// <summary>
        /// go to next
        /// </summary>
        private bool m_IsEnabledGoToNextB = false;
        public bool IsEnabledGoToNextB
        {
            get
            {
                return m_IsEnabledGoToNextB;
            }
            set
            {
                m_IsEnabledGoToNextB = value;
                //RaisePropertyChanged("IsEnabledGoToNextB");
            }
        }
        #endregion

        #region command
        //public DelegateCommand BtnGetVoltageCommand3
        //{
        //    get
        //    {
        //        return new DelegateCommand(delegate ()
        //        {
        //            System.Console.WriteLine($"[{getChannelNum()}, {ChannelIndex}] BtnGetVoltageCommand3");

        //            if (ChannelIndex == 0)
        //            {
        //                ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData3A;
        //                Devices.MultimeterInstance.SetInit();
        //                Devices.MultimeterInstance.GetVoltage();
        //            }
        //            else if (ChannelIndex == 1)
        //            {
        //                ChannelVMInstance.ReceivedMultimeterMeasureDataCallback = PublichReceivedMultimeterMeasureData3B;
        //                Devices.MultimeterInstance.SetInit();
        //                Devices.MultimeterInstance.GetVoltage();
        //            }
        //        });
        //    }
        //}
        #endregion

        #region method

        public override void DoLoaded()
		{
			base.DoLoaded();

			ManualVoltageInput3A = "0";
			ManualVoltageInput3B = "0";

			DBQuestion1A = false;
			DBQuestion1B = false;

			DBQuestion2A = false;
			DBQuestion2B = false;

			DBQuestion3A = false;
			DBQuestion3B = false;
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
        #endregion
    }
}
