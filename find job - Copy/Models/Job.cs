using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace find_job.Models
{
    public class Job
    {
        public int id { get; set; }
        [Display(Name ="اسم الوظيفة")]
        public string jopTitle { get; set; }
        [Display(Name = "وصف الوظيفة")]
        [AllowHtml]
        public string jopContent { get; set; }
        [Display(Name = "صورة الوظيفة")]
        public string jopImage{ get; set; }
        [Display(Name ="نوع الوظيفة")]
        public int CategoryId { get; set; }
        [Display(Name ="العنوان")]
        public string jobAddress { get; set; }
        [Display(Name = "المرتب")]
        public double jobSalary { get; set; }
        [Display(Name = "تاريخ النشر")]
        public DateTime jobDate  { get; set; }

        public string userId { get; set; }

        public virtual category Category { get; set; }
        public virtual ApplicationUser  user { get; set; }
    }
}