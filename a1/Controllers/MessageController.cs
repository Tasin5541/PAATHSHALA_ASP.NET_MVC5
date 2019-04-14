using a1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace a1.Controllers
{
    public class MessageController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        // GET: Message
        public ActionResult Message()
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
            com.CommandText = "SELECT * FROM user_t WHERE U_id != '" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            var model = new List<messageViewModel>();
            while (dr.Read())
            {
                var ud = new messageViewModel();

                ud.uid = Convert.ToInt32(dr["U_id"]);
                ud.username = Convert.ToString(dr["Username"]);
                ud.msgCount = indimsgCount(ud.uid);

                model.Add(ud);
            }
            con.Close();

            //show message
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM message WHERE S_id = '" + globalVariable.sidMessage + "' AND Destination_id = '" + globalVariable.uid + "'" +
                " OR (S_id = '" + globalVariable.uid + "' AND Destination_id = '" + globalVariable.sidMessage + "')";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                var ud = new messageViewModel();

                ud.uid = globalVariable.uid;
                ud.S_id = Convert.ToInt32(dr["S_id"]);
                ud.Destination_id = Convert.ToInt32(dr["Destination_id"]);
                ud.message1 = Convert.ToString(dr["message"]);

                model.Add(ud);
            }
            con.Close();

            //message username
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM user_t WHERE U_id = '" + globalVariable.sidMessage + "'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                var ud = new messageViewModel();
                
                ud.sendername = Convert.ToString(dr["Username"]);

                model.Add(ud);
            }
            con.Close();
            
            return View(model);
        }

        public ActionResult userMessage(int? sid)
        {
            globalVariable.sidMessage = Convert.ToInt32(sid);

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "UPDATE message SET reads = '0' WHERE S_id = '" + globalVariable.sidMessage + "' AND Destination_id = '" + globalVariable.uid + "'";
            dr = com.ExecuteReader();
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpPost]
        public ActionResult insertMessage(string messageText)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "INSERT INTO message (S_id, Destination_id, message) " +
                "VALUES ('" + globalVariable.uid + "', '" + globalVariable.sidMessage + "', '" + messageText + "')";
            dr = com.ExecuteReader();
            
            con.Close();

            return Redirect(Request.UrlReferrer.ToString());
        }

        public int msgCount()
        {
            int mCount = 0;

            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT COUNT (M_id) FROM message WHERE Destination_id = '" + globalVariable.uid + "'" +
                " AND reads = '1'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                mCount = dr.GetInt32(0);
            }
            con.Close();

            return mCount;
        }

        //individual msg count
        public int indimsgCount(int sid)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand com = new SqlCommand();
            SqlDataReader dr;

            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
            con.Open();

            int mCount = 0;
            
            com.Connection = con;
            com.CommandText = "SELECT COUNT (M_id) FROM message WHERE S_id = '" + sid + "' AND Destination_id = '" + globalVariable.uid + "'" +
                " AND reads = '1'";
            dr = com.ExecuteReader();
            while (dr.Read())
            {
                mCount = dr.GetInt32(0);
            }
            con.Close();

            return mCount;
        }

        void connectionString()
        {
            con.ConnectionString = "data source=.\\SQLEXPRESS; database=paathshala; integrated security=SSPI;";
        }
    }
}