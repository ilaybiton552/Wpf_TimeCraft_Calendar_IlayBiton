using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for EventsTableUserControl.xaml
    /// </summary>
    public partial class EventsTableUserControl : UserControl
    {
        private CalendarServiceClient serviceClient;
        private EventList events;
        public EventsTableUserControl()
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            try
            {
                eventsListView.ItemsSource = events = serviceClient.GetAllEvents();
            } catch { }
            eventsCB.ItemsSource = events;
            eventsCB.DisplayMemberPath = "ID";
        }
        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            ScrollBar scroll = sender as ScrollBar;
            viewer.ScrollToHorizontalOffset(2 * scroll.Value * (eventsListView.ActualWidth - scroll.ActualWidth + 10));
        }
        private void eventsCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Event _event = (sender as ComboBox).SelectedItem as Event;
            if (_event != null)
            {
                try
                {
                    if (serviceClient.DeleteEvent(_event) == 1)
                    {
                        events.RemoveAll(eve => eve.ID == _event.ID);
                        MessageBox.Show("Deleted event successfully");
                        CollectionViewSource.GetDefaultView(eventsListView.ItemsSource).Refresh();
                    }
                } catch { }
            }
        }
    }
}
