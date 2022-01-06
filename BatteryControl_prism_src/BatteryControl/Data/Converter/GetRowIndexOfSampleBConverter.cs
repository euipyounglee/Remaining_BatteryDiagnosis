using BatteryControl.Data.VM;
using SharedLib.Data.VM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BatteryControl.Data.Converter
{
	public class GetRowIndexOfSampleBConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var vm = (SampleBVM)values[0];
			var list = (IList<SampleBVM>)values[1];
			return $"{list.IndexOf(vm) + 1}. ";
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
