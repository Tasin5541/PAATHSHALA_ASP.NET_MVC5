using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a1.Models
{
    public class vidCommentModel
    {
        public int C_id { get; set; }
        public int U_id { get; set; }
        public string Username { get; set; }
        public int Vid_id { get; set; }
        public string Comment { get; set; }
    }
}