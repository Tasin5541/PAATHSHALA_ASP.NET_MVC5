using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a1.Models
{
    public class messageViewModel
    {
        public int uid { get; set; }
        public string username { get; set; }
        public string sendername { get; set; }

        public int M_id { get; set; }
        public int S_id { get; set; }
        public int Destination_id { get; set; }
        public string message1 { get; set; }
        public int reads { get; set; }

        public int msgCount { get; set; }
    }
}