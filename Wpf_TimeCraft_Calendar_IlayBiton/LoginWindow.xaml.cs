using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Windows.Media.TextFormatting;
using System.Windows.Shapes;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private User user;
        private CalendarServiceClient serviceClient;
        public LoginWindow(ref User user)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceClient();
            this.user = user;
            this.DataContext = this.user;
            CloseWindowUserControl closeWindowUserControl = new CloseWindowUserControl(ref user);
            closeWindowUserControl.MainWindow = false;
            grid.Children.Add(closeWindowUserControl);
        }

        private void ShowPassword_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            image.Source = new BitmapImage(new Uri(@"Media\show-password.png", UriKind.Relative));
            pbPass.Visibility = Visibility.Collapsed;
            tbPass.Text = pbPass.Password.ToString();
            tbPass.Visibility = Visibility.Visible;
        }

        private void HidePassword_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Image image = sender as Image;
            image.Source = new BitmapImage(new Uri(@"Media\hide-password.png", UriKind.Relative));
            pbPass.Focus();
            pbPass.Visibility = Visibility.Visible;
            tbPass.Visibility = Visibility.Collapsed;
        }

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ValidPassword validPassword = new ValidPassword();
            ValidationResult result = validPassword.Validate(((PasswordBox)sender).Password, CultureInfo.CurrentCulture);
            if (!result.IsValid) // result not valid
            {
                errPass.Text = result.ErrorContent.ToString();
                pbPass.ToolTip = result.ErrorContent;
                pbPass.BorderThickness = new Thickness(1);
                tbPass.BorderThickness = new Thickness(1);
            }
            else // result is valid
            {
                errPass.Text = string.Empty;
                pbPass.ToolTip = string.Empty;
                pbPass.BorderThickness = new Thickness(0);
                tbPass.BorderThickness = new Thickness(0);
                user.Password = ((PasswordBox)sender).Password;
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            user.Username = "Username"; user.Password = "Password1";
            ValidPassword validPassword = new ValidPassword();
            ValidationResult validationResult = validPassword.Validate(pbPass.Password, CultureInfo.CurrentCulture);
            //if (!validationResult.IsValid || Validation.GetHasError(tbxUsername))
            //{
            //    MessageBox.Show("Fix your errors!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}
            User temp = serviceClient.Login(user);
            if (temp == null) 
            {
                MessageBox.Show("Error login in", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                user = new User() { Username = tbxUsername.Text, Password = pbPass.Password.ToString() };
                return;
            }
            user.Email = temp.Email;
            user.Birthday = temp.Birthday;
            user.PhoneNumber = temp.PhoneNumber;
            user.FirstName = temp.FirstName;
            user.LastName = temp.LastName;
            user.IsAdmin = temp.IsAdmin;
            user.ID = temp.ID;
            user.Calendars = serviceClient.GetUserCalendars(user);
            user.Events = serviceClient.GetUserEvents(user);
            MessageBox.Show("Logged in!");
            Close();
        }

        private void Signup_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.FontWeight = FontWeights.Bold;
        }

        private void Singup_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.FontWeight = FontWeights.Normal;
        }

        private void Signup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            user = new User();
            SignUpWindow signUpWindow = new SignUpWindow(ref user);
            signUpWindow.Left = Left + (Width - signUpWindow.Width) / 2;
            signUpWindow.Top = Top;
            Close();
            signUpWindow.ShowDialog();
        }
    }
}
