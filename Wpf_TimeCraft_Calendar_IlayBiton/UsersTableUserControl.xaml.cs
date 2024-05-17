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

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for UsersTableUserControl.xaml
    /// </summary>
    public partial class UsersTableUserControl : UserControl
    {
        public UsersTableUserControl()
        {
            InitializeComponent();
            CalendarServiceClient serviceClient = new CalendarServiceClient();
            UserList users = serviceClient.GetAllUsers();
            foreach (User user in users) 
            {
                user.Calendars = serviceClient.GetUserCalendars(user);
                user.Events = serviceClient.GetUserEvents(user);
            }
            usersListView.ItemsSource = users;
        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            ScrollBar scroll = sender as ScrollBar;
            viewer.ScrollToHorizontalOffset(scroll.Value * (usersListView.ActualWidth - scroll.ActualWidth));
        }
    }
}
