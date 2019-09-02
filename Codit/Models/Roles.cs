using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Codit.Models
{
    public class Roles
    {
        [Key]
        [Required]
        public int RoleID { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Report { get; set; }
        [Required]
        public string Export { get; set; }
        [Required]
        public string ManageEmployees { get; set; }
        [Required]
        public string ManageProjects { get; set; }
        [Required]
        public string ManageRoles { get; set; }
        
    }
}