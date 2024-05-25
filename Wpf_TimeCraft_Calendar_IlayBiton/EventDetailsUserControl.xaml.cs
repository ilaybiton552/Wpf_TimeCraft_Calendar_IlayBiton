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
    /// Interaction logic for EventDetailsUserControl.xaml
    /// </summary>
    public partial class EventDetailsUserControl : UserControl
    {
        private Event _event;
        public EventDetailsUserControl(ref Event _event, User user)
        {
            InitializeComponent();
            this._event = _event;
            this.DataContext = _event;
            if (_event.EventType.Type == "Task")
            {
                tbDone.Visibility = Visibility.Visible;
                tbDone.Text = _event.IsDone ? "Done" : "Not Done";
            }
            if (_event.Creator.ID == user.ID || _event.Calendar.Creator.ID == user.ID) 
            {
                editButton.Visibility = Visibility.Visible;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            popup.Child = new EditEventUserControl(ref _event);
            popup.IsOpen = true;
        }

        private void popup_Closed(object sender, EventArgs e)
        {
            if (_event.EventType.Type == "Task")
            {
                tbDone.Visibility = Visibility.Visible;
                tbDone.Text = _event.IsDone ? "Done" : "Not Done";
            }
        }
    }
}
