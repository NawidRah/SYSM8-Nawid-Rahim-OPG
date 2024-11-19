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
    /// Interaction logic for WorkoutsWindow.xaml
    /// </summary>
    public partial class WorkoutsWindow : Window
    {
        public User User { get; set; }
        public List<Workout> WorkoutList { get; set; }

        public WorkoutsWindow(User user)
        {
            InitializeComponent();
            User = user;

            // Om användaren har användarnamnet ''Admin'' så har den speciella behörigheter. Såsom att kunna se alla träningar och modifiera dem.
            if (User.Username == "admin")
            {
                WorkoutList = UserManager.GetAllWorkouts(); // Visar alla träningspass för Admin.
            }
            else
            {
                WorkoutList = User.Workouts ?? new List<Workout>(); // Visar bara träningspassen för den inloggade användaren.
            }

            RefreshAndFilterWorkoutList();
        }


        public void LoadAllWorkouts()
        {
            WorkoutList = UserManager.GetAllWorkouts(); // Hämtar alla träningspass som finns.
            RefreshAndFilterWorkoutList();
        }
        // Metod för att ladda träningspass från användaren
        public void LoadWorkoutsForUser(User user)
        {
            if (user.Workouts != null && user.Workouts.Any())
            {
                WorkoutList = user.Workouts; // Kopiera användarens träningspass till WorkoutList
            }
            else
            {
                WorkoutList = new List<Workout>(); // Om inga träningspass finns, skapa en tom lista
            }

            WorkoutsList.ItemsSource = WorkoutList; // Uppdateras tränings listan.
        }



        // Uppdaterar träningslistan med redigeringar samt filtrerar träningarna.
        private void RefreshAndFilterWorkoutList()
        {
            FilterWorkouts();
        }



        //Lägger till ett träningspass i listan.
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            var addWorkoutWindow = new AddWorkoutWindow();

            if (addWorkoutWindow.ShowDialog() == true && addWorkoutWindow.NewWorkout != null)
            {
                Workout newWorkout = addWorkoutWindow.NewWorkout;
                WorkoutList.Add(newWorkout);
                RefreshAndFilterWorkoutList();
            }
            else
            {
                MessageBox.Show("No workout was added.");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show(); // Visa MainWindow
            this.Close();

        }

        // Skapar ett fönster som visar detaljer om träningspasset.
        private void DetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedWorkout = (Workout)WorkoutsList.SelectedItem;

            if (selectedWorkout != null)
            {
                var detailsWindow = new WorkoutDetailsWindow(selectedWorkout);
                detailsWindow.ShowDialog();

            }
            
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            {
                // Kontrollerar om det finns ett val som gjorts
                var selectedWorkout = (Workout)WorkoutsList.SelectedItem;
                if (selectedWorkout != null)
                {
                    // Tar bort eventuella valet.
                    WorkoutList.Remove(selectedWorkout);

                    // Listan uppdateras så att det blir vad användaren valt.
                    RefreshAndFilterWorkoutList();

                    MessageBox.Show("Workout removed successfully.");
                }
                else
                {
                    // Om en träning inte valts så kommet ett felmeddelande
                    MessageBox.Show("Please select a workout to remove.");
                }
            }

        }

        //Filtrera träningar
        private void FilterWorkouts()
        {
            string searchText = SearchTextBox.Text.ToLower();
            string selectedType = (WorkoutTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "All";

            var filteredWorkouts = WorkoutList.Where(workout =>
                (string.IsNullOrEmpty(searchText) ||
                 (workout.Name != null && workout.Name.ToLower().Contains(searchText)) ||
                 (workout.Notes != null && workout.Notes.ToLower().Contains(searchText))) &&
                (selectedType == "All" || workout.Type == selectedType)).ToList();

            WorkoutsList.ItemsSource = filteredWorkouts;
        }

        private void WorkoutTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterWorkouts();
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            FilterWorkouts();
        }


        //Ändrade info texten så att den blev kortare, samt mer tydlig och professionell.
        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            string infoMessage = "FitTrack is an app developed by Nawid Rahim. It helps users create and manage their workout plans." +
                                 " Users can add, edit, and search workouts with ease. For more information, email us at support@fittrack.com.";

            MessageBox.Show(infoMessage, "About FitTrack", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {

            var LogOutWindow = new LogOutWindow();
            LogOutWindow.ShowDialog();

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selectedWorkout = (Workout)WorkoutsList.SelectedItem;

            if (selectedWorkout != null)
            {
                var editWorkoutWindow = new AddWorkoutWindow();
                editWorkoutWindow.SetWorkoutDetails(selectedWorkout);

                if (editWorkoutWindow.ShowDialog() == true && editWorkoutWindow.NewWorkout != null)
                {
                    // Uppdatera träningspasset
                    var updatedWorkout = editWorkoutWindow.NewWorkout;
                    var index = WorkoutList.IndexOf(selectedWorkout);
                    WorkoutList[index] = updatedWorkout;

                    RefreshAndFilterWorkoutList();
                }
            }
            else
            {
                MessageBox.Show("Please select a workout to edit.");
            }
        }
    }
}