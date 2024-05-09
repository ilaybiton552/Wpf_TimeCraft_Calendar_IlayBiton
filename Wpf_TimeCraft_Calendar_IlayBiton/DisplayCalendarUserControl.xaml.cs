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
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;
using Calendar = Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference.Calendar;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for DisplayCalendarUserControl.xaml
    /// </summary>
    public partial class DisplayCalendarUserControl : UserControl
    {
        private Calendar calendar;
        public DisplayCalendarUserControl(ref Calendar calendar)
        {
            InitializeComponent();
            this.calendar = calendar;
            grid.Children.Add(new CalendarUserControl(ref calendar));
        }
    }
}
