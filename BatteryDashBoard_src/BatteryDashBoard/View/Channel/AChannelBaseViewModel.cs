//using Prism.Commands;
//using Prism.Mvvm;
//using RestApiLib.Defines;
//using SharedLib.Core;
//using SharedLib.Data.VM;
//using SharedLib.Defines;
//using SharedLib.View;
//using SQLManager.Data;
//using SQLManager.Data.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BatteryDashBoard.View.Channel
{
	public abstract class AChannelBaseViewModel //: BindableBase
	{
		#region property

		private byte m_ChannelIndex = 0xff;
		public byte ChannelIndex
		{
			get
			{
				System.Console.WriteLine($"AChannelBaseViewModel::ChannelIndex, value = {m_ChannelIndex}");
				return m_ChannelIndex;
			}
			set
			{
				m_ChannelIndex = value;
			//	RaisePropertyChanged("ChannelIndex");

				System.Console.WriteLine($"AChannelBaseViewModel::ChannelIndex, value = {m_ChannelIndex}");

				//ChannelVMInstance = value == 1 ? SharedPreferences.Instance.ChannelVMs.ElementAt(0) : SharedPreferences.Instance.ChannelVMs.ElementAt(1);

				if (value == 0)
				{
				//	ChannelVMInstance = SharedPreferences.Instance.ChannelVMs.ElementAt(0);
					System.Console.WriteLine($"AChannelBaseViewModel::ChannelIndex, ElementAt(0)");
				}
				else
                {
				//	ChannelVMInstance = SharedPreferences.Instance.ChannelVMs.ElementAt(1);
					System.Console.WriteLine($"AChannelBaseViewModel::ChannelIndex, ElementAt(1)");
				}
			}
		}

		public byte ChannelNum
		{
			get
			{
				byte num = (ChannelIndex == 0) ? (byte)1 : (byte)2;
				System.Console.WriteLine($"AChannelBaseViewModel::ChannelNum, value = {ChannelNum}");

				return num;
			}

		}

		protected bool IsRunLoadCommand { get; set; }


	//	protected ChannelViewModel ChannelVMInstance { get; set; }


		//public ChannelConfigVM ChannelConfig
		//{
		//	get
		//	{
		//		return ChannelVMInstance.ChannelConfig;
		//	}
		//}


		public int ChannelPosition
		{
			get
			{
				int num = ChannelIndex + 1;
				switch (num)
				{
					case 1: return 1;
					case 2: return 2;
					case 3: return 4;
					case 4: return 8;
					default: return 0;
				}
			}
		}


        public int ChannelFlag
        {
            get
            {
                if (ChannelIndex >= 4)
                    return 0;

                return ( 1 << ChannelIndex );   // 1, 2, 4, 8
            }
        }



  //      public ChannelDevice Devices
		//{
		//	get
		//	{
		//		return ChannelVMInstance.Devices;
		//	}
		//}


		//private pg_btry_info_VM m_SelectedBatteryInfo;
		//public pg_btry_info_VM SelectedBatteryInfo
		//{
		//	get
		//	{
		//		return m_SelectedBatteryInfo;
		//	}
		//	set
		//	{
		//		m_SelectedBatteryInfo = value;
		//		RaisePropertyChanged("SelectedBatteryInfo");

		//		SharedPreferences.Instance.SelectedBatteryMap[ChannelIndex] = value;
		//		UpdateBatteryInfo(value);
		//	}
		//}

		#endregion

		#region command

		/// <summary>
		/// loaded
		/// </summary>
		//public DelegateCommand<object> LoadedCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand<object>(delegate (object value)
		//		{
		//			Console.Out.WriteLine($"LoadedCommand value ==> {value.GetType()}");

		//			if (value is UserControl)
		//			{
		//				var uc = value as UserControl;
		//				ChannelIndex = Convert.ToByte(uc.Tag);

		//				Console.Out.WriteLine($"[debug] UserControl Tag, {uc.Tag}, ChannelIndex ==> {ChannelIndex}, #################");

		//				DoLoaded();
		//			}
		//			else
		//			{
		//				// TODO null handling
		//				Console.Out.WriteLine($"[Error] parameter as ChannelViewModel is null, {value.GetType().FullName}");
		//			}
		//		});
		//	}
		//}

		/// <summary>
		/// change progress state
		/// </summary>
		//public DelegateCommand<ProgressStateTypes?> ChangeProgressStateCommand
		//{
		//	get
		//	{
		//		return new DelegateCommand<ProgressStateTypes?>(delegate (ProgressStateTypes? value)
		//		{
		//			if (value != null)
		//			{
		//				ChannelVMInstance.CurrentProgressState = value.Value;
		//			}
		//		});
		//	}
		//}


		#endregion

		#region method

		public virtual void DoLoaded()
		{
			// 배터리 정보
		//	SelectedBatteryInfo = SharedPreferences.Instance.SelectedBatteryMap[ChannelIndex];
		}

	//	private void UpdateBatteryInfo(pg_btry_info_VM value)
		//{
			//if (ChannelVMInstance != null)
			//{
				//if (value != null)
				//{
				//	ChannelVMInstance.TaskRunInfo.BTRY_CODE = value.BTRY_CODE;
				//	ChannelVMInstance.TaskRunInfo.BTRY_TY = value.BTRY_TY;
				//	ChannelVMInstance.TaskRunInfo.MARK_CODE = value.MARK_CODE;
				//	ChannelVMInstance.TaskRunInfo.MAKR_DESC = value.MAKR_DESC;
				//	ChannelVMInstance.TaskRunInfo.MODEL_CODE = value.MODEL_CODE;
				//	ChannelVMInstance.TaskRunInfo.MODL_DESC = value.MODL_DESC;
				//	ChannelVMInstance.TaskRunInfo.CONFIG = value.CONFIG;
				//	ChannelVMInstance.TaskRunInfo.TYPE_P = value.TYPE_P;
				//	ChannelVMInstance.TaskRunInfo.TYPE_S = value.TYPE_S;
    //                ChannelVMInstance.TaskRunInfo.CELL_MIN_VLTGE = value.CELL_MIN_VLTGE;
    //                ChannelVMInstance.TaskRunInfo.CELL_MAX_VLTGE = value.CELL_MAX_VLTGE;
    //                ChannelVMInstance.TaskRunInfo.VLTGE = value.VLTGE;
				//	ChannelVMInstance.TaskRunInfo.CPCTY = value.CPCTY;
				//	ChannelVMInstance.TaskRunInfo.MUMM_VLTGE = value.MUMM_VLTGE;
				//	ChannelVMInstance.TaskRunInfo.MXMM_VLTGE = value.MXMM_VLTGE;
				//	ChannelVMInstance.TaskRunInfo.MUMM_VLTGE_LIMIT = value.MUMM_VLTGE_LIMIT;
				//	ChannelVMInstance.TaskRunInfo.BTRY_ENERGY = value.BTRY_ENERGY;
				//	ChannelVMInstance.TaskRunInfo.barcode = SharedPreferences.Instance.BatteryBarcodeMap[ChannelIndex];

				//	new tbl_task_run().UpdateBatteryInfo(ChannelVMInstance.TaskRunInfo);
				//}
				//else
				//{
				//	ChannelVMInstance.TaskRunInfo.BTRY_CODE = "";
				//	ChannelVMInstance.TaskRunInfo.BTRY_TY = "";
				//	ChannelVMInstance.TaskRunInfo.MARK_CODE = "";
				//	ChannelVMInstance.TaskRunInfo.MAKR_DESC = "";
				//	ChannelVMInstance.TaskRunInfo.MODEL_CODE = "";
				//	ChannelVMInstance.TaskRunInfo.MODL_DESC = "";
				//	ChannelVMInstance.TaskRunInfo.CONFIG = "";
				//	ChannelVMInstance.TaskRunInfo.TYPE_P = 0;
				//	ChannelVMInstance.TaskRunInfo.TYPE_S = 0;
    //                // Modified {
    //                ChannelVMInstance.TaskRunInfo.CELL_MIN_VLTGE = 0;
    //                ChannelVMInstance.TaskRunInfo.CELL_MAX_VLTGE = 0;
    //                // Modified }
    //                ChannelVMInstance.TaskRunInfo.VLTGE = 0;
				//	ChannelVMInstance.TaskRunInfo.CPCTY = 0;
				//	ChannelVMInstance.TaskRunInfo.MUMM_VLTGE = 0;
				//	ChannelVMInstance.TaskRunInfo.MXMM_VLTGE = 0;
				//	ChannelVMInstance.TaskRunInfo.MUMM_VLTGE_LIMIT = 0;
				//	ChannelVMInstance.TaskRunInfo.BTRY_ENERGY = 0;
				//	ChannelVMInstance.TaskRunInfo.barcode = SharedPreferences.Instance.BatteryBarcodeMap[ChannelIndex];

				//	new tbl_task_run().UpdateBatteryInfo(ChannelVMInstance.TaskRunInfo);
				//}
		//	} // end if
	//	}

		#endregion
	}
}
