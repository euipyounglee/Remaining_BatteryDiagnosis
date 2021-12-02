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

namespace BatteryDashBoard.View
{
	/// <summary>
	/// Interaction logic for DeviceSettingPopup.xaml
	/// </summary>
	public partial class DeviceSettingPopup : UserControl
	{
		public DeviceSettingPopup()
		{
			InitializeComponent();
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

		public bool callFileView(string strTitle, string menuName )
        {


			ConnectionManager wsServ = ConnectionManager.getInstance();

			string subTitle = string.Format("{0}-{1}:{2}/", strTitle, wsServ.getConnectIP(), wsServ.getConnectPort());

			ClassSettingView setup = new ClassSettingView(wsServ);
			return setup.CallFileView(subTitle, menuName);
        }
	}
}
