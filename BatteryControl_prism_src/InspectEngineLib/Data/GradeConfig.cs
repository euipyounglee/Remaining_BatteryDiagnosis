using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectEngineLib.Data
{
	public class GradeConfig
	{
		#region property

		public float A { get; set; }

		public float B { get; set; }

		public float C { get; set; }

		public float D { get; set; }

		public float E { get; set; }

		#endregion

		#region method

		public GradeConfig()
		{
			A = 90;
			B = 80;
			C = 70;
			D = 60;
			E = 0;
		}

		#endregion
	}
}
