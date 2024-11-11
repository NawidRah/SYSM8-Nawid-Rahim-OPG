using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Models
{
    public class CardioWorkout : Workout
    {
        // Egenskap som lagrar distansen (Distance)

        public int Distance { get; set; }

        // Metod för att lagra kalorier som bränns på antalet distans.
        public override int CalculateCaloriesBurned()
        {
            // beräkning av kaloriförbrukning: 2 kalorier per meter
            CaloriesBurned = Distance * 2;
            return CaloriesBurned;
        }


        public CardioWorkout()
        {
            Type = "Cardio"; // Kardio!
            Notes = "Enter your notes here";
        }
    }
}