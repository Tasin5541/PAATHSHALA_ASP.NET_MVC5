using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace a1.Controllers
{
    public static class globalVariable
    {
        public static string username;
        public static int uid, q_id, v_id, admin, rating;
        public static bool loggedin=false;

        public static bool searchreq = false;
        public static string search;

        public static int sidMessage;
    }
}