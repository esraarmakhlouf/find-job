using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using find_job.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace find_job.Controllers
{
    [Authorize]
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Jops
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var jops = db.Jops.Include(j => j.Category).Where(a=>a.userId==userID);
            return View(jops.ToList());
        }

        // GET: Jops/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job jop = db.Jops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            return View(jop);
        }
        [Authorize]
        // GET: Jops/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.categories, "id", "categoryName");

            return View();
        }

        // POST: Jops/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( Job job,HttpPostedFileBase Upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), Upload.FileName);
                Upload.SaveAs(path);
                job.jopImage = Upload.FileName;
                job.userId = User.Identity.GetUserId();
                job.jobDate = DateTime.Now;
                db.Jops.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.categories, "id", "categoryName", job.CategoryId);
            return View(job);
        }

        // GET: Jops/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job jop = db.Jops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.categories, "id", "categoryName", jop.CategoryId);
            return View(jop);
        }

        // POST: Jops/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Job jop,HttpPostedFileBase Upload)
        {
            if (ModelState.IsValid)
            {
                string oldPath = Path.Combine(Server.MapPath("~/Uploads"), jop.jopImage);
                if (Upload!=null)
                {
                   System.IO.File.Delete(oldPath);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), Upload.FileName);
                    Upload.SaveAs(path);
                    jop.jopImage = Upload.FileName;
                }
                jop.userId= User.Identity.GetUserId();
                db.Entry(jop).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.categories, "id", "categoryName", jop.CategoryId);
            return View(jop);
        }

        // GET: Jops/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job jop = db.Jops.Find(id);
            if (jop == null)
            {
                return HttpNotFound();
            }
            return View(jop);
        }

        // POST: Jops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job jop = db.Jops.Find(id);
            db.Jops.Remove(jop);
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
