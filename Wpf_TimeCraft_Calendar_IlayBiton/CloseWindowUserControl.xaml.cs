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
    /// Interaction logic for CloseWindowUserControl.xaml
    /// </summary>
    public partial class CloseWindowUserControl : UserControl
    {
        public CloseWindowUserControl()
        {
            InitializeComponent();
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
            Window.GetWindow(this).Close();
        }

    }
}
