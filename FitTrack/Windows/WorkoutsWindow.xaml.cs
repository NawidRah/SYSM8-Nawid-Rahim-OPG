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

            // Kontrollerar så att användaren har Admin behörighet
            if (User.Username == "admin")
            {
                WorkoutList = UserManager.GetAllWorkouts();

                // En debug för att se antalet träningar och bekräfta att det är Admin
                Console.WriteLine($"Admin logged in. Total workouts: {WorkoutList.Count}");
            }
            else
            {
                WorkoutList = User.Workouts ?? new List<Workout>();

                // En till debug för att godkänna att det är User som är inloggad, samt antal träningar för vanlig User.
                Console.WriteLine($"User logged in. Workouts: {WorkoutList.Count}");
            }

            LoadAllWorkouts();
        }


        public void LoadAllWorkouts()
        {
            WorkoutList = UserManager.GetAllWorkouts();

            // Debug-logg
            MessageBox.Show($"Workouts loaded: {WorkoutList.Count} workouts available.");

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
            WorkoutsList.ItemsSource = null; // Tömmer först listan så att det korrekt filtreras
            WorkoutsList.ItemsSource = WorkoutList; // Återställer listan så att det uppdateras korrekt.
            FilterWorkouts();
        }



        //Lägger till ett träningspass i listan.
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            var addWorkoutWindow = new AddWorkoutWindow();
            
            //Om AddWorkoutWindow är öppen som dialog, och NewWorkout inte är null så läggs den nya träningen in!
            if (addWorkoutWindow.ShowDialog() == true && addWorkoutWindow.NewWorkout != null)
            {
                Workout newWorkout = addWorkoutWindow.NewWorkout;

                // En debug logg för att se till att träningen läggs till!
                Console.WriteLine($"Adding new workout: {newWorkout.Name}");

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
                 (workout.Name != null && workout.Name.ToLower().Contains(searchText))) &&
                (selectedType == "All" || workout.Type == selectedType)).ToList();

            // Debug-logg
            Console.WriteLine($"Filtered workouts count: {filteredWorkouts.Count}");

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