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
    /// Interaction logic for WorkoutDetailsWindow.xaml
    /// </summary>
    public partial class WorkoutDetailsWindow : Window
    {
        private Workout CurrentWorkout;
        public Workout CopiedWorkout { get; private set; } // Kopierat träningspass

        // Konstruktor som tar emot ett träningspass och laddar dess detaljer
        public WorkoutDetailsWindow(Workout workout)
        {
            InitializeComponent();
            CurrentWorkout = workout;
            LoadWorkoutDetails();
        }


        // Metod för att fylla i träningsdetaljer i fönstret
        private void LoadWorkoutDetails()
        {
            // Sätter datumet till dagens datum om datumet inte är satt
            WorkoutDateInput.SelectedDate = CurrentWorkout.Date != DateTime.MinValue ? CurrentWorkout.Date : DateTime.Today;

            WorkoutTypeInput.Text = CurrentWorkout.Type;
            WorkoutDetailsInput.Text = CurrentWorkout.Notes;
            CaloriesBurnedInput.Text = CurrentWorkout.CaloriesBurned.ToString();
        }


        // Metod för att låsa upp redigering av träningsdetaljer
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            WorkoutDateInput.IsEnabled = true;
            WorkoutTypeInput.IsReadOnly = false;
            WorkoutDetailsInput.IsReadOnly = false;
            CaloriesBurnedInput.IsReadOnly = false;

        }

        // Sparar ändringar av träningsdetaljer
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WorkoutDateInput.SelectedDate.HasValue)
            {
                CurrentWorkout.Date = WorkoutDateInput.SelectedDate.Value;
            }
            else
            {
                MessageBox.Show("Please select a valid date.");
                return;
            }

            CurrentWorkout.Type = WorkoutTypeInput.Text;
            CurrentWorkout.Notes = WorkoutDetailsInput.Text;

            // Återställ fälten till skrivskyddade efter sparande
            WorkoutDateInput.IsEnabled = false;
            WorkoutTypeInput.IsReadOnly = true;
            WorkoutDetailsInput.IsReadOnly = true;
            CaloriesBurnedInput.IsReadOnly = true;

            MessageBox.Show("Workout details updated successfully!");
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();


        }

        private void CopyBtn_Click(object sender, RoutedEventArgs e)
        {
            var addWorkoutWindow = new AddWorkoutWindow();
            addWorkoutWindow.SetWorkoutDetails(CurrentWorkout); // Fyll i befintliga data

            if (addWorkoutWindow.ShowDialog() == true)
            {
                // Sätt kopian till den nya träningen som skapades i AddWorkoutWindow
                CopiedWorkout = addWorkoutWindow.NewWorkout;
                MessageBox.Show("Workout copied and saved successfully!");
                this.DialogResult = true;
                this.Close();
            }
        }
    }
}