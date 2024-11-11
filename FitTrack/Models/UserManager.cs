using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Models
{
    public static class UserManager
    {
        //Lista med användare som ska lagras för att behålla inlogg och skapade konton.
        public static List<User> Users = new List<User>();


        //En metod för att lägga till användare.
        public static void AddUser(User user)
        {
            Users.Add(user);
        }


        //Denna kod ser om användarnamnet och lösenordet matchar en användare.
        public static bool DoesUserExist(string username, string password)
        {
            return Users.Exists(user => user.Username == username && user.Password == password);
        }



    }
}
