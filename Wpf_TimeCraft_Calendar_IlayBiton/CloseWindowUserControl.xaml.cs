using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for CloseWindowUserControl.xaml
    /// </summary>
    public partial class CloseWindowUserControl : UserControl
    {
        public bool MainWindow {  get; set; }
        public CloseWindowUserControl()
        {
            InitializeComponent();
            tbClose.HorizontalAlignment = HorizontalAlignment.Center;
        }
        private void Close_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = Brushes.Red;
            tbClose.Foreground = Brushes.White;
        }
        private void Close_MouseLeave(object sender, MouseEventArgs e)
        {
            Border border = sender as Border;
            border.Background = null;
            tbClose.Foreground = Brushes.Black;
        }
        private void Close_LeftClick(object sender, MouseButtonEventArgs e)
        {
            if (MainWindow) Application.Current.Shutdown();
            Window.GetWindow(this).Close();
        }
    }
}
