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
using System.Windows.Shapes;
using Wpf_TimeCraft_Calendar_IlayBiton.CalendarServiceReference;

namespace Wpf_TimeCraft_Calendar_IlayBiton
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow : Window
    {
        private User user;
        private CalendarServiceReference.CalendarServiceClient serviceClient;
        public SignUpWindow(ref User user)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceReference.CalendarServiceClient();
            this.user = user;
            this.DataContext = this.user;
            this.user.Birthday = DateTime.Now;
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

        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            ValidPassword validPassword = new ValidPassword();
            ValidationResult validationResult = validPassword.Validate(pbPass.Password, CultureInfo.CurrentCulture);
            if (!validationResult.IsValid || Validation.GetHasError(tbxEmail) | Validation.GetHasError(tbxPhone) ||
                Validation.GetHasError(tbxFirstname) || Validation.GetHasError(tbxLastname) || 
                Validation.GetHasError(tbxUsername))
            {
                MessageBox.Show("Fix your errors!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (serviceClient.IsEmailTaken(user))
            {
                MessageBox.Show("Email is already used!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (serviceClient.IsUsenameTaken(user))
            {
                MessageBox.Show("Username is already used!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (serviceClient.InsertUser(user) == 1)
                    MessageBox.Show("Success creating user!");
                Close();
            }
            catch(Exception)
            {
                return;
            }
        }

        private void Login_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.FontWeight = FontWeights.Bold;
        }

        private void Login_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            textBlock.FontWeight = FontWeights.Normal;
        }

        private void Login_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            user = new User();
            LoginWindow loginWindow = new LoginWindow(ref user);
            Close();
            loginWindow.ShowDialog();
        }

    }
}
