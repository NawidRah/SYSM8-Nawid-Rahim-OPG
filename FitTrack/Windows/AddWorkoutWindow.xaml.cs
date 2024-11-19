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
        public Workout NewWorkout { get; private set; }

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

        private void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            // Kontrollera om en träningstyp har valts
            if (WorkoutTypeComboBox.SelectedItem is ComboBoxItem selectedType)
            {
                string selectedTypeName = selectedType.Content.ToString();

                switch (selectedTypeName)
                {
                    case "Cardio":
                        MessageBox.Show("Cardio workouts focus on distance-based activities like running or cycling. Input distance covered in kilometers or miles.",
                                        "Workout Type Details", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    case "Strength":
                        MessageBox.Show("Strength workouts focus on repetitions-based exercises like push-ups or weightlifting. Input the number of reps completed.",
                                        "Workout Type Details", MessageBoxButton.OK, MessageBoxImage.Information);
                        break;

                    default:
                        MessageBox.Show("Please select a workout type to view more details.",
                                        "Workout Type Details", MessageBoxButton.OK, MessageBoxImage.Warning);
                        break;
                }
            }
            else
            {
                MessageBox.Show("Please select a workout type to view more details.",
                                "Workout Type Details", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}