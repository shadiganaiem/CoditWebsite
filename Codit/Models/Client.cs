using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Codit.Models
{
    public class Client
    {
        [Key]
        public int CID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Company { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }

    }
}