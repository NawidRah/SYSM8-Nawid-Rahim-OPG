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
    /// Interaction logic for ResetPassWindow.xaml
    /// </summary>
    public partial class ResetPassWindow : Window
    {
        public ResetPassWindow()
        {
            InitializeComponent();
        }

        // Hanterar inmatningen genom ''Submit'' Knappen
        private void SubmitBtn_Click(object sender, RoutedEventArgs e)

        {
            string username = UsernameInput.Text;
            string answer = SecurityAnswerInput.Text;

            // Kontrollerar om användarnamnet är giltigt och säkerhetsfrågan är korrekt inmatad.
            if (IsUsernameValid(username) && IsSecurityAnswerCorrect(answer))
            {
                MessageBox.Show("Your password has been reset. Please check your email for the new password.");

                // Om allting är korrekt så stängs fönstret.
                this.Close(); 
            }
            else
            {
                MessageTextBlock.Text = "Invalid username or security answer.";
            }
        }
        // Kontrollerar om användarnamnet är tillgängligt.
        private bool IsUsernameValid(string username)
        {
            return username == "user";
        }

        // Kontrollerar om säkerhetskloden är korrekt.
        private bool IsSecurityAnswerCorrect(string answer)
        {
            // Kontrollera säkerhetsfrågan och om den är korrekt.
            return answer.Equals("blue", StringComparison.OrdinalIgnoreCase); 
        }
    }
}