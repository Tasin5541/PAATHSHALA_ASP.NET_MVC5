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
    public class qa_ratingController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: qa_rating
        public ActionResult Index()
        {
            var qa_rating = db.qa_rating.Include(q => q.qa);
            return View(qa_rating.ToList());
        }

        // GET: qa_rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa_rating qa_rating = db.qa_rating.Find(id);
            if (qa_rating == null)
            {
                return HttpNotFound();
            }
            return View(qa_rating);
        }

        // GET: qa_rating/Create
        public ActionResult Create()
        {
            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title");
            return View();
        }

        // POST: qa_rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Qa_rating_id,Qa_id,U_id,Qa_rating1")] qa_rating qa_rating)
        {
            if (ModelState.IsValid)
            {
                db.qa_rating.Add(qa_rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title", qa_rating.Qa_id);
            return View(qa_rating);
        }

        // GET: qa_rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa_rating qa_rating = db.qa_rating.Find(id);
            if (qa_rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title", qa_rating.Qa_id);
            return View(qa_rating);
        }

        // POST: qa_rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Qa_rating_id,Qa_id,U_id,Qa_rating1")] qa_rating qa_rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qa_rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title", qa_rating.Qa_id);
            return View(qa_rating);
        }

        // GET: qa_rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa_rating qa_rating = db.qa_rating.Find(id);
            if (qa_rating == null)
            {
                return HttpNotFound();
            }
            return View(qa_rating);
        }

        // POST: qa_rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            qa_rating qa_rating = db.qa_rating.Find(id);
            db.qa_rating.Remove(qa_rating);
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
