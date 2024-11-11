using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.Models
{
    public class Person
    {

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual void SignIn()
        {

        }

    }
}
