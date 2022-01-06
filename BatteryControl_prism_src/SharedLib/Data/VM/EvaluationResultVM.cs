using InspectEngineLib.Defines;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class EvaluationResultVM : BindableBase
	{
		/// <summary>
		/// grade
		/// </summary>
		private GradeTypes m_Grade = GradeTypes.F;
		public GradeTypes Grade
		{
			get
			{
				return m_Grade;
			}
			set
			{
				m_Grade = value;
				RaisePropertyChanged("Grade");
			}
		}

		/// <summary>
		/// soc
		/// </summary>
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

		/// <summary>
		/// soh
		/// </summary>
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

		/// <summary>
		/// sop
		/// </summary>
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

		/// <summary>
		/// sob
		/// </summary>
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

		/// <summary>
		/// 배터리 제조사
		/// </summary>
		private string m_BatteryMaker;
		public string BatteryMaker
		{
			get
			{
				return m_BatteryMaker;
			}
			set
			{
				m_BatteryMaker = value;
				RaisePropertyChanged("BatteryMaker");
			}
		}

		/// <summary>
		/// 차량 모델
		/// </summary>
		private string m_ModelDesc;
		public string ModelDesc
		{
			get
			{
				return m_ModelDesc;
			}
			set
			{
				m_ModelDesc = value;
				RaisePropertyChanged("ModelDesc");
			}
		}

		/// <summary>
		/// 배터리 구성
		/// </summary>
		private string m_BatteryConfig;
		public string BatteryConfig
		{
			get
			{
				return m_BatteryConfig;
			}
			set
			{
				m_BatteryConfig = value;
				RaisePropertyChanged("BatteryConfig");
			}
		}

		/// <summary>
		/// 바코드
		/// </summary>
		private string m_Barcode;
		public string Barcode
		{
			get
			{
				return m_Barcode;
			}
			set
			{
				m_Barcode = value;
				RaisePropertyChanged("Barcode");
			}
		}

		/// <summary>
		/// 검사타입
		/// </summary>
		private string m_EvaluationType;
		public string EvaluationType
		{
			get
			{
				return m_EvaluationType;
			}
			set
			{
				m_EvaluationType = value;
				RaisePropertyChanged("EvaluationType");
			}
		}

		/// <summary>
		/// 검사일자
		/// </summary>
		private DateTime m_EvaluationDate;
		public DateTime EvaluationDate
		{
			get
			{
				return m_EvaluationDate;
			}
			set
			{
				m_EvaluationDate = value;
				RaisePropertyChanged("EvaluationDate");
			}
		}

		/// <summary>
		/// 소요시간
		/// </summary>
		private TimeSpan m_TimeRequired;
		public TimeSpan TimeRequired
		{
			get
			{
				return m_TimeRequired;
			}
			set
			{
				m_TimeRequired = value;
				RaisePropertyChanged("TimeRequired");
			}
		}

		/// <summary>
		/// 시작시간
		/// </summary>
		private DateTime m_BeginDate;
		public DateTime BeginDate
		{
			get
			{
				return m_BeginDate;
			}
			set
			{
				m_BeginDate = value;
				RaisePropertyChanged("BeginDate");
			}
		}

		/// <summary>
		/// 종료시간
		/// </summary>
		private DateTime m_EndDate;
		public DateTime EndDate
		{
			get
			{
				return m_EndDate;
			}
			set
			{
				m_EndDate = value;
				RaisePropertyChanged("EndDate");
			}
		}


	}
}
