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

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for CalendarDayUserControl.xaml
    /// </summary>
    public partial class CalendarDayUserControl : UserControl
    {
        public CalendarDayUserControl(int day, Color color)
        {
            InitializeComponent();
            dayOfMonth.Text = day.ToString();
            textBorder.Background = new SolidColorBrush(color);
            textBorder.Background.Opacity = 0.5;
        }
    }
}
