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
    /// Interaction logic for NavigationBarUserControl.xaml
    /// </summary>
    public partial class NavigationBarUserControl : UserControl
    {
        private Window currWindow;
        private User user;
        private Grid grid;
        private CalendarServiceClient serviceClient;
        public NavigationBarUserControl(ref User user, ref Grid ucGrid)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            currWindow = Application.Current.MainWindow;
            this.user = user;
            this.grid = ucGrid;
            grid.Children.Add(new HomeUserControl());
        }

        private void TB_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Background = Brushes.LightBlue;
        }

        private void TB_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#10C4FF");
        }

        private void Login_Down(object sender, MouseButtonEventArgs e)
        {
            LoginWindow nextWindow = new LoginWindow(ref user);
            nextWindow.Left = currWindow.Left + (currWindow.Width - nextWindow.Width) / 2;
            nextWindow.Top = currWindow.Top + (currWindow.Height - nextWindow.Height) / 2;
            nextWindow.ShowDialog();
            if (!string.IsNullOrWhiteSpace(user.Username)) UserLogged();
        }

        private void SignUp_Down(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow nextWindow = new SignUpWindow(ref user);
            nextWindow.Left = currWindow.Left + (currWindow.Width - nextWindow.Width) / 2;
            nextWindow.Top = currWindow.Top + (currWindow.Height - nextWindow.Height) / 2;
            nextWindow.ShowDialog();
            if (!string.IsNullOrWhiteSpace(user.Username)) UserLogged();
        }

        private void Home_Down(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(new HomeUserControl());
        }

        public void UserLogged()
        {
            login.Visibility = Visibility.Collapsed;
            signup.Visibility = Visibility.Collapsed;
            calendars.Visibility = Visibility.Visible;
            events.Visibility = Visibility.Visible;
            settings.Visibility = Visibility.Visible;
            logout.Visibility = Visibility.Visible;
            tbHello.Text = "Hello " + user.Username;
        }

        private void Calendar_Down(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Clear();
            grid.Children.Add(new CalendarsUserControl(ref user));
        }

        private void Logout_Down(object sender, MouseButtonEventArgs e)
        {
            user = new User();
            login.Visibility = Visibility.Visible;
            signup.Visibility = Visibility.Visible;
            calendars.Visibility = Visibility.Collapsed;
            events.Visibility = Visibility.Collapsed;
            settings.Visibility = Visibility.Collapsed;
            logout.Visibility = Visibility.Collapsed;
            grid.Children.Clear();
            grid.Children.Add(new HomeUserControl());
            tbHello.Text = "Hello Guest";
        }

        private void Event_Down(object sender, MouseButtonEventArgs e)
        {
            grid.Children.Clear();
            Calendar calendar = new Calendar();
            calendar.BaseColor = Color.FromRgb(0, 0, 0);
            calendar.Events = user.Events;
            grid.Children.Add(new CalendarUserControl(ref calendar, ref user, false));
        }

        private void Settings_Down(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow update = new SignUpWindow(ref user, true);
            update.Left = currWindow.Left + (currWindow.Width - update.Width) / 2;
            update.Top = currWindow.Top + (currWindow.Height - update.Height) / 2;
            update.ShowDialog();
            if (string.IsNullOrWhiteSpace(user.Username)) Logout_Down(null, null);
        }
    }
}
