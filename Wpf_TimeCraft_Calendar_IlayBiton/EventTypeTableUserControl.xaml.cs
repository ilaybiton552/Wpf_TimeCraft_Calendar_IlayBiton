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
    /// Interaction logic for EventTypeTableUserControl.xaml
    /// </summary>
    public partial class EventTypeTableUserControl : UserControl
    {
        public EventTypeTableUserControl()
        {
            InitializeComponent();
            CalendarServiceClient serviceClient = new CalendarServiceClient();
            typesListView.ItemsSource = serviceClient.GetAllEventTypes();
        }
    }
}
