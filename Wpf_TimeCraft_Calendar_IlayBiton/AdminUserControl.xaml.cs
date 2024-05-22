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
    /// Interaction logic for AdminUserControl.xaml
    /// </summary>
    public partial class AdminUserControl : UserControl
    {
        public AdminUserControl()
        {
            InitializeComponent();
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            tablesGrid.Children.Clear();
            tablesGrid.Children.Add(new UsersTableUserControl());
        }

        private void Calendars_Click(object sender, RoutedEventArgs e)
        {
            tablesGrid.Children.Clear();
            tablesGrid.Children.Add(new CalendarsTableUserControl());
        }

        private void Events_Click(object sender, RoutedEventArgs e)
        {
            tablesGrid.Children.Clear();
            tablesGrid.Children.Add(new EventsTableUserControl());
        }

        private void EventTypes_Click(object sender, RoutedEventArgs e)
        {
            tablesGrid.Children.Clear();
            tablesGrid.Children.Add(new EventTypeTableUserControl());
        }
    }
}
