using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Models
{

    //Klassen ärver från en annan klass, i detta fall är det ''Person''
    public class User : Person
    {

        //Landet användaren är från
        public string Country { get; set; }

        //Säkerhetsfråga som användaren ska svara på för att kunna få nytt lösenord, eller skapa en användare.
        public string SecurityQuestion { get; set; }   


        //Svar på säkerhetsfrågan.
        public string SecurityAnswer { get; set; }


        public List<Workout> Workouts { get; set; } = new List<Workout>();
    }
}
 