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
        public Workout NewWorkout { get; private set; } //FIXA SÅ ATT ADDWORKOUTS LÄGGER TILL I WORKOUTSWINDOW!!

        public AddWorkoutWindow()
        {
            InitializeComponent();

        }




        // Sätter detaljer för ett träningspass när fönstret öppnas för redigering
        public void SetWorkoutDetails(Workout workout)
        {
            WorkoutDescriptionInput.Text = workout.Name;

            if (workout is CardioWorkout cardioWorkout)
            {
                WorkoutTypeComboBox.SelectedItem = "Cardio";
                WorkoutDetailsInput.Text = cardioWorkout.Distance.ToString();
            }
            else if (workout is StrengthWorkout strengthWorkout)
            {
                WorkoutTypeComboBox.SelectedItem = "Strength";
                WorkoutDetailsInput.Text = strengthWorkout.Repetitions.ToString();
            }
        }

        //Spara knapp för att lägga till träningen
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WorkoutTypeComboBox.SelectedItem != null && !string.IsNullOrEmpty(WorkoutDetailsInput.Text))
            {
                ComboBoxItem selectedType = (ComboBoxItem)WorkoutTypeComboBox.SelectedItem;

                if (int.TryParse(WorkoutDetailsInput.Text, out int value))
                {
                    if (selectedType.Content.ToString() == "Cardio")
                    {
                        NewWorkout = new CardioWorkout
                        {
                            Distance = value,
                            Name = WorkoutDescriptionInput.Text,
                            Type = "Cardio"
                        };
                    }
                    else if (selectedType.Content.ToString() == "Strength")
                    {
                        NewWorkout = new StrengthWorkout
                        {
                            Repetitions = value,
                            Name = WorkoutDescriptionInput.Text,
                            Type = "Strength"
                        };
                    }

                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please enter a valid number for details.");
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields.");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}