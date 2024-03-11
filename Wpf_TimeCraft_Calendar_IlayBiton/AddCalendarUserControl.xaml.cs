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

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for AddCalendarUserControl.xaml
    /// </summary>
    public partial class AddCalendarUserControl : UserControl
    {
        private User user;
        private CalendarServiceReference.Calendar calendar;
        private CalendarServiceClient serviceClient;
        public AddCalendarUserControl(User user)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.user = user;
            calendar = new CalendarServiceReference.Calendar();
            this.DataContext = calendar;
            calendar.Users = new UserList();
            calendar.Creator = user;
            AddUsers();
        }

        private void AddUsers()
        {
            UserList users = serviceClient.GetAllUsers();
            users.RemoveAll(x => x.ID == user.ID);
            foreach (User user in users) 
            {
                CheckBox userCB = new CheckBox();
                userCB.Margin = new Thickness(2.5);
                userCB.Content = user.Username;
                userCB.Tag = user;
                userCB.Style = FindResource("CheckBoxStyle") as Style;
                usersWP.Children.Add(userCB);
            }
        }

        private void GetChosenUsers()
        {
            calendar.Users.Add(user);
            foreach (CheckBox userCB in usersWP.Children) 
            {
                if ((bool)userCB.IsChecked)
                {
                    calendar.Users.Add(userCB.Tag as User);
                }
            }
        }

        private void ClearChosenUsers()
        {
            foreach (CheckBox userDB in usersWP.Children)
            {
                if ((bool)userDB.IsChecked)
                {
                    userDB.IsChecked = false;
                }
            }
        }

        private void ClearDetails()
        {
            calendar.CalendarName = string.Empty;
            calendar.Data = string.Empty;
            calendar.Users = new UserList();
            ClearChosenUsers();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearDetails();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            GetChosenUsers();
            int numOfRows = serviceClient.InsertCalendar(calendar);
            if (numOfRows < calendar.Users.Count + 1)
            {
                MessageBox.Show("Error creating calendar");
                return;
            }
            MessageBox.Show("Created Calendar Succesfully");
            user.Calendars = serviceClient.GetUserCalendars(user);
            ClearDetails();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.LineCount > 1) // if text starts wrapping
            {
                textBox.Height = Double.NaN;
            }
            else
            {
                textBox.Height = 25;
            }
        }
    }
}
