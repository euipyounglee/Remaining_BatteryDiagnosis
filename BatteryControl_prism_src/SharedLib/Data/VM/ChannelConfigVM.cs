using BaseLib.Net;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class ChannelConfigVM : BindableBase
	{
		#region property

		/// <summary>
		/// multimeter
		/// </summary>
		private HostInfo m_MultimeterHostInfo = new HostInfo
		{
			Host = "192.168.0.110",
			Port = 5025
		};
		public HostInfo MultimeterHostInfo
		{
			get
			{
				return m_MultimeterHostInfo;
			}
			set
			{
				m_MultimeterHostInfo = value;
				RaisePropertyChanged("MultimeterHostInfo");
			}
		}

		/// <summary>
		/// st5520 comport
		/// </summary>
		private string m_ST5520ComPort = "COM10";
		public string ST5520ComPort
		{
			get
			{
				return m_ST5520ComPort;
			}
			set
			{
				m_ST5520ComPort = value;
				RaisePropertyChanged("ST5520ComPort");
			}
		}

		/// <summary>
		/// relay controller comport
		/// </summary>
		private string m_RelayControllerComPort = "COM11";
		public string RelayControllerComPort
		{
			get
			{
				return m_RelayControllerComPort;
			}
			set
			{
				m_RelayControllerComPort = value;
				RaisePropertyChanged("RelayControllerComPort");
			}
		}

		/// <summary>
		/// barcode scanner comport
		/// </summary>
		private string m_BarcodeScannerComPort = "COM12";
		public string BarcodeScannerComPort
		{
			get
			{
				return m_BarcodeScannerComPort;
			}
			set
			{
				m_BarcodeScannerComPort = value;
				RaisePropertyChanged("BarcodeScannerComPort");
			}
		}

		/// <summary>
		/// bi box comport
		/// </summary>
		private string m_BIBoxComPort = "COM13";
		public string BIBoxComPort
		{
			get
			{
				return m_BIBoxComPort;
			}
			set
			{
				m_BIBoxComPort = value;
				RaisePropertyChanged("BIBoxComPort");
			}
		}

		/// <summary>
		/// acir host info
		/// </summary>
		private HostInfo m_ACIRHostInfo = new HostInfo
		{
			Host = "192.168.0.239",
			Port = 23
		};
		public HostInfo ACIRHostInfo
		{
			get
			{
				return m_ACIRHostInfo;
			}
			set
			{
				m_ACIRHostInfo = value;
				RaisePropertyChanged("ACIRHostInfo");
			}
		}

		#endregion
	}
}
