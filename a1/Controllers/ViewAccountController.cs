using a1.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace a1.Controllers
{
    public class ViewAccountController : Controller
    {
        SqlConnection con = new SqlConnection();
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;

        qaRatingController qr = new qaRatingController();

        // GET: ViewAccount
        public ActionResult ViewAccount(int? uid)
        {
            connectionString();
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM user_t WHERE U_id='" + uid + "'";
            dr = com.ExecuteReader();
            var model = new List<userDetailModel>();
            while (dr.Read())
            {
                var ud = new userDetailModel();
                
                ud.username = Convert.ToString(dr["Username"]);
                ud.email = Convert.ToString(dr["Email"]);
                ud.rating = Convert.ToInt32(dr["Rating"]);
                ud.admin = Convert.ToInt32(dr["Admin_check"]);

                model.Add(ud);
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