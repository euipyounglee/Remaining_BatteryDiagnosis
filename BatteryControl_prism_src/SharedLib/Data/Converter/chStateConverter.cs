using PneCtsLib.Defines;
using SharedLib.Data.VM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SharedLib.Data.Converter
{
	public class chStateConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			CTS_CH_DATA_VM chData = (CTS_CH_DATA_VM)value;
			switch (chData.ChState)
			{
				case Constants.PS_STATE_LINE_OFF: return "disconnected";
				case Constants.PS_STATE_LINE_ON: return "connected";
				case Constants.PS_STATE_READY: return "ready";
				case Constants.PS_STATE_IDLE: return "idle";
				case Constants.PS_STATE_STANDBY: return "standby";
				case Constants.PS_STATE_RUN:
					switch (chData.ChStepType)
					{
						case Constants.PS_STEP_NONE: return "run";
						case Constants.PS_STEP_CHARGE: return "Charge";
						case Constants.PS_STEP_DISCHARGE: return "Discharge";
						case Constants.PS_STEP_REST: return "Rest";
						case Constants.PS_STEP_OCV: return "OCV";
						case Constants.PS_STEP_IMPEDANCE: return "Impedance";
						case Constants.PS_STEP_END: return "End";
						case Constants.PS_STEP_LOOP: return "Loop";
						case Constants.PS_STEP_PATTERN: return "Pattern";
						default: return "run";
					} // end switch
				case Constants.PS_STATE_PAUSE: return "pause";
				case Constants.PS_STATE_MAINTENANCE: return "maintenance";
				case Constants.PS_STATE_END: return "Complete";
				default: return "disconnected";
			} // end switch
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
