using System.Windows;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            user = new User();
            userControlGrid.Width = Width * 3 / 4;
            mainGrid.Children.Add(new NavigationBarUserControl(ref user, ref userControlGrid));
        }
    }
}
