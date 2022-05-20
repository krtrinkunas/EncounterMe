using System;
using SQLite;
using System.Collections.Generic;
using System.Text;

namespace EncounterMeApp.Models
{
    public class FriendRequest
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get; set;
        }
        public int SenderID
        {
            get; set;
        }
        public int ReceiverID
        {
            get; set;
        }

        //better to not have multiple of these, no need to keep denied or accepted
        public int Status
        {
            get; set;
        }
    }
}
