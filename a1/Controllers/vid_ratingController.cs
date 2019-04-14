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
    public class vid_ratingController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: vid_rating
        public ActionResult Index()
        {
            var vid_rating = db.vid_rating.Include(v => v.video);
            return View(vid_rating.ToList());
        }

        // GET: vid_rating/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vid_rating vid_rating = db.vid_rating.Find(id);
            if (vid_rating == null)
            {
                return HttpNotFound();
            }
            return View(vid_rating);
        }

        // GET: vid_rating/Create
        public ActionResult Create()
        {
            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title");
            return View();
        }

        // POST: vid_rating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Vid_rating_id,Vid_id,U_id,Vid_rating1")] vid_rating vid_rating)
        {
            if (ModelState.IsValid)
            {
                db.vid_rating.Add(vid_rating);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title", vid_rating.Vid_id);
            return View(vid_rating);
        }

        // GET: vid_rating/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vid_rating vid_rating = db.vid_rating.Find(id);
            if (vid_rating == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title", vid_rating.Vid_id);
            return View(vid_rating);
        }

        // POST: vid_rating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Vid_rating_id,Vid_id,U_id,Vid_rating1")] vid_rating vid_rating)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vid_rating).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title", vid_rating.Vid_id);
            return View(vid_rating);
        }

        // GET: vid_rating/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vid_rating vid_rating = db.vid_rating.Find(id);
            if (vid_rating == null)
            {
                return HttpNotFound();
            }
            return View(vid_rating);
        }

        // POST: vid_rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vid_rating vid_rating = db.vid_rating.Find(id);
            db.vid_rating.Remove(vid_rating);
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
