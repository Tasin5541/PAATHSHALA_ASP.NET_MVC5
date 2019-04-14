using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using a1.Models;

namespace a1.Controllers
{
    public class HomeQAController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        qaRatingController qr = new qaRatingController();
        ViewPostController vp = new ViewPostController();
        

        // GET: HomeQA
        public ActionResult HomeQA()
        {
            ViewBag.admin = 0;
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
                com.CommandText = "SELECT * FROM qa q, user_t u WHERE q.U_id=u.U_id ORDER BY q.Qa_id DESC";
            }
            else
            {
                com.CommandText = "SELECT * FROM qa q, user_t u WHERE q.U_id=u.U_id AND q.Qa_title LIKE '%" + globalVariable.search + "%' ORDER BY q.Qa_id DESC";
            }
            dr = com.ExecuteReader();
            var model = new List<qaModel>();
            while(dr.Read())
            {
                var qa = new qaModel();
                qa.Qa_id = Convert.ToInt32(dr["Qa_id"]);
                qa.U_id = Convert.ToInt32(dr["U_id"]);
                qa.Username = Convert.ToString(dr["Username"]);
                qa.Qa_title = Convert.ToString(dr["Qa_title"]);
                qa.Qa_body = Convert.ToString(dr["Qa_body"]);

                qa.qaLike = qr.isLike(qa.Qa_id);
                qa.qaDislike = qr.isDislike(qa.Qa_id);
                qa.qalikeCount = qr.likeCount(qa.Qa_id);
                qa.qadislikeCount = qr.dislikeCount(qa.Qa_id);

                qa.commentCount = vp.countQAComment(qa.Qa_id);

                model.Add(qa);
            }

            //msg count
            var m = new qaModel();
            MessageController ms = new MessageController();
            m.msgCount = ms.msgCount();

            model.Add(m);

            globalVariable.searchreq = false;
            con.Close();
            return View(model);
        }

        public ActionResult Login(string email, string password)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM user_t WHERE Email='" + email + "' AND Password = '" + password + "' AND ban!='1'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                globalVariable.loggedin = true;
                globalVariable.uid = Convert.ToInt32(dr["U_id"]);
                globalVariable.username = Convert.ToString(dr["Username"]);
                globalVariable.admin = Convert.ToInt32(dr["Admin_check"]);
                globalVariable.rating = Convert.ToInt32(dr["Rating"]);

                if (globalVariable.admin==1)
                {
                    return Redirect("/AdminPanel/AdminPanel");
                }
            }
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult Logout()
        {
            globalVariable.loggedin = false;
            globalVariable.uid = 0;
            globalVariable.username = "";
            globalVariable.admin = 0;

            return Redirect("/HomeQA/HomeQA");
        }

        public ActionResult Search(string search)
        {
            globalVariable.search = search;
            globalVariable.searchreq = true;

            return RedirectToAction("HomeQA");
        }

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }
    }
}