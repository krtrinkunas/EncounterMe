using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class MyLocation
    {
        public double positionX { get; set; }
        public double positionY { get; set; }
        public string NAME { get; set; }
        public int points { get; set; }
        public string owner { get; set; }
        public int Id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public int rating { get; set; }
        public int numberOfRatings { get; set; }
    }
}
