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
    /// Interaction logic for NavigationBarUserControl.xaml
    /// </summary>
    public partial class NavigationBarUserControl : UserControl
    {
        private Window currWindow;
        private User user;
        public NavigationBarUserControl(ref User user)
        {
            InitializeComponent();
            currWindow = Application.Current.MainWindow;
            this.user = user;
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
            LoginWindow nextWindow = new LoginWindow(ref user);
            nextWindow.Left = currWindow.Left + (currWindow.Width - nextWindow.Width) / 2;
            nextWindow.Top = currWindow.Top + (currWindow.Height - nextWindow.Height) / 2;
            nextWindow.Show();
        }

        private void SignUp_Down(object sender, MouseButtonEventArgs e)
        {
            SignUpWindow nextWindow = new SignUpWindow(ref user);
            nextWindow.Left = currWindow.Left + (currWindow.Width - nextWindow.Width) / 2;
            nextWindow.Top = currWindow.Top + (currWindow.Height - nextWindow.Height) / 2;
            nextWindow.Show();
        }

        private void Home_Down(object sender, MouseButtonEventArgs e)
        {
            //MainWindow nextWindow = new MainWindow();
            //currWindow.Close();
            //nextWindow.ShowDialog();
        }

        public void UserLogged()
        {
            AddLogout();
            sp.Children.Remove(tb)
        }

        private void AddLogout()
        {
            TextBlock logout = new TextBlock();
            logout.Name = "tbLogout";
            logout.Text = "Log Out";
            logout.MouseEnter += TB_MouseEnter;
            logout.MouseLeave += TB_MouseLeave;
            logout.MouseLeftButtonDown += Logout_Down;
        }

        private void AddLogin()
        {
            TextBlock login = new TextBlock();
            login.Text = "tbLogin";
            login.Text = "Login";
            login.MouseEnter += TB_MouseEnter;
            login.MouseLeave += TB_MouseLeave;
            login.MouseLeftButtonDown += Login_Down;
        }

        private void AddSingup()
        {
            TextBlock signup = new TextBlock();
            signup.Name = "tbSignup";
            signup.Text = "Log Out";
            signup.MouseEnter += TB_MouseEnter;
            signup.MouseLeave += TB_MouseLeave;
            signup.MouseLeftButtonDown += SignUp_Down;
        }

        private void Logout_Down(object sender, MouseButtonEventArgs e)
        {
            AddLogin();
            AddSingup();
        }
    }
}
