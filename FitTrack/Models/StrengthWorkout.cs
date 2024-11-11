using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Models
{
    public class StrengthWorkout : Workout
    {
        // En int som möjliggör att skapa antal repititioner
        public int Repetitions { get; set; }

        // Metod som visar brända kalories för varje repitition av rörelsen.
        public override int CalculateCaloriesBurned()
        {
            CaloriesBurned = Repetitions * 1;
            return CaloriesBurned;
        }


        public StrengthWorkout()
        {
            Type = "Strength"; // Styrketräning!
            Notes = "Enter your notes here";
        }
    }
}