using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryDashBoard.Defines
{
	public class ProgramParameters
	{
		/// <summary>
		/// 모듈 상수
		/// </summary>
		public const string MODULE = "MODULE";

		/// <summary>
		/// 팩 상수
		/// </summary>
		public const string PACK = "PACK";


        public static string BuildDate = Properties.Resources.ResourceManager.GetObject("BuildDate").ToString();
    }
}
