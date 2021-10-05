﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class Player
    {
        public enum PlayerType { Creator, Explorer};
        public string NickName { get; set; }
        public string Points { get; set; }
        public string ProfilePic { get; set; }
    }
}
