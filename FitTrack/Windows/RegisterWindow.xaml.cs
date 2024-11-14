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

        


        //En rad kod som gör animeringen av Username input så att den försvinner när musen är över, beroende på om det finns text eller inte.
        private void UsernameInput_MouseEnter(object sender, MouseEventArgs e)
        {

            if (UsernameInput.Text != null || UsernameInput.Text != "") //Om texten INTE är null eller tom så försvinner texten
            {
                UsernameInput.Text = string.Empty; //Försvinner

                UsernameInput.Foreground = Brushes.Black; //Texten som användaren skriver kommer in i svart. Blir mer tydligt för användaren.
            }
        }


        //Denna kod förtydligar det som sker när musen lämnar, det vill säga att om koden är null och tom så kommer placeholder texten tillbaka!
        private void UsernameInput_MouseLeave(object sender, MouseEventArgs e)
        {

            if (UsernameInput.Text == null || UsernameInput.Text == "") //Om den är null eller tom!
            {
                UsernameInput.Text = "Username"; //Placeholder texten tillbaka

                UsernameInput.Foreground = Brushes.Gray; //Texten blir grå för att visa att det är en placeholder.
            }
        }
        //-----------------------------------------------------

        private void PwdInputTEXT_MouseEnter(object sender, MouseEventArgs e)
        {


            if (PwdInput.Password != null || PwdInput.Password != "")
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


        }
        //-----------------------------
        //Första lösenords animeringen, samma som ovan!
        private void PwdInput_MouseEnter(object sender, MouseEventArgs e)
        {

            if (PwdInput.Password != null || PwdInput.Password != "")
            {
                PwdInputTEXT.Visibility = Visibility.Collapsed;

            }
            

        }

        private void PwdInput_MouseLeave(object sender, MouseEventArgs e)
        {

            if (PwdInput.Password == null || PwdInput.Password == "")
            {
                PwdInputTEXT.Visibility = Visibility.Visible;
            }

        }
        //--------------------------------------------------------
        //Samma sak på Confirm lösenord!
        private void ConfirmPwdTEXT_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ConfirmPwd.Password != null || ConfirmPwd.Password != "")
            {
                ConfirmPwdTEXT.Visibility = Visibility.Collapsed;
            }

        }

        private void ConfirmPwdTEXT_MouseLeave(object sender, MouseEventArgs e)
        {

            if (ConfirmPwd.Password == null || ConfirmPwd.Password == "")
            {
                ConfirmPwdTEXT.Visibility = Visibility.Visible;
            }
        }


        //Andra delen av animeringen.
        private void ConfirmPwd_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ConfirmPwd.Password != null || ConfirmPwd.Password != "")
            {
                ConfirmPwdTEXT.Visibility= Visibility.Collapsed;
            }
        }

        private void ConfirmPwd_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ConfirmPwd.Password == null || ConfirmPwd.Password == "")
            {
                ConfirmPwdTEXT.Visibility= Visibility.Visible;
            }
        }
        //-----------------------------------------------------
        //Placeholder för komboboxen fungerar annorlunda, här behöver vi skriva SelectionChanged istället då den triggas när ett alternativ valts.
        private void CountryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CountryComboBox.SelectedItem == null)
            {
                // Om ingenting är valt, så kommer placeholder texten visas på komboboxen
                CountryComboBoxTEXT.Visibility = Visibility.Visible;

                
            }
            else
            {
                // Om ett alternativ är valt så försvinner placeholders, så att den inte går över texten!
                CountryComboBoxTEXT.Visibility = Visibility.Collapsed;
            }

        }


        //----------------------------------------------------------------------------


    }
}

    
