using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectEngineLib.Data
{
	public class EvaluationConfig
	{
		#region property

		public float SOC { get; set; }

		public float SOB { get; set; }

		public float SOH { get; set; }

		public float SOP { get; set; }

		#endregion

		#region method

		public EvaluationConfig()
		{
			SOC = 0;
			SOB = 40;
			SOH = 40;
			SOP = 20;
		}

		#endregion
	}
}
