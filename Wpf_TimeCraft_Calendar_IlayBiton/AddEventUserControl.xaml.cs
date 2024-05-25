using MaterialDesignThemes.Wpf;
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
            _event.Users = serviceClient.GetCalendarUsers(this.calendar);
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
            try
            {
                if (serviceClient.InsertEvent(_event) != 1)
                {
                    MessageBox.Show("Error creating event");
                    return;
                }
                MessageBox.Show("Created event Succesfully");
                calendar.Events.Add(_event);
                user.Events.Add(_event);
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
