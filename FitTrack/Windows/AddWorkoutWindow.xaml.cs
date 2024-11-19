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
    /// Interaction logic for AddWorkoutWindow.xaml
    /// </summary>
    public partial class AddWorkoutWindow : Window
    {
        // Skapar en ny träning som kan användas utanför denna klassen (Public)
        public Workout NewWorkout { get; private set; }
        public AddWorkoutWindow()
        {
            InitializeComponent();
        }


        // Metod som sätter in detaljer för träningen
        public void SetWorkoutDetails(Workout workout)
        {
            if (workout is CardioWorkout cardioWorkout)
            {
                // Cardio räknas in distans, denna kod ändrar så att det är distans och inte repititioner
                WorkoutTypeComboBox.SelectedItem = "Cardio";
                WorkoutDetailsInput.Text = cardioWorkout.Distance.ToString();
            }
            else if (workout is StrengthWorkout strengthWorkout)
            {
                // Strength är valt och då räknas repitioner, inte distans!
                WorkoutTypeComboBox.SelectedItem = "Strength";
                WorkoutDetailsInput.Text = strengthWorkout.Repetitions.ToString();
            }
        }

        // Användarens träning sparas när den klickar på "save"
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            // Kontrollerar om inmatningen är tom!
            if (WorkoutTypeComboBox.SelectedItem != null && !string.IsNullOrEmpty(WorkoutDetailsInput.Text))
            {
                // Detta hämtat den valda träningen!
                ComboBoxItem selectedType = (ComboBoxItem)WorkoutTypeComboBox.SelectedItem;


                int value;
                bool parseSuccess = int.TryParse(WorkoutDetailsInput.Text, out value);

                // Skapar träningen om cardio är valt och allting är giltigt!
                if (selectedType.Content.ToString() == "Cardio" && parseSuccess)
                {
                    NewWorkout = new CardioWorkout
                    {
                        Distance = value,
                        Name = WorkoutDescriptionInput.Text,
                        Type = "Cardio" // Typen är satt till Cardio!
                    };
                }
                // Skapar ett träningspass som har styrke träningar med repititioner!
                else if (selectedType.Content.ToString() == "Strength" && parseSuccess)
                {
                    NewWorkout = new StrengthWorkout
                    {
                        Repetitions = value, // Sparar repititioner
                        Name = WorkoutDescriptionInput.Text,
                        Type = "Strength" // Sparar som typen styrka!
                    };
                }

                // Om allting är korrekt och fungerar som det ska, så ska skärmen stängas av!
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                // Om det finns felaktigheter i texten, skrivs detta ut!
                MessageBox.Show("Please enter valid workout details.");
            }
        }

        // Fönstret avbryts när cancel klickas
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();

        }

        private void WorkoutDetailsInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = false;
        }

            
        
    }

}
