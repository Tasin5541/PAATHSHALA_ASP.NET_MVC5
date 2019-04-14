using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using a1.Models;
using System.Data.SqlClient;

namespace a1.Controllers
{
    public class ViewVidController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        vidRatingController vr = new vidRatingController();
        // GET: ViewVid
        public ActionResult ViewVid(int? vid_id)
        {
            if (globalVariable.loggedin)
            {
                ViewBag.loggedin = true;
                ViewBag.username = globalVariable.username;
                ViewBag.admin = globalVariable.admin;
            }

            globalVariable.v_id = Convert.ToInt32(vid_id);
            ViewBag.vidLike = vr.isLike(globalVariable.v_id);
            ViewBag.vidDislike = vr.isDislike(globalVariable.v_id);
            ViewBag.vidlikeCount = vr.likeCount(globalVariable.v_id);
            ViewBag.viddislikeCount = vr.dislikeCount(globalVariable.v_id);

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM video v, user_t u WHERE v.U_id=u.U_id AND v.Vid_id='" + vid_id + "'";
            dr = com.ExecuteReader();
            var model = new List<vidVM>();
            while (dr.Read())
            {
                var vid = new vidVM();
                vid.VideoModel.Vid_id = Convert.ToInt32(dr["Vid_id"]);
                vid.VideoModel.U_id = Convert.ToInt32(dr["U_id"]);
                vid.VideoModel.Username = Convert.ToString(dr["Username"]);
                vid.VideoModel.Vid_title = Convert.ToString(dr["Vid_title"]);
                vid.VideoModel.Vid_body = Convert.ToString(dr["Vid_body"]);

                model.Add(vid);
            }
            con.Close();

            con.Open();
            com.CommandText = "SELECT * FROM vid_comment vc, user_t u WHERE vc.U_id=u.U_id AND vc.Vid_id='" + vid_id + "' ORDER BY C_id DESC";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                var vidc = new vidVM();
                vidc.vidCommentModel.U_id = Convert.ToInt32(dr["U_id"]);
                vidc.vidCommentModel.Username = Convert.ToString(dr["Username"]);
                vidc.vidCommentModel.Comment = Convert.ToString(dr["Comment"]);

                model.Add(vidc);
            }
            con.Close();
            return View(model);
        }

        public int countVidComment(int vid_id)
        {
            int count = 0;

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT COUNT (C_id) FROM vid_comment WHERE Vid_id='" + vid_id + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                count = dr.GetInt32(0);
            }
            con.Close();

            return count;
        }

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }

        [HttpPost]
        public ActionResult InsertComment(string comment)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO vid_comment (U_id,Vid_id,Comment ) VALUES('" + globalVariable.uid + "', '" + globalVariable.v_id + "', '" + comment + "')";
            dr = com.ExecuteReader();
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult InsertLike(int? vid_id)
        {
            vr.insertLike(Convert.ToInt32(vid_id));

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult InsertDislike(int? vid_id)
        {
            vr.insertDislike(Convert.ToInt32(vid_id));

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult unRate(int? vid_id)
        {
            vr.unRate(Convert.ToInt32(vid_id));

            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}