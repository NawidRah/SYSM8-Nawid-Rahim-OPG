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
    /// Interaction logic for _2FAWindow.xaml
    /// </summary>
    public partial class _2FAWindow : Window
    {
         // LLagrar verifieringskoden i en variabel!

        private readonly string verificationCode;

        // Skapar en bool sats av ''Verification'' som vi kommer kunna sätta ''true'' eller ''false'' på.
        public bool Verification { get; private set; }

        public _2FAWindow(string code)
        {
            InitializeComponent();
            verificationCode = code;
        }

        // När man klickar på Verify så ser den om koden matchar!
        private void Verify_Click(object sender, RoutedEventArgs e)
        {
            // Ser till så att inmatningen är detsamma!
            if (CodeInput.Text == verificationCode)
            {
                // Vid rätt inmatning, så blir Verification = True;
                Verification = true;

                // Stänger fönstret
                this.Close(); 
            }
            else
            {
                // Visar fel meddelande vid fel knappsats.
                MessageBox.Show("The code is incorrect, Try again!");

               

            }
        }
    }
}