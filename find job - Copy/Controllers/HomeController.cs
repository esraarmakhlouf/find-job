using find_job.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            return View(db.categories.ToList());
        }
        public ActionResult Details(int id)
        {
            var job = db.Jops.Find(id);
            if(job==null)
            {
                return HttpNotFound();
            }
            Session["jobId"] = id;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Apply(string Message, HttpPostedFileBase Upload)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["jobId"];

            var check = db.ApplyForJobs.Where(a => a.jobId == jobId && a.userId == userId).ToList();
            if (check.Count< 1)
            {

                
                var job = new ApplyForJob();
                job.jobId = jobId;
                job.userId = userId;
                job.message = Message;
                job.applyDate = DateTime.Now;
                if (Upload != null)
                {
                    string path = Path.Combine(Server.MapPath("~/Uploads"), Upload.FileName);
                    Upload.SaveAs(path);
                    job.userCV = Upload.FileName;
                }
               
                db.ApplyForJobs.Add(job);
                db.SaveChanges();

                ViewBag.Result= " تم التقدم للوظيفة بنجاح";

            }
            else
            {
                ViewBag.Result= "عذرا قد سبق وان تقدمت لهذ الوظيفة من قبل ";
            }
            return View();
          
        }
        [Authorize]
        public ActionResult getJobsByUser()
        {
            var userId = User.Identity.GetUserId();
            var job = db.ApplyForJobs.Where(a => a.userId == userId).ToList();
            return View(job.ToList());
        }

        [Authorize]
        public ActionResult detailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        [Authorize]
        public ActionResult getJopsByPublisher()
        {
            var userId = User.Identity.GetUserId();
            var job = from app in db.ApplyForJobs
                      join jobs in db.Jops
                      on app.jobId equals jobs.id
                      where jobs.user.Id == userId
                      select app;
            var groubed = from j in job
                          group j by j.job.jopTitle
                        into gr
                          select new jobsViewModel
                          {
                              jobTitle = gr.Key,
                              item = gr
                          };

            return View(groubed.ToList());
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.applyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("getJobsByUser");

            }
            return View(job);
        }

        [Authorize]
        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            try
            {
                // TODO: Add delete logic here
                var myjob = db.ApplyForJobs.Find(job.id);
                db.ApplyForJobs.Remove(myjob);
                db.SaveChanges();
                return RedirectToAction("getJobsByUser");
            }
            catch
            {
                return View(job);
            }
        }
      

       
        public ActionResult search(string searchName)
        {
            var result = db.Jops.Where(a => a.jopTitle.Contains(searchName) ||
            a.jopContent.Contains(searchName)
            || a.Category.categoryName.Contains(searchName)
            || a.Category.categoryDescription.Contains(searchName)).ToList();
            return View(result.ToList());
        }

        [Authorize]
        public ActionResult useres()
        {
            var useres = db.Users.Where(a=>a.UserType!="Admins");
            return View(useres.ToList());
        }


        public ActionResult DeleteUser(string id)
        {
           
            var user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Jops/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Useres");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            

            return View();
        }
        [HttpPost]
        public ActionResult Contact(contantModel contant)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("esraa.makhlof999@gmail.com", "15119956");
            mail.From = new MailAddress(contant.Email);
            mail.To.Add(new MailAddress("esraa.makhlof999@gmail.com"));
            mail.Subject = contant.subject;
            mail.IsBodyHtml = true;
            string body = "اسم المرسل : " + contant.name + "<br>"
                + "بريد المرسل : " + contant.Email + "<br>"
                + "عنوان الرسالة : " + contant.subject + "<br>"
                + "نص الرساله : " + contant.message + "<br>";
            mail.Body = body;
            var smtpclient = new SmtpClient("smtp.gmail.com", 587);
            smtpclient.EnableSsl = true;
            smtpclient.Credentials = loginInfo;
            smtpclient.Send(mail);
            return RedirectToAction("Index");
        }
    }
}