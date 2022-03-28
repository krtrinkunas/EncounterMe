using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class LocationRating
    {
        [PrimaryKey, AutoIncrement]
        public int LocationRatingId { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
    }
}
