using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class Friend
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get; set;
        }
        public int Friend1ID
        {
            get; set;
        }
        public int Friend2ID
        {
            get; set;
        }
    }
}
