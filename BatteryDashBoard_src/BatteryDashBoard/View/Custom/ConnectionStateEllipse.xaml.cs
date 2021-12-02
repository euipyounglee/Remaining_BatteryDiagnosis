//using BaseLib.Defines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace BatteryDashBoard.View.Custom
{
	/// <summary>
	/// Interaction logic for ConnectionStateEllipse.xaml
	/// </summary>
	public partial class ConnectionStateEllipse : UserControl, INotifyPropertyChanged
	{
		public ConnectionStateEllipse()
		{
			//InitializeComponent();
		}

		#region bindable 

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string name)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(name));
			}
		}

		#endregion

		#region dependency property (IsOpened)

		private static void OnIsOpenedPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			if (sender is ConnectionStateEllipse)
			{
				var view = sender as ConnectionStateEllipse;
				view.OnPropertyChanged("IsOpened");
			}
		}

		//private static readonly DependencyProperty IsOpenedProperty = DependencyProperty.Register("IsOpened", typeof(ConnectionStates), typeof(ConnectionStateEllipse), new FrameworkPropertyMetadata
		//{
		//	PropertyChangedCallback = OnIsOpenedPropertyChanged,
		//	DefaultUpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
		//	BindsTwoWayByDefault = true
		//});
		//public ConnectionStates IsOpened
		//{
		//	get { return (ConnectionStates)GetValue(IsOpenedProperty); }
		//	set { SetValue(IsOpenedProperty, value); }
		//}

		#endregion
	}
}
