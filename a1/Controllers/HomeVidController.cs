using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using a1.Models;
using System.Data.SqlClient;

namespace a1.Controllers
{
    public class HomeVidController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        vidRatingController vr = new vidRatingController();
        ViewVidController vv = new ViewVidController();
        // GET: HomeVid
        public ActionResult HomeVid()
        {
            if (globalVariable.loggedin)
            {
                ViewBag.loggedin = true;
                ViewBag.username = globalVariable.username;
                ViewBag.admin = globalVariable.admin;
                ViewBag.rating = globalVariable.rating;
            }

            connectionString();
            con.Open();
            com.Connection = con;
            if (!globalVariable.searchreq)
            {
                com.CommandText = "SELECT * FROM video v, user_t u WHERE v.U_id=u.U_id ORDER BY v.Vid_id DESC";
            }
            else
            {
                com.CommandText = "SELECT * FROM video v, user_t u WHERE v.U_id=u.U_id AND v.Vid_title LIKE '%" + globalVariable.search + "%'ORDER BY v.Vid_id DESC";
            }
            dr = com.ExecuteReader();
            var model = new List<VideoModel>();
            while (dr.Read())
            {
                var vid = new VideoModel();
                vid.Vid_id = Convert.ToInt32(dr["Vid_id"]);
                vid.U_id = Convert.ToInt32(dr["U_id"]);
                vid.Username = Convert.ToString(dr["Username"]);
                vid.Vid_title = Convert.ToString(dr["Vid_title"]);
                vid.Vid_body = Convert.ToString(dr["Vid_body"]);

                vid.vidLike = vr.isLike(vid.Vid_id);
                vid.vidDislike = vr.isDislike(vid.Vid_id);
                vid.vidlikeCount = vr.likeCount(vid.Vid_id);
                vid.viddislikeCount = vr.dislikeCount(vid.Vid_id);

                vid.commentCount = vv.countVidComment(vid.Vid_id);

                model.Add(vid);
            }
            con.Close();

            //msg count
            var m = new VideoModel();
            MessageController ms = new MessageController();
            m.msgCount = ms.msgCount();

            model.Add(m);

            globalVariable.searchreq = false;
            return View(model);
        }

        public ActionResult Search(string search)
        {
            globalVariable.search = search;
            globalVariable.searchreq = true;

            return RedirectToAction("HomeVid");
        }

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }
    }
}