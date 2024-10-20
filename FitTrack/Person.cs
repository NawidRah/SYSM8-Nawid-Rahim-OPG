using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack
{
    public abstract class Person
    {

        public abstract string Username { get; set; }

        public abstract string Password { get; set; }



        public void SignIn(string username, string password)
        {
            username = Username;
            password = Password;

            

        }

        




    }
}
