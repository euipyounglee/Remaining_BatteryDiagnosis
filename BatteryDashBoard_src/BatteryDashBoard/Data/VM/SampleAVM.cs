//using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDashBoard.Data.VM
{
	public class SampleAVM //: BindableBase
	{

		private int m_No;
		public int No
		{
			get
			{
				return m_No;
			}
			set
			{
				m_No = value;
				//RaisePropertyChanged("No");
			}
		}


		private string m_Mode;
		public string Mode
		{
			get
			{
				return m_Mode;
			}
			set
			{
				m_Mode = value;
				//RaisePropertyChanged("Mode");
			}
		}


		private string m_Device;
		public string Device
		{
			get
			{
				return m_Device;
			}
			set
			{
				m_Device = value;
				//RaisePropertyChanged("Device");
			}
		}


		private string m_Condition;
		public string Condition
		{
			get
			{
				return m_Condition;
			}
			set
			{
				m_Condition = value;
				//RaisePropertyChanged("Condition");
			}
		}


		private TimeSpan m_ElapsedTime;
		public TimeSpan ElapsedTime
		{
			get
			{
				return m_ElapsedTime;
			}
			set
			{
				m_ElapsedTime = value;
				//RaisePropertyChanged("ElapsedTime");
			}
		}


		private string m_State;
		public string State
		{
			get
			{
				return m_State;
			}
			set
			{
				m_State = value;
				//RaisePropertyChanged("State");
			}
		}

	}
}
