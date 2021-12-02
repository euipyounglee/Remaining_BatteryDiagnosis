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

namespace BatteryDashBoard.View.Channel
{
	/// <summary>
	/// Interaction logic for SimpleInspectionView.xaml
	/// </summary>
	public partial class SimpleInspectionView : UserControl
	{
		public SimpleInspectionView()
		{
			//InitializeComponent();

            //PopupPause.MouseMove += new MouseEventHandler(pop_MouseMove);
            //PopupIsolate.MouseMove += new MouseEventHandler(pop_MouseMove);
        }

		private void LV1_SizeChanged(object sender, SizeChangedEventArgs e)
		{
			//var cols = (LV1.View as GridView).Columns;
			//double width = (LV1.ActualWidth - 10) / cols.Count;

			//foreach (var col in cols)
			//{
			//	col.Width = width;
			//}
		}

        void pop_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //PopupIsolate.PlacementRectangle = new Rect(new Point(e.GetPosition(this).X, e.GetPosition(this).Y)
                //                                          , new Point(490, 305));

            }
        }
    }
}
