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

            RefreshWorkoutList();
        }


        public void LoadAllWorkouts()
        {
            WorkoutList = UserManager.GetAllWorkouts(); // Hämtar alla träningspass som finns.
            RefreshWorkoutList();
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



        // Uppdaterar träningslistan med redigeringar osv.
        private void RefreshWorkoutList()
        {
            WorkoutsList.ItemsSource = null;
            WorkoutsList.ItemsSource = WorkoutList;
        }



        //Lägger till ett träningspass i listan.
        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            var addWorkoutWindow = new AddWorkoutWindow();

            // Öppna fönstret och lägg till träningspasset
            if (addWorkoutWindow.ShowDialog() == true)
            {
                Workout newWorkout = addWorkoutWindow.NewWorkout;
                WorkoutList.Add(newWorkout);
                RefreshWorkoutList();
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
                    RefreshWorkoutList();

                    MessageBox.Show("Workout removed successfully.");
                }
                else
                {
                    // Om en träning inte valts så kommet ett felmeddelande
                    MessageBox.Show("Please select a workout to remove.");
                }
            }

        }


        private void FilterWorkouts()
        {

            // Filtrerar träningspassen genom att söka.
            string searchText = SearchTextBox.Text.ToLower();

            // Hämtar valda kategorin Cardio eller strength som valts i comboListan
            string selectedType = (WorkoutTypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "All";

            // Filtrerar träningspassen baserat på det som skrivits.
            var filteredWorkouts = WorkoutList.Where(workout =>

                // Denna kod läser genom så att det Name notes eller beskrivning innehåller söktexten.
                (string.IsNullOrEmpty(searchText) ||
                 (workout.Name != null && workout.Name.ToLower().Contains(searchText)) ||
                 (workout.Notes != null && workout.Notes.ToLower().Contains(searchText))) &&

                // Kontrollera att typen matchar eller är inställd på "All"
                (selectedType == "All" || workout.Type == selectedType)).ToList();

            // Listan uppdateras så att sökningen sker.
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

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            string infoMessage = "FitTrack is an app manufactures by Nawid Rahim, who is the lead programmer in this project." +
                         " FitTrack enables users to make a workout plan so that the user has a easier time following their schedule." +
                         " The app supports every type of workout you need, but specifically Cardio and Strength!" +
                         " The app is designed to be simple and user-friendly, and we try our hardest to make it just that!" +
                         "You can register new workouts, edit them as well as search for workouts you've already made on this screen!" +
                         "FitTrack has a mission to make it as easy as possible, and if you have any questions about the program you can Email us!";

            MessageBox.Show(infoMessage, "About FitTrack", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {

            var LogOutWindow = new LogOutWindow();
            LogOutWindow.ShowDialog();

        }
    }
}