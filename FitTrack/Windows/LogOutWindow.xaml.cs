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
    /// Interaction logic for LogOutWindow.xaml
    /// </summary>
    public partial class LogOutWindow : Window
    {
        public LogOutWindow()
        {
            InitializeComponent();
        }

        private void NoBTN_Click(object sender, RoutedEventArgs e)
        {

            

        }

        private void YesBTN_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thanks for using FitTrack! You are logged out!");

            
        }
    }
}
