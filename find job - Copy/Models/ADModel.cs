using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace find_job.Models
{
    public class ADModel
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="اسم الاعلان")]
        public string adAddress { get; set; }
        [Required]
        [Display(Name = "URL")]
        public string adURL { get; set; }
        [Required]
        [Display(Name = "صورة الاعلان")]
        public string adImage { get; set; }
        [Required]
        [Display(Name = "نبذه عنه")]
        public string adContent { get; set; }
        public bool AdminAccept { get; set; }

        public string userid { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}