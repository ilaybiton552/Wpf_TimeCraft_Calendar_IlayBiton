using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for UsersTableUserControl.xaml
    /// </summary>
    public partial class UsersTableUserControl : UserControl
    {
        private CalendarServiceClient serviceClient;
        private UserList users;
        public UsersTableUserControl()
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            try
            {
                users = serviceClient.GetAllUsers();
                foreach (User user in users)
                {
                    user.Calendars = serviceClient.GetUserCalendars(user);
                    user.Events = serviceClient.GetUserEvents(user);
                }
            }
            catch { }
            usersListView.ItemsSource = users;
            usersCB.ItemsSource = users;
            usersCB.DisplayMemberPath = "Username";
        }
        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            ScrollBar scroll = sender as ScrollBar;
            viewer.ScrollToHorizontalOffset(2 * scroll.Value * (usersListView.ActualWidth - scroll.ActualWidth));
        }
        private void usersCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User user = (sender as ComboBox).SelectedItem as User;
            if (user != null)
            {
                try
                {
                    if (serviceClient.DeleteUser(user) == 1)
                    {
                        users.RemoveAll(usr => usr.ID == user.ID);
                        MessageBox.Show("Deleted user successfully");
                        CollectionViewSource.GetDefaultView(usersListView.ItemsSource).Refresh();
                    }
                } catch { }
            }
        }
    }
}
