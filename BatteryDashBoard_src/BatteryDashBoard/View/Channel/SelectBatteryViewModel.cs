//using BaseLib.Pubsub;
//using BatteryDashBoard.Defines;
//using Prism.Commands;
//using RestApiLib.Defines;
//using SharedLib.Core;
//using SharedLib.Data.VM;
//using SQLManager.Data;
//using SQLManager.Data.DTO;
//using SQLManager.Data.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDashBoard.View.Channel
{
	public class SelectBatteryViewModel : AChannelBaseViewModel
	{
		#region property

		/// <summary>
		/// for filter
		/// </summary>
		private const string FIXED_TXT1 = "선택해주세요";

		/// <summary>
		/// battery types
		/// </summary>
		private ObservableCollection<string> m_BatteryTypes;
		public ObservableCollection<string> BatteryTypes
		{
			get
			{
				if (m_BatteryTypes == null)
				{
					m_BatteryTypes = new ObservableCollection<string>(new List<string>
					{
						//FIXED_TXT1,
						//"MODULE",
						//"PACK"
						//SharedPreferences.Instance.LocalConfig.BatteryTestType
					});

					SelectedBatteryType = m_BatteryTypes.ElementAt(0);
				}
				return m_BatteryTypes;
			}
			set
			{
				m_BatteryTypes = value;
				//RaisePropertyChanged("BatteryTypes");
			}
		}

		/// <summary>
		/// selected battery type
		/// </summary>
		private string m_SelectedBatteryType;
		public string SelectedBatteryType
		{
			get
			{
				return m_SelectedBatteryType;
			}
			set
			{
				m_SelectedBatteryType = value;
				//RaisePropertyChanged("SelectedBatteryType");

				InitBatteryMakers();
				InitBatteryModels();
				InitBatteryConfigs();
				//SelectedBatteryInfo = null;

				if (value != null && !FIXED_TXT1.Equals(value))
				{
					LoadBatteryMakers(value);
				}
			}
		}

		/// <summary>
		/// 제조사 목록
		/// </summary>
		//private ObservableCollection<pg_btry_info_VM> m_BatteryMakers;
		//public ObservableCollection<pg_btry_info_VM> BatteryMakers
		//{
		//	get
		//	{
		//		if (m_BatteryMakers == null)
		//		{
		//			m_BatteryMakers = new ObservableCollection<pg_btry_info_VM>();
		//		}
		//		return m_BatteryMakers;
		//	}
		//	set
		//	{
		//		m_BatteryMakers = value;
		//		RaisePropertyChanged("BatteryMakers");
		//	}
		//}

		/// <summary>
		/// selected 제조사
		/// </summary>
		//private pg_btry_info_VM m_SelectedBatteryMaker;
		//public pg_btry_info_VM SelectedBatteryMaker
		//{
		//	get
		//	{
		//		return m_SelectedBatteryMaker;
		//	}
		//	set
		//	{
		//		m_SelectedBatteryMaker = value;
		//		RaisePropertyChanged("SelectedBatteryMaker");

		//		InitBatteryModels();
		//		InitBatteryConfigs();
		//		SelectedBatteryInfo = null;

		//		// 새 배터리 모델
		//		if (value != null && value.MARK_CODE != null && value.MARK_CODE.Length > 0)
		//		{
		//			LoadBatteryModels(SelectedBatteryType, value.MARK_CODE);
		//		}
		//	}
		//}

		/// <summary>
		/// 배터리 모델
		/// </summary>
		//private ObservableCollection<pg_btry_info_VM> m_BatteryModels;
		//public ObservableCollection<pg_btry_info_VM> BatteryModels
		//{
		//	get
		//	{
		//		if (m_BatteryModels == null)
		//		{
		//			m_BatteryModels = new ObservableCollection<pg_btry_info_VM>();
		//		}
		//		return m_BatteryModels;
		//	}
		//	set
		//	{
		//		m_BatteryModels = value;
		//		RaisePropertyChanged("BatteryModels");
		//	}
		//}

		/// <summary>
		/// selected battery model
		/// </summary>
		//private pg_btry_info_VM m_SelectedBatteryModel;
		//public pg_btry_info_VM SelectedBatteryModel
		//{
		//	get
		//	{
		//		return m_SelectedBatteryModel;
		//	}
		//	set
		//	{
		//		m_SelectedBatteryModel = value;
		//		RaisePropertyChanged("SelectedBatteryModel");

		//		InitBatteryConfigs();
		//		SelectedBatteryInfo = null;

		//		// 새 배터리 구성
		//		if (value != null && value.MODEL_CODE != null && value.MODEL_CODE.Length > 0)
		//		{
		//			LoadBatteryConfigs(SelectedBatteryType, SelectedBatteryMaker.MARK_CODE, value.MODEL_CODE);
		//		}
		//	}
		//}

		/// <summary>
		/// 배터리 구성
		/// </summary>
		//private ObservableCollection<pg_btry_info_VM> m_BatteryConfigs;
		//public ObservableCollection<pg_btry_info_VM> BatteryConfigs
		//{
		//	get
		//	{
		//		if (m_BatteryConfigs == null)
		//		{
		//			m_BatteryConfigs = new ObservableCollection<pg_btry_info_VM>();
		//		}
		//		return m_BatteryConfigs;
		//	}
		//	set
		//	{
		//		m_BatteryConfigs = value;
		//		RaisePropertyChanged("BatteryConfigs");
		//	}
		//}

		/// <summary>
		/// selected battery config
		/// </summary>
		//private pg_btry_info_VM m_SelectedBatteryConfig;
		//public pg_btry_info_VM SelectedBatteryConfig
		//{
		//	get
		//	{
		//		return m_SelectedBatteryConfig;
		//	}
		//	set
		//	{
		//		m_SelectedBatteryConfig = value;
		//		RaisePropertyChanged("SelectedBatteryConfig");

		//		SelectedBatteryInfo = null;

		//		if (value != null && !FIXED_TXT1.Equals(value.CONFIG))
		//		{
		//			byte ChannelNum = ChannelIndex == 0 ? (byte) 1: (byte) 2;

		//			SelectedBatteryInfo = new pg_btry_info_VM(new pg_btry_info().Get(SelectedBatteryType, SelectedBatteryMaker.MARK_CODE, SelectedBatteryModel.MODEL_CODE, value.CONFIG));
  //                  // RestApi : EVAL_STEP.BATTERY  - 배터리 선택시 
  //                  var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
		//			RestApiLib.Core.Helper.SendStep(restApiLogType, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
		//			{
		//				STEP_NO = $"{(int)EVAL_STEP.BATTERY}",
		//				CHANNEL = $"{ChannelNum}",
		//				INPUT_VALUE = $"{SharedPreferences.Instance.LocalConfig.BatteryTestType};{SelectedBatteryInfo.MAKR_DESC};{SelectedBatteryInfo.MODL_DESC};{SelectedBatteryInfo.CONFIG}",
		//				LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
		//			});
		//		}
		//	}
		//}

		/// <summary>
		/// battery barcode
		/// </summary>
		private string m_BatteryBarcode;
		public string BatteryBarcode
		{
			get
			{
				return m_BatteryBarcode;
			}
			set
			{
				m_BatteryBarcode = value;
				//RaisePropertyChanged("BatteryBarcode");
			}
		}

		#endregion

		#region command

		/// <summary>
		/// unloaded
		/// </summary>
		//public DelegateCommand UnloadedCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand(delegate ()
		//		{
		//			Devices.BarcodeScannerInstance.Unsubscribe(BarcodeScannerPushCallback);
		//		});
		//	}
		//}


		#endregion

		#region method

		public override void DoLoaded()
		{
			// barcode scanner 셋업
			//Devices.BarcodeScannerInstance.Subscribe(BarcodeScannerPushCallback);
			
			//// 배터리 정보 초기화
			//SelectedBatteryInfo = null;

			InitBatteryMakers();
			InitBatteryModels();
			InitBatteryConfigs();

			SelectedBatteryType = BatteryTypes.ElementAt(0);
		}

		/// <summary>
		/// load battery makers
		/// </summary>
		private void LoadBatteryMakers(string BTRY_TY)
		{
			//BatteryMakers.Clear();

			//List<pg_btry_info_VM> list = new List<pg_btry_info_VM>(new pg_btry_info_VM[]
			//{
			//	new pg_btry_info_VM(new pg_btry_info_DTO
			//	{
			//		MARK_CODE = "",
			//		MAKR_DESC = FIXED_TXT1
			//	})
			//});

			//var dtos = new pg_btry_info().GetMakers(BTRY_TY);
			//foreach (var dto in dtos)
			//{
			//	list.Add(new pg_btry_info_VM(dto));
			//}

			//BatteryMakers = new ObservableCollection<pg_btry_info_VM>(list);
			//SelectedBatteryMaker = BatteryMakers.ElementAt(0);
		}

		/// <summary>
		/// load battery models
		/// </summary>
		/// <param name="MARK_CODE"></param>
		private void LoadBatteryModels(string BTRY_TY, string MARK_CODE)
		{
			//BatteryModels.Clear();

			//List<pg_btry_info_VM> list = new List<pg_btry_info_VM>(new pg_btry_info_VM[] {
			//	new pg_btry_info_VM(new pg_btry_info_DTO
			//	{
			//		MODEL_CODE = "",
			//		MODL_DESC = FIXED_TXT1
			//	})
			//});

			//var dtos = new pg_btry_info().GetModels(BTRY_TY, MARK_CODE);
			//foreach (var dto in dtos)
			//{
			//	list.Add(new pg_btry_info_VM(dto));
			//}

			//BatteryModels = new ObservableCollection<pg_btry_info_VM>(list);
			//SelectedBatteryModel = BatteryModels.ElementAt(0);
		}

		/// <summary>
		/// load battery configs
		/// </summary>
		/// <param name="MARK_CODE"></param>
		/// <param name="MODEL_CODE"></param>
		private void LoadBatteryConfigs(string BTRY_TY, string MARK_CODE, string MODEL_CODE)
		{
			//BatteryConfigs.Clear();

			//List<pg_btry_info_VM> list = new List<pg_btry_info_VM>(new pg_btry_info_VM[] {
			//	new pg_btry_info_VM(new pg_btry_info_DTO
			//	{
			//		CONFIG = FIXED_TXT1
			//	})
			//});

			//var dtos = new pg_btry_info().GetConfigs(BTRY_TY, MARK_CODE, MODEL_CODE);
			//foreach (var dto in dtos)
			//{
			//	list.Add(new pg_btry_info_VM(dto));
			//}

			//BatteryConfigs = new ObservableCollection<pg_btry_info_VM>(list);
			//SelectedBatteryConfig = BatteryConfigs.ElementAt(0);
		}

		/// <summary>
		/// init battery maker
		/// </summary>
		private void InitBatteryMakers()
		{
			//SelectedBatteryMaker = null;
			//BatteryMakers.Clear();
			//BatteryMakers.Add(new pg_btry_info_VM(new pg_btry_info_DTO
			//{
			//	MARK_CODE = "",
			//	MAKR_DESC = FIXED_TXT1
			//}));
			//SelectedBatteryMaker = BatteryMakers.ElementAt(0);
		}

		/// <summary>
		/// init battery model
		/// </summary>
		private void InitBatteryModels()
		{
			//SelectedBatteryModel = null;
			//BatteryModels.Clear();
			//BatteryModels.Add(new pg_btry_info_VM(new pg_btry_info_DTO
			//{
			//	MODEL_CODE = "",
			//	MODL_DESC = "선택해주세요"
			//}));
			//SelectedBatteryModel = BatteryModels.ElementAt(0);
		}

		/// <summary>
		/// init battery config
		/// </summary>
		private void InitBatteryConfigs()
		{
			//SelectedBatteryConfig = null;
			//BatteryConfigs.Clear();
			//BatteryConfigs.Add(new pg_btry_info_VM(new pg_btry_info_DTO
			//{
			//	CONFIG = "선택해주세요"
			//}));
			//SelectedBatteryConfig = BatteryConfigs.ElementAt(0);
		}

		#endregion

		#region callback

		//private void BarcodeScannerPushCallback(PushDataDTO dto)
		//{
			//System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (Action)delegate
			//{
			//	switch (dto.PushDataType)
			//	{
			//		case PushDataDTO.PushDataTypes.Log:
			//			break;
			//		case PushDataDTO.PushDataTypes.Open:
			//			break;
			//		case PushDataDTO.PushDataTypes.Close:
			//			break;
			//		case PushDataDTO.PushDataTypes.Data:

			//			byte ChannelNum = ChannelIndex == 0 ? (byte)1 : (byte)2;

			//			SharedPreferences.Instance.BatteryBarcodeMap[ChannelIndex] = $"{dto.Data}";
			//			BatteryBarcode = SharedPreferences.Instance.BatteryBarcodeMap[ChannelIndex];

   //                     // RestApi : EVAL_STEP.BARCODE_SEL - 바코드 입력시
   //                     var restApiLogType = ChannelNum == 1 ? RestApiLogTypes.Channel1 : RestApiLogTypes.Channel2;
			//			RestApiLib.Core.Helper.SendStep(restApiLogType, new pg_btry_srvive_evl_input_DTO
			//			{
			//				STEP_NO = $"{(int)EVAL_STEP.BARCODE_SEL}",
			//				CHANNEL = $"{ChannelNum}",
			//				INPUT_VALUE = $"{BatteryBarcode}",
			//				LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
			//			});

			//			break;
			//		default:
			//			break;
			//	} // end switch
			//});
		//}

		#endregion
	}
}
