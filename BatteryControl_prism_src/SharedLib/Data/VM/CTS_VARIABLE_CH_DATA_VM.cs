using PneCtsLib.Data.DTO;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class CTS_VARIABLE_CH_DATA_VM : BindableBase
	{
		#region property
		
		/// <summary>
		/// 기본 채널 데이터
		/// </summary>
		private CTS_CH_DATA_VM m_ChData;
		public CTS_CH_DATA_VM ChData
		{
			get
			{
				return m_ChData;
			}
			set
			{
				m_ChData = value;
				RaisePropertyChanged("ChData");
			}
		}
		
		/// <summary>
		/// Aux Data
		/// </summary>
		private ObservableCollection<CTS_AUX_DATA_VM> m_AuxData;
		public ObservableCollection<CTS_AUX_DATA_VM> AuxData
		{
			get
			{
				if (m_AuxData == null)
				{
					m_AuxData = new ObservableCollection<CTS_AUX_DATA_VM>();
				}
				return m_AuxData;
			}
			set
			{
				m_AuxData = value;
				RaisePropertyChanged("AuxData");
			}
		}
		
		/// <summary>
		/// Can Data
		/// </summary>
		private ObservableCollection<CTS_CAN_DATA_VM> m_CanData;
		public ObservableCollection<CTS_CAN_DATA_VM> CanData
		{
			get
			{
				if (m_CanData == null)
				{
					m_CanData = new ObservableCollection<CTS_CAN_DATA_VM>();
				}
				return m_CanData;
			}
			set
			{
				m_CanData = value;
				RaisePropertyChanged("CanData");
			}
		}


		#endregion

		#region method

		public CTS_VARIABLE_CH_DATA_VM(CTS_VARIABLE_CH_DATA_DTO dto)
		{
			Copy(dto);
		}

		public void Copy(CTS_VARIABLE_CH_DATA_DTO dto)
		{
			ChData = new CTS_CH_DATA_VM(dto.ChData);

			AuxData.Clear();
			foreach (var data in dto.AuxData)
			{
				AuxData.Add(new CTS_AUX_DATA_VM(data));
			}

			CanData.Clear();
			foreach (var data in dto.CanData)
			{
				CanData.Add(new CTS_CAN_DATA_VM(data));
			}
		}

		#endregion
	}
}
