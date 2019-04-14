using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using a1.Models;

namespace a1.Controllers
{
    public class SignUpController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: SignUp
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "Username,Email,Password")] user_t user_t)
        {
            if (ModelState.IsValid)
            {
                db.user_t.Add(user_t);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(user_t);
        }
    }
}