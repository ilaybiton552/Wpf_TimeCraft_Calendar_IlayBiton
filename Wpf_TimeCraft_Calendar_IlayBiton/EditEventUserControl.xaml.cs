﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for EditEventUserControl.xaml
    /// </summary>
    public partial class EditEventUserControl : UserControl
    {
        private Event _event;
        private Event tempEvent;
        private CalendarServiceClient serviceClient;
        public EditEventUserControl(ref Event _event)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            tempEvent = new Event() { ID = _event.ID, EventName = _event.EventName,
                                      Creator = _event.Creator, IsDone = _event.IsDone,
                                      StartDate = _event.StartDate, DueDate = _event.DueDate,
                                      Data = _event.Data, Calendar = _event.Calendar,
                                      EventType = _event.EventType } ;
            this.DataContext = tempEvent;
            this._event = _event;
            EventTypeList eventTypes = serviceClient.GetAllEventTypes();
            cmbTypes.ItemsSource = eventTypes;
            cmbTypes.DisplayMemberPath = "Type";
            cmbTypes.SelectedItem = eventTypes.FirstOrDefault(type => type.Type == tempEvent.EventType.Type);
            CheckTask();
            startDate.Date = tempEvent.StartDate;
            dueDate.Date = tempEvent.DueDate;
        }
        private void CheckTask()
        {
            if ((cmbTypes.SelectedItem as EventType).Type == "Task")
            {
                taskSP.Visibility = Visibility.Visible;
                checkboxDone.IsChecked = tempEvent.IsDone;
            }
        }
        private void ClearDetails()
        {
            tempEvent.EventName = string.Empty;
            tempEvent.Data = string.Empty;
            cmbTypes.SelectedItem = null;
            startDate.ClearDateChoice();
            dueDate.ClearDateChoice();
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearDetails();
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                tempEvent.StartDate = startDate.Date;
                tempEvent.DueDate = dueDate.Date;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            tempEvent.EventType = cmbTypes.SelectedItem as EventType;
            if (Validation.GetHasError(tbxEventName) || tempEvent.EventType == null)
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
            if (tempEvent.EventType.Type == "Task")
            {
                tempEvent.IsDone = (bool)checkboxDone.IsChecked;
            }
            try
            {
                if (serviceClient.UpdateEvent(ref tempEvent) != 1)
                {
                    MessageBox.Show("Error editing event");
                    return;
                }
                MessageBox.Show("Edited event Succesfully");
                _event.StartDate = tempEvent.StartDate;
                _event.DueDate = tempEvent.DueDate;
                _event.EventType = tempEvent.EventType;
                _event.Data = tempEvent.Data;
                _event.EventName = tempEvent.EventName;
                _event.IsDone = tempEvent.IsDone;
                _event.EventBackground = tempEvent.EventBackground;
                ClearDetails();
                UpdateGrid();
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
                textBox.Height = 30;
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (serviceClient.DeleteEvent(_event) == 1)
                {
                    UpdateGrid(_event);
                    MessageBox.Show("Deleted event");
                    ClearDetails();
                    (Parent as Popup).IsOpen = false;
                }
            }
            catch { }
        }
        private void UpdateGrid(Event _event = null)
        {
            var editPopup = Parent as Popup;
            var gridPopup = editPopup.Parent as Grid;
            var borderPopup = gridPopup.Parent as Border;
            var eventDetailUC = borderPopup.Parent as EventDetailsUserControl;
            var detailsPopup = eventDetailUC.Parent as Popup;
            gridPopup = detailsPopup.Parent as Grid;
            borderPopup = gridPopup.Parent as Border;
            var dayUserControl = borderPopup.Parent as CalendarDayUserControl;
            var wrapPanel = dayUserControl.Parent as WrapPanel;
            var grid = wrapPanel.Parent as Grid;
            CalendarUserControl calendarUserControl = grid.Parent as CalendarUserControl;
            if (_event != null)
            {
                calendarUserControl.calendar.Events.RemoveAll(item => item.ID == _event.ID);
            }
            calendarUserControl.LoadDays();
        }
        private void cmbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
                CheckTask();
        }
    }
}
