using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a1.Models
{
    public class adminModel
    {
        public int userCount { get; set; }
        public int qaCount { get; set; }
        public int vidCount { get; set; }
        public int msgCount { get; set; }

        public string adminUser { get; set; }
        public string bannedUser { get; set; }
        public string tutorUser { get; set; }
    }
}