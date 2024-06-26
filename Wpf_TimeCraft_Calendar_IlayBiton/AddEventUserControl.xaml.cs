﻿using System;
using System.Windows;
using System.Windows.Controls;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;
using Calendar = Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference.Calendar;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for AddEventUserControl.xaml
    /// </summary>
    public partial class AddEventUserControl : UserControl
    {
        private Calendar calendar;
        private Event _event;
        private CalendarServiceClient serviceClient;
        private User user;
        public AddEventUserControl(ref User user, ref Calendar calendar)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.calendar = calendar;
            _event = new Event();
            this.DataContext = _event;
            _event.Calendar = this.calendar;
            _event.Creator = user;
            this.user = user;
            EventTypeList eventTypes = serviceClient.GetAllEventTypes();
            cmbTypes.ItemsSource = eventTypes;
            cmbTypes.DisplayMemberPath = "Type";
        }
        private void ClearDetails()
        {
            _event.EventName = string.Empty;
            _event.Data = string.Empty;
            cmbTypes.SelectedItem = null;
            startDate.ClearDateChoice();
            dueDate.ClearDateChoice();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearDetails();
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _event.StartDate = startDate.Date;
                _event.DueDate = dueDate.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            _event.EventType = cmbTypes.SelectedItem as EventType;
            if (Validation.GetHasError(tbxEventName) || _event.EventType == null)
            {
                MessageBox.Show("Fields can't remian empty");
                return;
            }
            if (_event.StartDate > _event.DueDate)
            {
                MessageBox.Show("End date can't be 'older' than start date");
                return;
            }
            if (_event.Data == string.Empty || _event.Data == null)
            {
                MessageBox.Show("Event's Data can't remain empty");
                return;
            }
            try
            {
                if (serviceClient.InsertEvent(_event) != 1)
                {
                    MessageBox.Show("Error creating event");
                    return;
                }
                MessageBox.Show("Created event Succesfully");
                calendar.Events = serviceClient.GetCalendarEvents(calendar);
                user.Events = serviceClient.GetUserEvents(user);
                ClearDetails();
            }
            catch { }
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
                textBox.Height = 40;
            }
        }
    }
}
