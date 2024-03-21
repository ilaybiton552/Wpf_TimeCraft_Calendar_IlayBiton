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
    /// Interaction logic for AddEventUserControl.xaml
    /// </summary>
    public partial class AddEventUserControl : UserControl
    {
        private User user;
        private Calendar calendar;
        private Event _event;
        private CalendarServiceClient serviceClient;
        public AddEventUserControl(ref User user, ref Calendar calendar)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.user = user;
            this.calendar = calendar;
            _event = new Event();
            this.DataContext = _event;
            _event.Users = new UserList();
            _event.Creator = user;
            AddUsers();
        }

        private void AddUsers()
        {
            UserList users = serviceClient.GetCalendarUsers(calendar);
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
            _event.Users.Add(user);
            foreach (CheckBox userCB in usersWP.Children)
            {
                if ((bool)userCB.IsChecked)
                {
                    _event.Users.Add(userCB.Tag as User);
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
            _event.EventName = string.Empty;
            _event.Data = string.Empty;
            _event.Users = new UserList();
            ClearChosenUsers();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearDetails();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            GetChosenUsers();
            int numOfRows = serviceClient.InsertEvent(_event);
            if (numOfRows < _event.Users.Count + 1)
            {
                MessageBox.Show("Error creating event");
                return;
            }
            MessageBox.Show("Created event Succesfully");
            user.Events = serviceClient.GetUserEvents(user);
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
