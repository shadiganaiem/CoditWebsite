using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codit.Models;
namespace Codit.ViewModel
{
    public class EmpLstViewModel
    {

        public List<User> users { get; set; }
        public List<Employee> employees { get; set; }
        public UserViewModel uvm { get; set; }
    }
}