using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Codit.dal;
using Codit.Models;
using Codit.ViewModel;

namespace Codit.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private UserDal udal = new UserDal();
        private EmployeeDal empdal =  new EmployeeDal();
        public ActionResult Index()
        {
            return View("HomePage");
        }

        public ActionResult LoginAction(User user)
        {
            
            List<User> users = (from x in udal.userLst where x.Username.Equals(user.Username)&&x.Password.Equals(user.Password) select x).ToList<User>();
            if (users.Count() == 0)
            {
                TempData["status"] = "שם משתמש או סיסמה שגויים ";

                return RedirectToAction("Index", "Home");
            }

            Session["username"] = user.Username;
            return RedirectToAction("index", "User");
            
            
        }
    }
}