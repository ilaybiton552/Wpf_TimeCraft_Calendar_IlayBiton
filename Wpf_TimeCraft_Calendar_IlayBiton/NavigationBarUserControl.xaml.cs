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

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for NavigationBarUserControl.xaml
    /// </summary>
    public partial class NavigationBarUserControl : UserControl
    {
        Window currWindow;
        public NavigationBarUserControl()
        {
            InitializeComponent();
            currWindow = Application.Current.MainWindow;
        }

        private void TB_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Background = Brushes.LightBlue;
            textBlock.FontWeight = FontWeights.Regular;
        }

        private void TB_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.Background = Brushes.DarkCyan;
            textBlock.FontWeight = FontWeights.Bold;
        }

        private void Login_Down(object sender, MouseButtonEventArgs e)
        {
            LoginWindow nextWindow = new LoginWindow();
            nextWindow.Left = currWindow.Left + (currWindow.Width - nextWindow.Width) / 2;
            nextWindow.Top = currWindow.Top + (currWindow.Height - nextWindow.Height) / 2;
            nextWindow.Show();
        }

        private void SignUp_Down(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow nextWindow = new SignUpWindow();
            nextWindow.Left = currWindow.Left + (currWindow.Width - nextWindow.Width) / 2;
            nextWindow.Top = currWindow.Top + (currWindow.Height - nextWindow.Height) / 2;
            nextWindow.ShowDialog();
        }

        private void Home_Down(object sender, MouseButtonEventArgs e)
        {
            //MainWindow nextWindow = new MainWindow();
            //currWindow.Close();
            //nextWindow.ShowDialog();
        }
    }
}
