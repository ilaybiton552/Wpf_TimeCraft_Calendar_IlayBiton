using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            this.DataContext = _event;
            this._event = _event;
            EventTypeList eventTypes = serviceClient.GetAllEventTypes();
            cmbTypes.ItemsSource = eventTypes;
            cmbTypes.DisplayMemberPath = "Type";
            cmbTypes.SelectedItem = _event.EventType;
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
            try
            {
                if (serviceClient.UpdateEvent(tempEvent) != 1)
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
