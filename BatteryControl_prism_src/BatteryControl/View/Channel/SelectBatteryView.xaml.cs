using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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

namespace BatteryControl.View.Channel
{
	/// <summary>
	/// Interaction logic for SelectBatteryView.xaml
	/// </summary>
	public partial class SelectBatteryView : UserControl
	{
		public SelectBatteryView()
		{
			InitializeComponent();
		}


        private void TextBox_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            string strBarCode = ((TextBox)sender).Text;

            Debug.WriteLine(strBarCode + ":00");
        }

      
    }
}
