using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace find_job.Models
{
    public class RolaViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name ="نوع الصلاحية")]
        public string Name { get; set; }
    }
}