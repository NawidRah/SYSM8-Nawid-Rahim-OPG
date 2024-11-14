using FitTrack.Models;
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
using System.Windows.Shapes;

namespace FitTrack.Windows
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }



        // Metod som körs när registreringsknappen klickas
        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            // Kontrollera om lösenorden matchar

            if (PwdInput.Password != ConfirmPwd.Password)
            {
                ErrorMessage.Text = "Passwords do not match";
                return;
            }

            // Kontrollera lösenordets längd och att det innehåller en siffra och ett specialtecken

            if (PwdInput.Password.Length < 8 || !PwdInput.Password.Any(char.IsDigit) || !PwdInput.Password.Any(char.IsPunctuation))
            {
                ErrorMessage.Text = "Password must be at least 8 characters long, with at least one number and one special character.";
                return;
            }

            // Kontrollera om användarnamnet redan är upptaget

            if (UserManager.IsUsernameTaken(UsernameInput.Text))
            {
                ErrorMessage.Text = "Username is already taken.";
                return;
            }

            // Skapa en ny användare och lägg till den i UserManager
            var newUser = new User
            {
                Username = UsernameInput.Text,
                Password = PwdInput.Password,
                Country = CountryComboBox.Text
            };

            UserManager.AddUser(newUser); // Spara användaren
            MessageBox.Show("User registered successfully!");

            this.Close();
        }


        // Stäng fönstret om Cancel-knappen klickas
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        // Kontrollera ifall användarnamnet redan är upptaget
        private bool IsUsernameTaken(string username)
        {
            if (username == "admin")
            {
                return true;
            }

            return false;
        }

        private void PwdInputTEXT_MouseEnter(object sender, MouseEventArgs e)
        {

            
            if (PwdInput.Password == null || PwdInput.Password == "")
            {
                PwdInputTEXT.Visibility = Visibility.Visible;
            }
            else
            {
                PwdInputTEXT.Visibility = Visibility.Collapsed;
            }
        }

        private void PwdInputTEXT_MouseLeave(object sender, MouseEventArgs e)
        {
            if (PwdInput.Password == null || PwdInput.Password == "")
            {
                PwdInputTEXT.Visibility = Visibility.Visible;
            }
            else
            {
                PwdInputTEXT.Visibility = Visibility.Collapsed;
            }

        }
    }
}

    
