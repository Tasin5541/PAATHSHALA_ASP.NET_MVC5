using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a1.Models
{
    public class qaModel
    {
        public int Qa_id { get; set; }
        public int U_id { get; set; }
        public string Username { get; set; }
        public string Qa_title { get; set; }
        public string Qa_body { get; set; }

        public bool qaLike { get; set; }
        public bool qaDislike { get; set; }
        public int qalikeCount { get; set; }
        public int qadislikeCount { get; set; }

        public int msgCount { get; set; }

        public int commentCount { get; set; }
    }
}