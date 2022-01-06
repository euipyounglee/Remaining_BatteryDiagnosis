using InspectEngineLib.Data;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.Data.VM
{
	public class EngineConfigVM : BindableBase
	{
		/// <summary>
		/// evaluation config
		/// </summary>
		public EvaluationConfig EvaluationConfig
		{
			get
			{
				return EngineConfig.Instance.Evaluation;
			}
			set
			{
				EngineConfig.Instance.Evaluation = value;
				RaisePropertyChanged("EvaluationConfig");
			}
		}

		/// <summary>
		/// grade config
		/// </summary>
		public GradeConfig GradeConfig
		{
			get
			{
				return EngineConfig.Instance.Grade;
			}
			set
			{
				EngineConfig.Instance.Grade = value;
				RaisePropertyChanged("GradeConfig");
			}
		}

	}
}
