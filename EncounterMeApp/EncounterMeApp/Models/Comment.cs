using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class Comment
    {
        [PrimaryKey, AutoIncrement]
        public int CommentId { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public int Rating { get; set; }
        public bool HasSpoilers { get; set; }
        public bool HasCaptured { get; set; }
        public DateTime TimePosted { get; set; }
    }
}
