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
        private User user;
        private Event _event;
        private CalendarServiceClient serviceClient;
        public AddEventUserControl(ref User user, Calendar calendar)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.user = user;
            _event = new Event();
            this.DataContext = _event;
            _event.Calendar = calendar;
            _event.Users = serviceClient.GetCalendarUsers(calendar);
            _event.Creator = user;
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
            _event.StartDate = startDate.Date;
            _event.DueDate = dueDate.Date;
            _event.EventType = cmbTypes.SelectedItem as EventType;
            try
            {
                if (serviceClient.InsertEvent(_event) != 1)
                {
                    MessageBox.Show("Error creating event");
                    return;
                }
                MessageBox.Show("Created event Succesfully");
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
                textBox.Height = 25;
            }
        }

    }
}
