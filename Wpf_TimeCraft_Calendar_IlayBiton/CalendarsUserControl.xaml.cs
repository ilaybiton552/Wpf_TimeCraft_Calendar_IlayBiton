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
        public CalendarsUserControl(User user)
        {
            InitializeComponent();
            this.user = user;
            AddCalendars();
        }

        private void AddCalendars()
        {
            foreach (var calendar in user.Calendars)
            {
                Button button = new Button();
                button.Content = calendar.CalendarName;
                button.Style = (Style)FindResource("ExapderButtonStyle");
                button.Click += Calendar_Click;
                button.Tag = calendar;
                calendars.Children.Add(button);
            }
        }

        private void Calendar_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            addEveBtn.Visibility = Visibility.Visible;
            editCalBtn.Visibility = Visibility.Visible;
            yourCalsBtn.Visibility = Visibility.Visible;
            expander.Visibility = Visibility.Collapsed;
            Calendar cal = (sender as Button).Tag as Calendar;
            ucGrid.Children.Add(new DisplayCalendarUserControl(ref cal));
        }

        private void AddCalendar_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            expander.Visibility = Visibility.Collapsed;
            yourCalsBtn.Visibility = Visibility.Visible;
            ucGrid.Children.Add(new AddCalendarUserControl(user));
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditCalendar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void YourCalendars_Click(object sender, RoutedEventArgs e)
        {
            ucGrid.Children.Clear();
            addEveBtn.Visibility = Visibility.Hidden;
            editCalBtn.Visibility = Visibility.Hidden;
            yourCalsBtn.Visibility = Visibility.Hidden;
            expander.Visibility = Visibility.Visible;
        }
    }
}
