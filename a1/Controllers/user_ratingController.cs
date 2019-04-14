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
    public class user_ratingController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: user_rating
        public ActionResult Index()
        {
            var user_rating = db.user_rating.Include(u => u.user_t);
            return View(user_rating.ToList());
        }

        // GET: user_rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_rating user_rating = db.user_rating.Find(id);
            if (user_rating == null)
            {
                return HttpNotFound();
            }
            return View(user_rating);
        }

        // GET: user_rating/Create
        public ActionResult Create()
        {
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username");
            return View();
        }

        // POST: user_rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "User_rating_id,U_id,User_rating1,User_tag")] user_rating user_rating)
        {
            if (ModelState.IsValid)
            {
                db.user_rating.Add(user_rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", user_rating.U_id);
            return View(user_rating);
        }

        // GET: user_rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_rating user_rating = db.user_rating.Find(id);
            if (user_rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", user_rating.U_id);
            return View(user_rating);
        }

        // POST: user_rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "User_rating_id,U_id,User_rating1,User_tag")] user_rating user_rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user_rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", user_rating.U_id);
            return View(user_rating);
        }

        // GET: user_rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user_rating user_rating = db.user_rating.Find(id);
            if (user_rating == null)
            {
                return HttpNotFound();
            }
            return View(user_rating);
        }

        // POST: user_rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user_rating user_rating = db.user_rating.Find(id);
            db.user_rating.Remove(user_rating);
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
