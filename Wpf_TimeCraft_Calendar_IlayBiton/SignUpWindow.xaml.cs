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
        private User tempUser;
        private CalendarServiceReference.CalendarServiceClient serviceClient;
        private bool update;
        public SignUpWindow(ref User user, bool update = false)
        {
            InitializeComponent();
            serviceClient = new CalendarServiceReference.CalendarServiceClient();
            this.user = user;
            tempUser = new User()
            { 
                ID = user.ID,
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                Birthday = user.Birthday,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsAdmin = user.IsAdmin
            };
            this.update = update;
            this.DataContext = tempUser;
            if (!update) tempUser.Birthday = DateTime.Now;
            else title.Text = "Update";
            CloseWindowUserControl closeWindowUserControl = new CloseWindowUserControl();
            closeWindowUserControl.MainWindow = false;
            closeWindowUserControl.SetValue(Grid.ColumnProperty, 1);
            grid.Children.Add(closeWindowUserControl);
            if (update) ChangeUpdate();
        }

        private void ChangeUpdate()
        {
            sendBtn.Content = "Edit";
            loginPass.Text = "Delete User";
            pbPass.Password = tempUser.Password;
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
                if (!update)
                    tempUser.Password = ((PasswordBox)sender).Password;
                else
                    tempUser.Password = ((PasswordBox)sender).Password;
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
            if (serviceClient.IsEmailTaken(tempUser))
            {
                MessageBox.Show("Email is already used!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (serviceClient.IsUsenameTaken(tempUser))
            {
                MessageBox.Show("Username is already used!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (!update)
                {
                    user.ID = serviceClient.InsertUser(tempUser);
                    if (user.ID != -1)
                    {
                        MessageBox.Show("Success creating user!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Error Creating User", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    if (serviceClient.UpdateUser(tempUser) == 1)
                    {
                        MessageBox.Show("Success editing user!");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Error editing User", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                user.Email = tempUser.Email;
                user.Birthday = tempUser.Birthday;
                user.FirstName = tempUser.FirstName;
                user.LastName = tempUser.LastName;
                user.Username = tempUser.Username;
                user.Password = tempUser.Password;
                user.PhoneNumber = tempUser.PhoneNumber;
            }
            catch(Exception) { }
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
            if (!update)
            {
                LoginWindow loginWindow = new LoginWindow(ref user);
                loginWindow.Left = Left + (Width - loginWindow.Width) / 2;
                loginWindow.Top = Top;
                Close();
                loginWindow.ShowDialog();
            }
            else
            {
                int rows = serviceClient.DeleteUser(tempUser);
                if (user.Calendars != null && rows == 1 + user.Calendars.Count || rows == 1) 
                {
                    MessageBox.Show("Deleted User");
                    user.Username = string.Empty;
                    Close();
                    return;
                }
                MessageBox.Show("Error deleting user");
            }
        }

    }
}
