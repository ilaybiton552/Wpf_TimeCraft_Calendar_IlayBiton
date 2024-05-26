using System.Windows.Controls;
using System.Windows.Input;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;
using Calendar = Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference.Calendar;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for DisplayCalendarUserControl.xaml
    /// </summary>
    public partial class DisplayCalendarUserControl : UserControl
    {
        public DisplayCalendarUserControl(ref Calendar calendar, ref User user)
        {
            InitializeComponent();
            grid.Children.Add(new CalendarUserControl(ref calendar, ref user));
            popup.Child = new CalendarDetailsUserConstrol(calendar);
        }
        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            popup.IsOpen = true;
        }
    }
}
