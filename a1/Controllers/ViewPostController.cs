using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using a1.Models;

namespace a1.Controllers
{
    public class ViewPostController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        qaRatingController qr = new qaRatingController();
        // GET: ViewPost
        public ActionResult ViewPost(int? qa_id)
        {
            if (globalVariable.loggedin)
            {
                ViewBag.loggedin = true;
                ViewBag.username = globalVariable.username;
                ViewBag.admin = globalVariable.admin;
            }

            globalVariable.q_id = Convert.ToInt32(qa_id);
            ViewBag.qaLike = qr.isLike(globalVariable.q_id);
            ViewBag.qaDislike = qr.isDislike(globalVariable.q_id);
            ViewBag.qalikeCount = qr.likeCount(globalVariable.q_id);
            ViewBag.qadislikeCount = qr.dislikeCount(globalVariable.q_id);

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM qa q, user_t u WHERE q.U_id=u.U_id AND q.Qa_id='"+ qa_id+"'";
            dr = com.ExecuteReader();
            var model = new List<qaVM>();
            while (dr.Read())
            {
                var qa = new qaVM();
                qa.qaModel.Qa_id = Convert.ToInt32(dr["Qa_id"]);
                qa.qaModel.U_id = Convert.ToInt32(dr["U_id"]);
                qa.qaModel.Username = Convert.ToString(dr["Username"]);
                qa.qaModel.Qa_title = Convert.ToString(dr["Qa_title"]);
                qa.qaModel.Qa_body = Convert.ToString(dr["Qa_body"]);

                model.Add(qa);
            }
            con.Close();

            con.Open();
            com.CommandText = "SELECT * FROM qa_comment qc, user_t u WHERE qc.U_id=u.U_id AND qc.Qa_id='" + qa_id + "' ORDER BY C_id DESC";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                var qac = new qaVM();
                qac.qaCommentModel.U_id = Convert.ToInt32(dr["U_id"]);
                qac.qaCommentModel.Username = Convert.ToString(dr["Username"]);
                qac.qaCommentModel.Comment = Convert.ToString(dr["Comment"]);

                model.Add(qac);
            }
            con.Close();
            return View(model);

        }

        public int countQAComment(int qa_id)
        {
            int count = 0;

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT COUNT (C_id) FROM qa_comment WHERE Qa_id='" + qa_id + "'";
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
            com.CommandText = "INSERT INTO Qa_comment (U_id,Qa_id,Comment ) VALUES('" + globalVariable.uid + "', '" + globalVariable.q_id + "', '" + comment + "')";
            dr = com.ExecuteReader();
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }
        
        public ActionResult InsertLike(int? qa_id)
        {
            qr.insertLike(Convert.ToInt32(qa_id));

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult InsertDislike(int? qa_id)
        {
            qr.insertDislike(Convert.ToInt32(qa_id));

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult unRate(int? qa_id)
        {
            qr.unRate(Convert.ToInt32(qa_id));

            return Redirect(Request.UrlReferrer.ToString());
        }

    }
}