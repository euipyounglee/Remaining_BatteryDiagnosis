using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class SampleBVM : BindableBase
	{
		#region property

		private DateTime m_InspectionDate;
		public DateTime InspectionDate
		{
			get
			{
				return m_InspectionDate;
			}
			set
			{
				m_InspectionDate = value;
				RaisePropertyChanged("InspectionDate");
			}
		}


		private string m_Model;
		public string Model
		{
			get
			{
				return m_Model;
			}
			set
			{
				m_Model = value;
				RaisePropertyChanged("Model");
			}
		}


		private string m_Channel;
		public string Channel
		{
			get
			{
				return m_Channel;
			}
			set
			{
				m_Channel = value;
				RaisePropertyChanged("Channel");
			}
		}


		private string m_InspectionType;
		public string InspectionType
		{
			get
			{
				return m_InspectionType;
			}
			set
			{
				m_InspectionType = value;
				RaisePropertyChanged("InspectionType");
			}
		}

		private string m_BatteryCode;
		public string BatteryCode
		{
			get
			{
				return m_BatteryCode;
			}
			set
			{
				m_BatteryCode = value;
				RaisePropertyChanged("BatteryCode");
			}
		}


		private TimeSpan m_StartTime;
		public TimeSpan StartTime
		{
			get
			{
				return m_StartTime;
			}
			set
			{
				m_StartTime = value;
				RaisePropertyChanged("StartTime");
			}
		}


		private TimeSpan m_EndTime;
		public TimeSpan EndTime
		{
			get
			{
				return m_EndTime;
			}
			set
			{
				m_EndTime = value;
				RaisePropertyChanged("EndTime");
			}
		}


		public TimeSpan RunningTime
		{
			get
			{
				return EndTime - StartTime;
			}
		}


		private string m_SOC;
		public string SOC
		{
			get
			{
				return m_SOC;
			}
			set
			{
				m_SOC = value;
				RaisePropertyChanged("SOC");
			}
		}


		private string m_SOH;
		public string SOH
		{
			get
			{
				return m_SOH;
			}
			set
			{
				m_SOH = value;
				RaisePropertyChanged("SOH");
			}
		}


		private string m_SOP;
		public string SOP
		{
			get
			{
				return m_SOP;
			}
			set
			{
				m_SOP = value;
				RaisePropertyChanged("SOP");
			}
		}


		private string m_SOB;
		public string SOB
		{
			get
			{
				return m_SOB;
			}
			set
			{
				m_SOB = value;
				RaisePropertyChanged("SOB");
			}
		}


		private bool m_IsExpanded;
		public bool IsExpanded
		{
			get
			{
				return m_IsExpanded;
			}
			set
			{
				m_IsExpanded = value;
				RaisePropertyChanged("IsExpanded");
			}
		}

		#endregion

		#region command


		public DelegateCommand ToggleExpandCommand
		{
			get
			{
				return new DelegateCommand(delegate ()
				{
					IsExpanded = !IsExpanded;
				});
			}
		}


		#endregion

	}
}
