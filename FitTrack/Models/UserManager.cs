using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Models
{
    public static class UserManager
    {
        // Lista som behåller alla användare.
        public static List<User> Users = new List<User>();

        // En metod som gör kontroll om användare finns,
        public static bool IsUsernameTaken(string username)
        {
            return Users.Exists(user => user.Username == username);
        }

        // Metod för att lägga till användare.
        public static void AddUser(User user)
        {
            user.Workouts ??= new List<Workout>(); // Initiera listan om den är null
            Users.Add(user);
        }

        // Kontrollera om användare matchar det som finns i den lokala databasen.
        public static bool AuthenticateUser(string username, string password)
        {
            return Users.Exists(user => user.Username == username && user.Password == password);
        }

        // Denna metod finns för att skapa en standard användare.
        public static void InitializeDefaultUsers()
        {
            // Denna finns för att lägga till en admin om det inte redan finns.
            if (!IsUsernameTaken("admin"))
            {
                Users.Add(new User
                {
                    Username = "admin",
                    Password = "password",
                    Country = "Sweden"
                });
            }

            // Lägger till en user användare med simpla träningspass!
            if (!IsUsernameTaken("user"))
            {
                Users.Add(new User
                {
                    Username = "user",
                    Password = "password",
                    Country = "Sweden",
                    Workouts = new List<Workout>
            {
                // Lägger till cardio-träningspass för "user" användaren
                        new CardioWorkout
                {
                    Name = "Jogging", Type = "Cardio", Notes = "Morning run", Distance = 5000
                        },
                // Lägg till strength-träningspass för "user" användaren
                        new StrengthWorkout
                {
                    Name = "Weight Lifting", Type = "Strength", Notes = "Upper body workout", Repetitions = 15
                        }
            }
                });
            }
        }

        // Denna metod hämtar alla träningspass som finns i listan. + En debugging så att vi ser hur mycket det finns.
        public static List<Workout> GetAllWorkouts()
        {
            var allWorkouts = Users.SelectMany(user => user.Workouts ?? new List<Workout>()).ToList();
            Console.WriteLine($"Total workouts found: {allWorkouts.Count}");
            return allWorkouts;
        }

        // Denna metod hämtar träningspass baserat på användarnamnet.
        public static User GetUserByUsername(string username)
        {
            return Users.Find(user => user.Username == username);
        }
    }
}