using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for EventsTableUserControl.xaml
    /// </summary>
    public partial class EventsTableUserControl : UserControl
    {
        public EventsTableUserControl()
        {
            InitializeComponent();
            CalendarServiceClient serviceClient = new CalendarServiceClient();
            eventsListView.ItemsSource = serviceClient.GetAllEvents();
        }

        private void ScrollBar_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {
            ScrollBar scroll = sender as ScrollBar;
            viewer.ScrollToHorizontalOffset(5 * scroll.Value * (eventsListView.ActualWidth - scroll.ActualWidth));
        }
    }
}
