using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace find_job.Models
{
    public class ApplyForJob
    {
        public int id { get; set; }
        public string message { get; set; }
        public DateTime applyDate { get; set; }
        public int jobId { get; set; }
        public string userId { get; set; }
        [Display(Name ="السيرة الذتية")]
        public string userCV { get; set; }

        public virtual Job job { get; set; }
        public virtual ApplicationUser user { get; set; }
        
    }
}