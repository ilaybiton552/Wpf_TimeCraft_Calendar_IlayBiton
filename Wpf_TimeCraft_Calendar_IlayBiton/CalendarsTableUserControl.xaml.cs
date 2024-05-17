using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for CalendarsTableUserControl.xaml
    /// </summary>
    public partial class CalendarsTableUserControl : UserControl
    {
        public CalendarsTableUserControl()
        {
            InitializeComponent();
            CalendarServiceClient serviceClient = new CalendarServiceClient();
            CalendarList calendars = serviceClient.GetAllCalendars();
            foreach (Calendar calendar in calendars)
            {
                calendar.Events = serviceClient.GetCalendarEvents(calendar);
                calendar.Users = serviceClient.GetCalendarUsers(calendar);
            }
            calendarsListView.ItemsSource = calendars;
        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            ScrollBar scroll = sender as ScrollBar;
            viewer.ScrollToHorizontalOffset(scroll.Value * (calendarsListView.ActualWidth - scroll.ActualWidth));
        }
    }
}
