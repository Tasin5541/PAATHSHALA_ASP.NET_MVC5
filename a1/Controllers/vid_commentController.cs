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
    public class vid_commentController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: vid_comment
        public ActionResult Index()
        {
            var vid_comment = db.vid_comment.Include(v => v.user_t).Include(v => v.video);
            return View(vid_comment.ToList());
        }

        // GET: vid_comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vid_comment vid_comment = db.vid_comment.Find(id);
            if (vid_comment == null)
            {
                return HttpNotFound();
            }
            return View(vid_comment);
        }

        // GET: vid_comment/Create
        public ActionResult Create()
        {
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username");
            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title");
            return View();
        }

        // POST: vid_comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_id,U_id,Vid_id,Comment")] vid_comment vid_comment)
        {
            if (ModelState.IsValid)
            {
                db.vid_comment.Add(vid_comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", vid_comment.U_id);
            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title", vid_comment.Vid_id);
            return View(vid_comment);
        }

        // GET: vid_comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vid_comment vid_comment = db.vid_comment.Find(id);
            if (vid_comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", vid_comment.U_id);
            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title", vid_comment.Vid_id);
            return View(vid_comment);
        }

        // POST: vid_comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_id,U_id,Vid_id,Comment")] vid_comment vid_comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vid_comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", vid_comment.U_id);
            ViewBag.Vid_id = new SelectList(db.videos, "Vid_id", "Vid_title", vid_comment.Vid_id);
            return View(vid_comment);
        }

        // GET: vid_comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            vid_comment vid_comment = db.vid_comment.Find(id);
            if (vid_comment == null)
            {
                return HttpNotFound();
            }
            return View(vid_comment);
        }

        // POST: vid_comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            vid_comment vid_comment = db.vid_comment.Find(id);
            db.vid_comment.Remove(vid_comment);
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
