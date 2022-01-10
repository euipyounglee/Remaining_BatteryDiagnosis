using BatteryControl.WebSocket;
using RestApiLib.Defines;
using SQLManager.Data.DTO;
using System;
using System.Collections.Generic;
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

namespace BatteryControl
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{

		private static ConnectionManager _connect = null;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount >= 2)
			{
				WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
			}
			else
			{
				DragMove();
			}
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

			ws_Loaded();

			DbConnect();

		}
		#region websocket_Server
		
		private void ws_Loaded()
		{

			// _wport = 0;// wport;
			//=========================================================================
			//    webServer.getInstance().ThreadStart(wport);
			//=========================================================================
			getConnect_Instance();

		}

		private void DbConnect()
		{
			string currentPath = System.IO.Directory.GetCurrentDirectory();

			ClassSqlite db = new ClassSqlite();
			db.CreateDataSqlite(currentPath);
		}


		public static ConnectionManager getConnect_Instance()
		{
			if (null == _connect) {
				_connect = ConnectionManager.getInstance();
			}

			return _connect;
		}

        #endregion


    }
}
