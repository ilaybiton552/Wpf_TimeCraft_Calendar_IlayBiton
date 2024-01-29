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
using MaterialDesignThemes;
using MaterialDesignThemes.Wpf;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for CalendarsUserControl.xaml
    /// </summary>
    public partial class CalendarsUserControl : UserControl
    {
        private User user;
        public CalendarsUserControl(User user)
        {
            InitializeComponent();
            this.user = user;
            calendars.ItemsSource = user.Calendars;
        }
    }
}
