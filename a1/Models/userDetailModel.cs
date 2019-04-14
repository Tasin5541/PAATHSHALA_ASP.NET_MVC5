using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a1.Models
{
    public class userDetailModel
    {
        public int uid { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public int rating { get; set; }
        public string access { get; set; }
        public string status { get; set; }
        public int msgCount { get; set; }


        public int admin { get; set; }
    }
}