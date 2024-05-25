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
        private CalendarServiceClient serviceClient;
        private EventTypeList types;
        public EventTypeTableUserControl()
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            types = serviceClient.GetAllEventTypes();
            typesListView.ItemsSource = types;
            typessCB.ItemsSource = types;
            typessCB.DisplayMemberPath = "Type";
        }

        private void typessCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventType type = (sender as ComboBox).SelectedItem as EventType;
            if (type != null)
            {
                if (serviceClient.DeleteEventType(type) == 1)
                {
                    types.RemoveAll(evType => evType.ID == type.ID);
                    MessageBox.Show("Deleted type successfully");
                    CollectionViewSource.GetDefaultView(typesListView.ItemsSource).Refresh();
                }
            }
        }
    }
}
