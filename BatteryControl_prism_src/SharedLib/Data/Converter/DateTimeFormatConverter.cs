using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SharedLib.Data.Converter
{
	public class DateTimeFormatConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			DateTime v = (DateTime)value;
			if ($"Date".Equals(parameter))
			{
				return string.Format("{0:D4}.{1:D2}.{2:D2}", v.Year, v.Month, v.Day);
			}
			else
			{
				return string.Format("{0:D4}.{1:D2}.{2:D2} {3:D2}:{4:D2}:{5:D2}",
				v.Year, v.Month, v.Day, v.Hour, v.Minute, v.Second);
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
