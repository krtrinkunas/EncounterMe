using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class Block
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get; set;
        }
        public int BlockedByID
        {
            get; set;
        }
        public int UserBlockedID
        {
            get;set;
        }

    }
}
