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

namespace find_job.Controllers
{
    public class ADModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ADModels
        public ActionResult Index()
        {
            var aDModels = db.ADModels.Include(a => a.user);
            return View(aDModels.ToList());
        }

        // GET: ADModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADModel aDModel = db.ADModels.Find(id);
            if (aDModel == null)
            {
                return HttpNotFound();
            }
            return View(aDModel);
        }

        // GET: ADModels/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.Users, "Id", "UserType");
            return View();
        }

        // POST: ADModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ADModel aDModel , HttpPostedFileBase Upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), Upload.FileName);
                Upload.SaveAs(path);
                aDModel.adImage = Upload.FileName;
                db.ADModels.Add(aDModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.Users, "Id", "UserType", aDModel.userid);
            return View(aDModel);
        }

        // GET: ADModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADModel aDModel = db.ADModels.Find(id);
            if (aDModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.Users, "Id", "UserType", aDModel.userid);
            return View(aDModel);
        }

        // POST: ADModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,adAddress,adURL,adImage,adContent,AdminAccept,userid")] ADModel aDModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.Users, "Id", "UserType", aDModel.userid);
            return View(aDModel);
        }

        // GET: ADModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADModel aDModel = db.ADModels.Find(id);
            if (aDModel == null)
            {
                return HttpNotFound();
            }
            return View(aDModel);
        }

        // POST: ADModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ADModel aDModel = db.ADModels.Find(id);
            db.ADModels.Remove(aDModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult show()
        {
            var ad = db.ADModels.Where(a => a.AdminAccept == true);
            return View(ad.ToList());
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
