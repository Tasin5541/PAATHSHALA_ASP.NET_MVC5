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
    public class qasController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();

        // GET: qas
        public ActionResult Index()
        {
            var qas = db.qas.Include(q => q.user_t);
            return View(qas.ToList());
        }

        // GET: qas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa qa = db.qas.Find(id);
            if (qa == null)
            {
                return HttpNotFound();
            }
            return View(qa);
        }

        // GET: qas/Create
        public ActionResult Create()
        {
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username");
            return View();
        }

        // POST: qas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Qa_id,U_id,Qa_title,Qa_body")] qa qa)
        {
            if (ModelState.IsValid)
            {
                db.qas.Add(qa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", qa.U_id);
            return View(qa);
        }

        // GET: qas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa qa = db.qas.Find(id);
            if (qa == null)
            {
                return HttpNotFound();
            }
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", qa.U_id);
            return View(qa);
        }

        // POST: qas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Qa_id,U_id,Qa_title,Qa_body")] qa qa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", qa.U_id);
            return View(qa);
        }

        // GET: qas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            qa qa = db.qas.Find(id);
            if (qa == null)
            {
                return HttpNotFound();
            }
            return View(qa);
        }

        // POST: qas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            qa qa = db.qas.Find(id);
            db.qas.Remove(qa);
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
