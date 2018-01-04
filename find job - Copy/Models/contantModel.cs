using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace find_job.Models
{
    public class contantModel
    {
        [Required]
        [Display(Name = "الاسم")]
        public string name { get; set; }

        [Required]
        [Display(Name = "البريد")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "الموضوع")]
        public string subject { get; set; }

        [Required]
        [Display(Name = "الرسالة")]
        public string message { get; set; }
    }
}