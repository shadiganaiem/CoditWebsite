using Codit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codit.ViewModel;
namespace Codit.ViewModel
{
    public class UserViewModel
    {
        public User user { get; set; }
        public Employee employee { get; set; }

        public Roles role { get; set; }
        public List<Roles> roles { get; set; }

        public List<Client> clients { get; set; }

        public List<Project> projects { get; set; }

        public List<Report> reports { get; set; }

        public List<Report> myReports { get; set; }

        public List<User> users { get; set; }

        public ExportViewModel toExport { get; set; }
    }
}