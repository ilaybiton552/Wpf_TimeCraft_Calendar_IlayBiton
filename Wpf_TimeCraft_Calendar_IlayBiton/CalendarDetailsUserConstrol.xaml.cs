using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
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
            inBorder.Background = new SolidColorBrush(calendar.BaseColor);
            try
            {
                users.Text += string.Join(", ", serviceClient.GetCalendarUsers(calendar).Select(user => user.Username));
                events.Text += string.Join(", ", serviceClient.GetCalendarEvents(calendar).Select(eve => eve.EventName));
            } catch { }
        }
    }
}
