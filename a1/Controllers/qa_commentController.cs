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
    public class qa_commentController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: qa_comment
        public ActionResult Index()
        {
            var qa_comment = db.qa_comment.Include(q => q.qa).Include(q => q.user_t);
            return View(qa_comment.ToList());
        }

        // GET: qa_comment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa_comment qa_comment = db.qa_comment.Find(id);
            if (qa_comment == null)
            {
                return HttpNotFound();
            }
            return View(qa_comment);
        }

        // GET: qa_comment/Create
        public ActionResult Create()
        {
            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title");
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username");
            return View();
        }

        // POST: qa_comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "C_id,U_id,Qa_id,Comment")] qa_comment qa_comment)
        {
            if (ModelState.IsValid)
            {
                db.qa_comment.Add(qa_comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title", qa_comment.Qa_id);
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", qa_comment.U_id);
            return View(qa_comment);
        }

        // GET: qa_comment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa_comment qa_comment = db.qa_comment.Find(id);
            if (qa_comment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title", qa_comment.Qa_id);
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", qa_comment.U_id);
            return View(qa_comment);
        }

        // POST: qa_comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "C_id,U_id,Qa_id,Comment")] qa_comment qa_comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qa_comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Qa_id = new SelectList(db.qas, "Qa_id", "Qa_title", qa_comment.Qa_id);
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", qa_comment.U_id);
            return View(qa_comment);
        }

        // GET: qa_comment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa_comment qa_comment = db.qa_comment.Find(id);
            if (qa_comment == null)
            {
                return HttpNotFound();
            }
            return View(qa_comment);
        }

        // POST: qa_comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            qa_comment qa_comment = db.qa_comment.Find(id);
            db.qa_comment.Remove(qa_comment);
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
