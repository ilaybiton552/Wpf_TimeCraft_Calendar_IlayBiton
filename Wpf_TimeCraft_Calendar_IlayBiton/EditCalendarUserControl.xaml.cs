﻿using System;
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
    /// Interaction logic for EditCalendarUserControl.xaml
    /// </summary>
    public partial class EditCalendarUserControl : UserControl
    {
        private User user;
        private Calendar calendar;
        private Calendar tempCal;
        private CalendarServiceClient serviceClient;
        public EditCalendarUserControl(ref User user, ref Calendar calendar)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.user = user;
            this.calendar = calendar;
            tempCal = new Calendar() { BaseColor = calendar.BaseColor, CalendarName = calendar.CalendarName,
                                       Data = calendar.Data, Users = calendar.Users, ID = calendar.ID};                          
            this.DataContext = tempCal;
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
                if (tempCal.Users.Where(usr=>usr.Username == user.Username).ToList().Count != 0) // checking calendar's users
                {
                    userCB.IsChecked = true;
                }
                usersWP.Children.Add(userCB);
            }
        }

        private void GetChosenUsers()
        {
            tempCal.Users.Clear();
            tempCal.Users.Add(user);
            foreach (CheckBox userCB in usersWP.Children)
            {
                if ((bool)userCB.IsChecked)
                {
                    tempCal.Users.Add(userCB.Tag as User);
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
            tempCal.CalendarName = string.Empty;
            tempCal.Data = string.Empty;
            tempCal.Users = new UserList();
            ClearChosenUsers();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearDetails();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            GetChosenUsers();
            int numOfRows = serviceClient.UpdateCalendar(tempCal);
            if (numOfRows < tempCal.Users.Count + 1)
            {
                MessageBox.Show("Error editing calendar");
                return;
            }
            MessageBox.Show("Edited Calendar Succesfully");
            calendar.CalendarName = tempCal.CalendarName;
            calendar.Data = tempCal.Data;
            calendar.Users = tempCal.Users;
            calendar.BaseColor = tempCal.BaseColor;
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