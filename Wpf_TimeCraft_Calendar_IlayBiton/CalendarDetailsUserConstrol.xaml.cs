using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for CalendarDetailsUserConstrol.xaml
    /// </summary>
    public partial class CalendarDetailsUserConstrol : UserControl
    {
        private CalendarServiceClient serviceClient; 
        public CalendarDetailsUserConstrol(Calendar calendar)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.DataContext = calendar;
            LoadUsers(serviceClient.GetCalendarUsers(calendar));
            LoadEvents(serviceClient.GetCalendarEvents(calendar));
        }

        private void LoadUsers(UserList users)
        {
            foreach (User user in users)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = 12;
                textBlock.Text = user.Username;
                usersWP.Children.Add(textBlock);
            }
        }

        private void LoadEvents(EventList events)
        {
            foreach (Event _event in events)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.FontSize = 12;
                textBlock.Text = _event.EventName;
                usersWP.Children.Add(textBlock);
            }
        }
    }
}
