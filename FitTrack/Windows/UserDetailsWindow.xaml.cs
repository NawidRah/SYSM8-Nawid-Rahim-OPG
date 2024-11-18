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
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        private User CurrentUser;

        // Konstruktor som tar emot användare och skriver ut informationen
        public UserDetailsWindow(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            LoadUserDetails();
        }

        // Laddar in användardetaljerna i programmet.
        private void LoadUserDetails()
        {
            CurrentUsername.Text = CurrentUser.Username;
            CountryComboBox.Text = CurrentUser.Country;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            // Hämtar dem nya värdena som inmatats i programmet.
            string newUsername = NewUsrnameInput.Text;
            string newPassword = NewPwdInput.Password;
            string ConfirmPassword = ConfirmPwdInput.Password;
            string selectedCountry = CountryComboBox.Text;


            // Kontrollerar att det nya användaren har ett användarnamn över 3 tecken
            if (newUsername.Length < 3)
            {
                ErrorMessage.Text = "Username must contain atleast 3 letters!"; //Fel meddelande som kommer vid misslyckat försök!
                return;
            }

            // Kontrollerar om användarnamnet är taget.
            if (IsUsernameTaken(newUsername))
            {
                ErrorMessage.Text = "Username is already taken!"; //Felmeddelande vid taget användarnamn!
                return;
            }

            // Kontrollerar så att båda lösenorden matchar, annars fel meddelande!
            if (newPassword != ConfirmPassword)
            {
                ErrorMessage.Text = "Passwords do not match! Try again."; //Felmeddelandet!
                return;
            }

            // Kontrollerar att lösenordet är minst 5 tecken långt
            if (newPassword.Length < 5)
            {
                ErrorMessage.Text = "Password must contain at least 5 characters!";
                return;
            }

            // Uppdaterar användaren om allting är rätt inmatat!
            CurrentUser.Username = newUsername;
            CurrentUser.Password = newPassword;
            CurrentUser.Country = selectedCountry;

            MessageBox.Show("User has succesfully updated!"); //Godkännande meddelande!
            this.Close();
        }

        //Kontrollerar om användarnamnet redan är taget!
        private bool IsUsernameTaken(string username)
        {
            return username == "admin";
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

            // Stänger fönstret om cancel klickas!
            this.Close();

        }
    }
}