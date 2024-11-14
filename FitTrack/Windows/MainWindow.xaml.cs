using FitTrack.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitTrack.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UserManager.InitializeDefaultUsers();
        }

        private void LogInBTN_Click(object sender, RoutedEventArgs e)
        {

            // Hämtar användarnamn och lösenord som användaren matat in.
            string username = UsernameInput.Text;
            string password = PasswordInput.Password;

            // Ser till så att inmatningen matchar med vad som finns i databasen.
            if (UserManager.AuthenticateUser(username, password))
            {
                // Genererar en ''random'' siffra för att kunna använda till 2FA.
                string verificationCode = new Random().Next(100000, 999999).ToString();
                MessageBox.Show($"Verification code: {verificationCode}", "2FA Code");

                // Visar upp 2FA fönstret
                var twoFactorWindow = new _2FAWindow(verificationCode);
                twoFactorWindow.ShowDialog();

                //  Kontrollerar så att koden är correct!

                if (twoFactorWindow.Verification)
                {
                    // Hämtar användaren som klarat sig genom 2FA
                    User authenticatedUser = UserManager.GetUserByUsername(username);

                    // Om användaren är en ''Admin'' så har den fler behörigheter, såsom att ta emot alla träningspass.
                    if (username == "admin")
                    {
                        var workoutsWindow = new WorkoutsWindow(authenticatedUser);

                        // Laddar in alla träningspass inför Admin
                        workoutsWindow.LoadAllWorkouts(); 
                        workoutsWindow.Show();
                    }
                    else
                    {
                        // Visar ett WorkoutWindow med alla olika träningspass
                        var workoutsWindow = new WorkoutsWindow(authenticatedUser);

                        // Laddar användarens egna träningspass.
                        workoutsWindow.LoadWorkoutsForUser(authenticatedUser); 
                        workoutsWindow.Show();
                    }

                    this.Close();
                }
                else
                {

                    //Fel meddelande vid felaktig 2FA
                    MessageBox.Show("Two-factor authentication failed.", "Login failed");
                }
            }
            else
            {

                //Felmeddelande vid felaktig namn eller lösenord.
                MessageBox.Show("Invalid username or password.", "Login failed");
            }
        }


        private void RegisterBTN_Click(object sender, RoutedEventArgs e)
        {
            {

                // Öppnar register window när användaren klickar Register knappen.
                var registerWindow = new RegisterWindow();
                registerWindow.Show();

                this.Close();
            }

        }


        //Glömt lösenord knappen leder till ''Forgot Password'' för att återställa lösenord.
        private void ForgotPassBTN_Click(object sender, RoutedEventArgs e)
        {
            var resetPasswordWindow = new ResetPassWindow();
            resetPasswordWindow.ShowDialog();

        }

        private void UsernameInput_MouseEnter(object sender, MouseEventArgs e)
        {

            UsernameInputTEXT.Visibility = Visibility.Collapsed;


        }

        private void UsernameInput_MouseLeave(object sender, MouseEventArgs e)
        {

            if (sender is TextBox box)
            {
                if (string.IsNullOrEmpty(box.Text))
                {
                    UsernameInputTEXT.Visibility = Visibility.Visible;
                }
            }

        }

        private void UsernameInputTEXT_MouseEnter(object sender, MouseEventArgs e)
        {
            UsernameInputTEXT.Visibility = Visibility.Collapsed;

        }

        private void UsernameInputTEXT_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is TextBox box)
            {
                if (string.IsNullOrEmpty(box.Text))
                {
                    UsernameInputTEXT.Visibility = Visibility.Visible;
                }
            }
        }

        private void UsernameInput_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (sender is TextBox UserName)
            {
                if (string.IsNullOrEmpty(UserName.Text))
                {
                    UsernameInputTEXT.Visibility = Visibility.Visible;
                }
                else
                {
                    UsernameInputTEXT.Visibility = Visibility.Collapsed;
                }
            }

        }


        //Password input och placeholder för passwordbox
        private void PasswordInput_MouseEnter(object sender, MouseEventArgs e)
        {
            PasswordInputTEXT.Visibility = Visibility.Collapsed;

        }

        private void PasswordInput_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox pass)
            {
                {
                    if (string.IsNullOrEmpty(pass.Password))
                    {
                        PasswordInputTEXT.Visibility = Visibility.Visible;
                    }
                }
            }

        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox Pass)
            {
                if (string.IsNullOrEmpty(Pass.Password))
                {
                    PasswordInputTEXT.Visibility = Visibility.Visible;
                }
                else
                {
                    PasswordInputTEXT.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void PasswordInputTEXT_MouseEnter(object sender, MouseEventArgs e)
        {
            PasswordInputTEXT.Visibility = Visibility.Collapsed;

        }

        private void PasswordInputTEXT_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is PasswordBox pass)
            {
                if (string.IsNullOrEmpty(pass.Password))
                {
                    PasswordInputTEXT.Visibility = Visibility.Visible;
                }

            }

        }

    }
}
