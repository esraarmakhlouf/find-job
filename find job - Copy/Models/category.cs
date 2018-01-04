using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace find_job.Models
{
    public class category
    {
        [Required]
        public int id { get; set; }
        [Required]
        [Display(Name="نوع الوظيفة")]
        public string categoryName { get; set; }
        [Required]
        [Display(Name="وصف الوظيفة")]
        public string categoryDescription { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }
    }
}