using BatteryControl.Defines;
using RestApiLib.Defines;
using SharedLib.Core;
using SQLManager.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BatteryControl
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			var a = SharedPreferences.Instance;
			base.OnStartup(e);

            BaseLib.Helper.LogHelper.BuildVersion($"{ProgramParameters.BuildDate.Trim()}");
        }

		protected override void OnExit(ExitEventArgs e)
		{
            // RestApi : 프로그램 종료
            RestApiLib.Core.Helper.SendStep(RestApiLogTypes.Common, new SQLManager.Data.DTO.pg_btry_srvive_evl_input_DTO
			{
				STEP_NO = $"{(int)EVAL_STEP.EXIT}",
				LOGIN_ID = SharedPreferences.Instance.LoginUser.USER_ID
			});

			base.OnExit(e);
		}
	}
}
