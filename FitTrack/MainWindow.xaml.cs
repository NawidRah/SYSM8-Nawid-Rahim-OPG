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

namespace FitTrack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //Username input och placeholder effekt för Username textboxen.

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
                if (string.IsNullOrEmpty (pass.Password))
                {  
                    PasswordInputTEXT.Visibility = Visibility.Visible;
                }
                
            }
            
        }


        //Login knapp som registrerar inlogg och verifierar om det är rätt. Sedan förs användaren till WorkoutsWindow där användaren kan använda appen.
        private void LoginButton_Click(object sender, RoutedEventArgs e) 
        {
            
            string Username = UsernameInput.Text;
            string Password = PasswordInput.Password;

            if (Username == "Admin" && Password == "1234")
            {
                MessageBox.Show("Login sucessful!");

                WorkoutsWindow workoutWindow = new WorkoutsWindow();
                workoutWindow.Show();

                Close();
                

                
            }
            else
            {
                MessageBox.Show("Incorrect password!");
            }



        }






        //Register knapp som tar användaren till RegisterWindow där en profil kan skapas.
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {

            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();

            Close();


        }

        public void Register() //SKRIV DENNA!
        {

        }
        

    }
}