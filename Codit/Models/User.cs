using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Codit.Models
{
    public class User
    {
        
        public int UserID { get; set; }
        
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}