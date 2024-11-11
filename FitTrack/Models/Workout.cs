using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Models
{
    public abstract class Workout
    {
        //Egenskaperna!
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public TimeSpan Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public string Notes { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }




        //Metod för att kalkylera kalories som bränts
        public abstract int CalculateCaloriesBurned();
    }
}