using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Codit.Models
{
    public class Project
    {
        
        public int ProjectID { get; set; }

        [Required]
        public string ProjectName { get; set; }
        [Required]
        public int ClientID { get; set; }
        [Required]
        public double HoursLimit { get; set; }
        [Required]
        public double SumHours { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public string Status { get; set; }
    }
}