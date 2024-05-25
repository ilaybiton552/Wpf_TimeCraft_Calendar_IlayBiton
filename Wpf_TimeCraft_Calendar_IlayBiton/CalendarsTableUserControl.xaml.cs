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
        private CalendarServiceClient serviceClient;
        private CalendarList calendars;
        public CalendarsTableUserControl()
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            calendars = serviceClient.GetAllCalendars();
            foreach (Calendar calendar in calendars)
            {
                calendar.Events = serviceClient.GetCalendarEvents(calendar);
                calendar.Users = serviceClient.GetCalendarUsers(calendar);
            }
            calendarsListView.ItemsSource = calsCB.ItemsSource = calendars;
            calsCB.DisplayMemberPath = "CalendarName";
        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            ScrollBar scroll = sender as ScrollBar;
            viewer.ScrollToHorizontalOffset(5 * scroll.Value * (calendarsListView.ActualWidth - scroll.ActualWidth));
        }

        private void calsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Calendar calendar = (sender as ComboBox).SelectedItem as Calendar;
            if (calendar != null)
            {
                if (serviceClient.DeleteCalendar(calendar) == 1)
                {
                    calendars.RemoveAll(cal => cal.ID == calendar.ID);
                    MessageBox.Show("Deleted calendar successfully");
                    CollectionViewSource.GetDefaultView(calendarsListView.ItemsSource).Refresh();
                }
            }
        }
    }
}
