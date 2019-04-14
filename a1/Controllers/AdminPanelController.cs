using a1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace a1.Controllers
{
    public class AdminPanelController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        adminModel am = new adminModel();
        // GET: AdminPanel
        public ActionResult AdminPanel()
        {
            if (globalVariable.loggedin)
            {
                ViewBag.loggedin = true;
                ViewBag.username = globalVariable.username;
                ViewBag.admin = globalVariable.admin;
            }

            var model = new List<adminModel>();
            connectionString();
            con.Open();
            com.Connection = con;
            //user count
            com.CommandText = "SELECT COUNT (U_id) FROM user_t";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                am.userCount = dr.GetInt32(0);
            }
            con.Close();

            //qa count
            con.Open();
            com.CommandText = "SELECT COUNT (Qa_id) FROM qa";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                am.qaCount = dr.GetInt32(0);
            }
            con.Close();

            //vid count
            con.Open();
            com.CommandText = "SELECT COUNT (Vid_id) FROM video";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                am.vidCount = dr.GetInt32(0);
            }
            con.Close();

            //msg count
            MessageController ms = new MessageController();
            am.msgCount = ms.msgCount();

            model.Add(am);

            //admin
            con.Open();
            com.CommandText = "SELECT * FROM user_t WHERE Admin_check = '1'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                adminModel adName = new adminModel();
                adName.adminUser = Convert.ToString(dr["Username"]);
                model.Add(adName);
            }
            con.Close();

            //ban
            con.Open();
            com.CommandText = "SELECT * FROM user_t WHERE ban = '1'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                adminModel banName = new adminModel();
                banName.bannedUser = Convert.ToString(dr["Username"]);
                model.Add(banName);
            }
            con.Close();

            //tutor
            con.Open();
            com.CommandText = "SELECT * FROM user_t WHERE Rating >= '500'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                adminModel tutorName = new adminModel();
                tutorName.tutorUser = Convert.ToString(dr["Username"]);
                model.Add(tutorName);
            }
            con.Close();

            return View(model);
        }

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }
    }
}