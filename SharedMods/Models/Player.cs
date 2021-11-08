//using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public enum PlayerType { Creator, Explorer };
    public class Player : IComparable
    {
        //[PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public PlayerType Type { get; set; }
        public string NickName { get; set; }
        public int Points { get; set; }
        public int LocationsVisited { get; set; }
        public int LocationsOwned { get; set; }
        public string ProfilePic { get; set; }
        public string Email { get; set; }

        public int CompareTo(object obj)
        {
            if (!(obj is Player))
                throw new ArgumentException("Object is not a Player!");

            Player other = (Player)obj;
            return Points.CompareTo(other.Points);
        }
    }
}
