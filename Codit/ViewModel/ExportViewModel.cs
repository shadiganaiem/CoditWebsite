using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codit.Models;
namespace Codit.ViewModel
{
    public class ExportViewModel
    {

        public Project project { get; set; }
        
       public  List<Report> reports { get; set; }

       public Client client { get; set; }


    }
}