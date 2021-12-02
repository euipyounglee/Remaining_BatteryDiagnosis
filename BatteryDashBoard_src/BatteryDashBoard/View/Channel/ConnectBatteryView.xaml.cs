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
	/// Interaction logic for ConnectBatteryView.xaml
	/// </summary>
	public partial class ConnectBatteryView : UserControl
	{
		public ConnectBatteryView()
		{
			//DependencyObject parent = FindVisualParent<UserControl>(Application.Current.MainWindow);
			//33 object obj = this.Parent;

			//(DataContext as ChannelViewModel).ChannelIndex;

		//	InitializeComponent();

			//33 DependencyObject parent = FindVisualParent<UserControl>(this);

			//33 Window parentWindow = Window.GetWindow(this);
		}
/*
		public static T FindVisualParent<T>(DependencyObject sender) where T : DependencyObject
		{
			if (sender == null)
			{
				return (null);
			}
			else if (VisualTreeHelper.GetParent(sender) is T)
			{
				return (VisualTreeHelper.GetParent(sender) as T);
			}
			else
			{
				DependencyObject parent = VisualTreeHelper.GetParent(sender);
				if (parent == null)
					parent = LogicalTreeHelper.GetParent(sender);
				return (FindVisualParent<T>(parent));
			}
		}
*/



		/*
		public string BatteryTestType
		{
			get
			{
				//System.Console.WriteLine($"[{ChannelIndex}] BatteryTestType, {SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex + 1).ToString() }");
				//return (SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex).ToString());

				string retChannelBatteryTestType = SharedPreferences.Instance.LocalConfig.BatteryTestType;

				//Border b = FindVisualParentByName<Border>(this, "border1");
				//DependencyObject parent = FindVisualParent<UserControl>(BatteryDashBoard.View.Channel.ConnectBatteryView);

				//byte ChannelNum = (ChannelIndex == 0) ? (byte)1 : (byte)2;
				if (ChannelIndex == 0)
				{
					// channel #1
					retChannelBatteryTestType += "0";
				}
				else
				{
					// channel #2
					retChannelBatteryTestType += "1";
				}

				//System.Console.WriteLine($"[{ChannelIndex}] BatteryTestType, {SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex + 1).ToString() }");
				//return (SharedPreferences.Instance.LocalConfig.BatteryTestType + (ChannelIndex + 1).ToString());


				System.Console.WriteLine($"[{ChannelIndex}] BatteryTestType ==> {retChannelBatteryTestType}");

				return retChannelBatteryTestType;
			}
		}
		*/
	}
}
