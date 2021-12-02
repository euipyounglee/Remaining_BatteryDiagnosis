//using SharedLib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BatteryDashBoard.View.Channel
{
	/// <summary>
	/// Interaction logic for ConnectBatteryViewForModule.xaml
	/// </summary>
	public partial class ConnectBatteryViewForModule : UserControl
	{
		public ConnectBatteryViewForModule()
		{
		//	InitializeComponent();
		}

		private Regex regex = new Regex("[^0-9.-]+");

		private bool IsNumeric(string source)
		{
			return !regex.IsMatch(source);
		}

		private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = !IsNumeric(e.Text);
		}


		private void GotFocusStep1(object sender, RoutedEventArgs e)
		{
			//SharedPreferences.Instance.ChannelDevices.ElementAt(1).MultimeterInstance;
			
			TextBox tb = sender as TextBox;
			if (tb != null)
			{
				tb.SelectAll(); //select all text in TextBox
			}
		}
    }
}
