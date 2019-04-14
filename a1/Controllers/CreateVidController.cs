using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using a1.Models;

namespace a1.Controllers
{
    public class CreateVidController : Controller
    {
        private paathshalaEntities db = new paathshalaEntities();
        // GET: CreateVid
        [ValidateInput(false)]
        public ActionResult CreateVid()
        {
            if (globalVariable.loggedin)
            {
                ViewBag.loggedin = true;
                ViewBag.username = globalVariable.username;
                ViewBag.admin = globalVariable.admin;
            }

            return View();
        }
        
        public string UploadVideo(HttpFileCollection video)
        {
            if (video.Count <= 0) return null;
            var fileName = Path.GetFileName(video.Get(0).FileName);
            var path = Path.Combine(Server.MapPath("~/Content/assets/uploads"), fileName);
            // save video here
            video[0].SaveAs(path);
            return fileName;
        }

        public class Video
        {
            public HttpFileCollection File { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVid([Bind(Include = "Vid_id,Vid_title")] video vid)
        {
            var video = System.Web.HttpContext.Current.Request.Files;
            var VideoFileName = UploadVideo(video);
            vid.Vid_body = VideoFileName;
            vid.U_id = globalVariable.uid;
            if (ModelState.IsValid)
            {
                db.videos.Add(vid);
                db.SaveChanges();
                return Redirect("/HomeVid/HomeVid");
            }

            //ViewBag.U_id = new SelectList(db.user_t, "U_id", "Username", video.U_id);
            return View(vid);
        }
    }

}