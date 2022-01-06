using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectEngineLib.Data
{
	public class EngineConfig
	{
		#region property

		/// <summary>
		/// grade
		/// </summary>
		public GradeConfig Grade { get; set; }

		/// <summary>
		/// evaluation
		/// </summary>
		public EvaluationConfig Evaluation { get; set; }

		#endregion

		#region single-ton

		/// <summary>
		/// instance
		/// </summary>
		private static EngineConfig m_Instance;
		public static EngineConfig Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new EngineConfig();
					m_Instance.Init();
				}
				return m_Instance;
			}
		}

		/// <summary>
		/// private constructor
		/// </summary>
		private EngineConfig()
		{

		}

		/// <summary>
		/// init
		/// </summary>
		private void Init()
		{
			Grade = new GradeConfig();
			Evaluation = new EvaluationConfig();
		}

		#endregion
	}
}
