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
    public class user_tController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: user_t
        public ActionResult Index()
        {
            return View(db.user_t.ToList());
        }

        // GET: user_t/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_t user_t = db.user_t.Find(id);
            if (user_t == null)
            {
                return HttpNotFound();
            }
            return View(user_t);
        }

        // GET: user_t/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: user_t/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "U_id,Username,Email,Password,Admin_check,ban")] user_t user_t)
        {
            if (ModelState.IsValid)
            {
                db.user_t.Add(user_t);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user_t);
        }

        // GET: user_t/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_t user_t = db.user_t.Find(id);
            if (user_t == null)
            {
                return HttpNotFound();
            }
            return View(user_t);
        }

        // POST: user_t/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "U_id,Username,Email,Password,Admin_check,ban")] user_t user_t)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_t).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user_t);
        }

        // GET: user_t/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_t user_t = db.user_t.Find(id);
            if (user_t == null)
            {
                return HttpNotFound();
            }
            return View(user_t);
        }

        // POST: user_t/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_t user_t = db.user_t.Find(id);
            db.user_t.Remove(user_t);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
