using System;
using System.Windows;
using System.Windows.Controls;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for DateTimePickerUserControl.xaml
    /// </summary>
    public partial class DateTimePickerUserControl : UserControl
    {
        private ComboBox cmbHours;
        private ComboBox cmbMinutes;
        private string hoursValue;
        private string minutesValue;
        private bool isChanged;
        public DateTime Date
        {
            get
            {
                var selectedDate = datePicker.SelectedDate;
                if (selectedDate != null)
                {
                    DateTime date = (DateTime)selectedDate;
                    CheckValidMinutes();
                    date = date.AddMinutes(int.Parse(minutesValue));
                    date = date.AddHours(int.Parse(hoursValue));
                    return date;
                }
                throw new Exception("Need to select date");
            }
            set 
            {
                hoursValue = value.Hour.ToString("00");
                minutesValue = value.Minute.ToString("00");
                datePicker.SelectedDate = value;
            }
        }
        public DateTimePickerUserControl()
        {
            InitializeComponent();
            isChanged = false;
            hoursValue = "00";
            minutesValue = "00";
        }
        private void CheckValidMinutes()
        {
            if (cmbMinutes != null)
            {
                int val = int.Parse(cmbMinutes.Text);
                if (val < 0 || val > 60)
                {
                    cmbMinutes.SelectedItem = "00";
                    minutesValue = "00";
                }
            }
        }
        private void SetHours()
        {
            for (int i = 0; i < 24; i++)
            {
                cmbHours.Items.Add(i.ToString("00"));
            }
            cmbHours.Text = hoursValue;
        }
        private void SetMinutes()
        {
            for (int i = 0; i < 60; i+=5)
            {
                cmbMinutes.Items.Add(i.ToString("00"));
            }
            cmbMinutes.Text = minutesValue;
            CheckValidMinutes();
        }
        private void Minutes_Loaded(object sender, RoutedEventArgs e)
        {
            cmbMinutes = sender as ComboBox;
            SetMinutes();
        }
        private void Hours_Loaded(object sender, RoutedEventArgs e)
        {
            cmbHours = sender as ComboBox;
            SetHours();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isChanged = true;
        }
        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (isChanged)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox.Name == "hours")
                {
                    hoursValue = comboBox.Text;
                }
                else
                {
                    minutesValue = comboBox.Text;
                    CheckValidMinutes();
                }
                isChanged = false;
            }
        }
        public void ClearDateChoice()
        {
            hoursValue = "00";
            minutesValue = "00";
            datePicker.SelectedDate = null;
        }
    }
}
