using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Codit.Models
{
    public class Report
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        public int UserID { get; set; }
        [Required]
        public int ProjectID { get; set; }
        [Required]
        public double WorkHours { get; set; }
        
        public string Notes { get; set; }
        [Required]
        public DateTime UpdateDate { get; set; }

    }
}