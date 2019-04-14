using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a1.Models
{
    public class VideoModel
    {
        public int Vid_id { get; set; }
        public int U_id { get; set; }
        public string Username { get; set; }
        public string Vid_title { get; set; }
        public string Vid_body { get; set; }

        public bool vidLike { get; set; }
        public bool vidDislike { get; set; }
        public int vidlikeCount { get; set; }
        public int viddislikeCount { get; set; }

        public int msgCount { get; set; }

        public int commentCount { get; set; }
    }
}