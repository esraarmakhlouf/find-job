using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace find_job.Models
{
    public class jobsViewModel
    {
        public string jobTitle { get; set; }
        public IEnumerable<ApplyForJob> item { get; set; }
    }
}