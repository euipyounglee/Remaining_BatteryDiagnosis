using Prism.Mvvm;
using RestApiLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class RestApiConfigVM : BindableBase
	{
		/// <summary>
		/// Portal의 REST API에 대한 Base Address
		/// </summary>
		public string PortBaseUri
		{
			get
			{
				return RestApiConfig.Instance.PortBaseUri;
			}
			set
			{
				RestApiConfig.Instance.PortBaseUri = value;
				RaisePropertyChanged("PortBaseUri");
			}
		}

		/// <summary>
		/// 검사장비 종류: P = Pack 검사장비, M = Module 검사장비
		/// </summary>
		public string DeviceType
		{
			get
			{
				return RestApiConfig.Instance.DeviceType;
			}
			set
			{
				RestApiConfig.Instance.DeviceType = value;
				RaisePropertyChanged("DeviceType");
			}
		}

		/// <summary>
		/// 검사장비 번호: Pack = 1 ~ 9, Module = 10 ~ 30
		/// </summary>
		public string DeviceNo
		{
			get
			{
				return RestApiConfig.Instance.DeviceNo;
			}
			set
			{
				RestApiConfig.Instance.DeviceNo = value;
				RaisePropertyChanged("DeviceNo");
			}
		}

	}
}
