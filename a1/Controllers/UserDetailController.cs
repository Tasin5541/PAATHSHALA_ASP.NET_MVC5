using a1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace a1.Controllers
{
    public class UserDetailController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: UserDetail
        public ActionResult UserDetail()
        {
            if (globalVariable.loggedin)
            {
                ViewBag.loggedin = true;
                ViewBag.username = globalVariable.username;
                ViewBag.admin = globalVariable.admin;
            }

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM user_t";
            dr = com.ExecuteReader();
            var model = new List<userDetailModel>();
            while (dr.Read())
            {
                var ud = new userDetailModel();

                ud.uid = Convert.ToInt32(dr["U_id"]);
                ud.username = Convert.ToString(dr["Username"]);
                ud.email = Convert.ToString(dr["Email"]);
                if (Convert.ToInt32(dr["Admin_check"]) == 1)
                    ud.access = "Admin Privilege";
                else
                    ud.access = "Limited";
                if (Convert.ToInt32(dr["ban"]) == 1)
                    ud.status = "Banned";
                else
                    ud.status = "Active";
                ud.rating = Convert.ToInt32(dr["Rating"]);

                model.Add(ud);
            }
            con.Close();

            //msg count
            var m = new userDetailModel();
            MessageController ms = new MessageController();
            m.msgCount = ms.msgCount();

            model.Add(m);

            return View(model);
        }

        public ActionResult makeAdmin(int? uid)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE user_t SET Admin_check = 1 WHERE U_id = '" + uid + "'";
            dr = com.ExecuteReader();
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult removeAdmin(int? uid)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE user_t SET Admin_check = 0 WHERE U_id = '" + uid + "'";
            dr = com.ExecuteReader();
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult banUser(int? uid)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE user_t SET ban = 1 WHERE U_id = '" + uid + "'";
            dr = com.ExecuteReader();
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public ActionResult unbanUser(int? uid)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE user_t SET ban = 0 WHERE U_id = '" + uid + "'";
            dr = com.ExecuteReader();
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }
    }
}