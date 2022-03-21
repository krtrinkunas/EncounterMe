using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class CaptureAttempt
    {
        [PrimaryKey, AutoIncrement]
        public int CaptureAttemptId { get; set; }
        public int LocationId { get; set; }
        public int UserId { get; set; }
        public bool HasCaptured { get; set; }
    }
}
