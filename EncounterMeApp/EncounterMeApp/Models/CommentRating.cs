using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class CommentRating
    {
        [PrimaryKey, AutoIncrement]
        public int CommentRatingId { get; set; }
        public int CommentId { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public bool Rating { get; set; }
    }
}
