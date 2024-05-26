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
using MaterialDesignThemes;
using MaterialDesignThemes.Wpf;
using Calendar = Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference.Calendar;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for CalendarsUserControl.xaml
    /// </summary>
    public partial class CalendarsUserControl : UserControl
    {
        private User user;
        private Calendar calendar;
        private CalendarServiceClient serviceClient;

        public CalendarsUserControl(ref User user)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.user = user;
            calendar = new Calendar();
            calendars.ItemsSource = user.Calendars;
            calendars.DisplayMemberPath = "CalendarName";
        }

        private void AddCalendar_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            calendars.Visibility = Visibility.Collapsed;
            yourCalsBtn.Visibility = Visibility.Visible;
            ucGrid.Children.Add(new AddCalendarUserControl(ref user));
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            calendars.Visibility = Visibility.Collapsed;
            yourCalsBtn.Visibility = Visibility.Visible;
            ucGrid.Children.Add(new AddEventUserControl(ref user, ref calendar));
        }

        private void EditCalendar_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            calendars.Visibility = Visibility.Collapsed;
            yourCalsBtn.Visibility = Visibility.Visible;
            ucGrid.Children.Add(new EditCalendarUserControl(ref user, ref calendar));
        }

        private void YourCalendars_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            calendars.ItemsSource = user.Calendars;
            CollectionViewSource.GetDefaultView(calendars.ItemsSource).Refresh();
            addEveBtn.Visibility = Visibility.Hidden;
            editCalBtn.Visibility = Visibility.Hidden;
            yourCalsBtn.Visibility = Visibility.Hidden;
            calendars.Visibility = Visibility.Visible;
        }

        private void calendars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            calendar = (sender as ComboBox).SelectedItem as Calendar;
            if (calendar != null)
            {
                try
                {
                    calendar.Users = serviceClient.GetCalendarUsers(calendar);
                    calendar.Events = serviceClient.GetCalendarEvents(calendar);
                    ucGrid.Children.Clear();
                    addEveBtn.Visibility = Visibility.Visible;
                    if (calendar.Creator.ID == user.ID) // only creator can edit calendar
                        editCalBtn.Visibility = Visibility.Visible;
                    yourCalsBtn.Visibility = Visibility.Visible;
                    calendars.Visibility = Visibility.Collapsed;
                    ucGrid.Children.Add(new DisplayCalendarUserControl(ref calendar, ref user));
                } catch { }
            }
        }
    }
}
