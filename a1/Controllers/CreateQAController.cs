using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using a1.Models;
using System.Data.SqlClient;

namespace a1.Controllers
{
    public class CreateQAController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();
        
        qa q = new qa();
        // GET: CreateQA
        public ActionResult CreateQA()
        {
            if (globalVariable.loggedin)
            {
                ViewBag.loggedin = true;
                ViewBag.username = globalVariable.username;
                ViewBag.admin = globalVariable.admin;
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQA([Bind(Include = "Qa_id,Qa_title,Qa_body")] qa qa)
        {
            qa.U_id = globalVariable.uid;
            if (ModelState.IsValid)
            {
                db.qas.Add(qa);
                db.SaveChanges();
                return Redirect("/HomeQA/HomeQA");
            }

            //ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", qa.U_id);
            return View(qa);
        }
    }
}