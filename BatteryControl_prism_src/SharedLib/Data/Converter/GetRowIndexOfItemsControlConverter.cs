using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace SharedLib.Data.Converter
{
	public class GetRowIndexOfItemsControlConverter : IMultiValueConverter
	{
		public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
		{
			var vm = (UserControl)values[0];
			var list = (ItemsControl)values[1];
			int idx = list.ItemContainerGenerator.IndexFromContainer(vm);
			return $"{idx + 1}";
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
