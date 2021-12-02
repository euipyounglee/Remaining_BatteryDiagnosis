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
using System.Windows.Resources;
//using System.Windows.Shapes;

namespace BatteryDashBoard.View
{
	/// <summary>
	/// Interaction logic for LoginView.xaml
	/// </summary>
	public partial class LoginView  :  System.Windows.Controls.UserControl
	{
		public LoginView()
		{
			InitializeComponent();
        }

		private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
		{
			//var dc = DataContext as MainViewModel;
			//if (dc != null)
			//{
			//	dc.LoginPassword = (sender as PasswordBox).Password;
			//}
		}

		private void UserControl_Loaded(object sender, RoutedEventArgs e)
		{
			TB1.Focus();
		}


		private void uiBtn_LoginButton_Click(object sender, RoutedEventArgs e)
		{
#if false


			//환경설정 화면 실행시키기

			Uri targetUri = new Uri("View/WorkspaceView.xaml", UriKind.Relative);

			StreamResourceInfo info = Application.GetResourceStream(targetUri);

			System.Windows.Markup.XamlReader reader = new System.Windows.Markup.XamlReader();

			//Page page = (Page)reader.LoadAsync(info.Stream);

			//	this.pageFrame.Content = page;

			//	this.DataContext = new WorkspaceView();// SomeViewViewModel();


			//	Uri uri = new Uri("/Mypage1.xaml", UriKind.Relative);
			//	NavigationService.Navigate(uri);

		//	NavigationService navigationService = page.NavigationService;


		//	(Application.Current.LoginView as LoginView).Navigate(targetUri);



			//	CurrentWorkspaceType = WorkspaceTypes.Main;
#else
			//L.E.P 임시 테스트 함수
			setupTest();
#endif

		}


		//환경설정 화면 실행시키기
		private void setupTest()
        {

			DeviceSettingPopup setup = new DeviceSettingPopup();

			setup.callFileView("DeashBoard", "Settting" );


		}


	}
}
