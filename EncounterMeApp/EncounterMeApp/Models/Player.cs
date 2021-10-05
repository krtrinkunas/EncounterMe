using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class Player : IComparable
    {
        public enum PlayerType { Creator, Explorer};
        public string NickName { get; set; }
        public int Points { get; set; }
        public string ProfilePic { get; set; }

        public int CompareTo(object obj)
        {
            if (!(obj is Player))
                throw new ArgumentException("Object is not a Player!");

            Player other = (Player)obj;
            return Points.CompareTo(other.Points);
        }
    }
}
