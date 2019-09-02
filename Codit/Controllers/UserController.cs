using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Codit.Models;
using Codit.dal;
using Codit.ViewModel;
namespace Codit.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        private UserDal udal = new UserDal();
        private EmployeeDal empdal = new EmployeeDal();
        private RoleDal roldal = new RoleDal();
        private Employee newEmp = new Employee();
        private ClientDal cdal = new ClientDal();
        private ProjectDal pdal = new ProjectDal();
        private ReportDal rdal = new ReportDal();
        public ActionResult Index()
        {
            UserViewModel uvm = getUserViewModel();
            return View("UserMainPage",uvm);
        }

        public ActionResult UsersManagement()
        {
            List<User> usrs = (from x in udal.userLst select x).ToList<User>();
            List<Employee> emps = (from x in empdal.empLst select x).ToList<Employee>();
            EmpLstViewModel empvm = new EmpLstViewModel
            {
                employees = emps,
                users = usrs,
                uvm = getUserViewModel()
            };


            return View("UsersManagementPage",empvm); 
        }

        public ActionResult ExportPage()
        {
            UserViewModel uvm = getUserViewModel();
            uvm.toExport = getExportViewModel(Request.Form["row"]);
            return View(uvm);
        }
        public ActionResult ProjectsManagement()
        {


            return View("ProjectsManagementPage",getUserViewModel());
        }

        public ActionResult AllReports()
        {
            return View(getUserViewModel());
        }

        public ActionResult searchProject()
        {
            string value = Request.Form["value"];
            if (value.Equals("כל הפרויקטים"))
            {
                return View("ProjectsManagementPage", getUserViewModel());
            }
            
            string[] split = value.Split(new Char[] { ' ' });
            string client = split[0];
            int id = Int32.Parse(client);

            UserViewModel uvm = getUserViewModel();
            List<Project> projects = (from x in pdal.projectList where x.ClientID == id select x).ToList<Project>();
            uvm.projects = projects;
            return View("ProjectsManagementPage", uvm);
        }
            
        public ActionResult searchReports()
        {
            string value = Request.Form["pid"];
            if(value.Equals("כל הדוחות"))
            {
                return View("AllReports", getUserViewModel());
            }
            string[] split = value.Split(new char[] { ' ' });
            string project = split[0];
            int id = Int32.Parse(project);

            UserViewModel uvm = getUserViewModel();
            List<Report> reports = (from x in rdal.reportslist where x.ProjectID == id select x).ToList<Report>();
            uvm.reports = reports;
            return View("AllReports", uvm);
        }

        public ActionResult Search()
        {
            List<Client> clients;
            string type = Request.Form["stype"];
            string toSearch = Request.Form["CSearch"];
            if (type.Equals("שם פרטי"))
                clients = (from x in cdal.clientsList where x.FirstName.Equals(toSearch) select x).ToList<Client>();
            else if (type.Equals("שם משפחה"))
                clients = (from x in cdal.clientsList where x.LastName.Equals(toSearch) select x).ToList<Client>();
            else if (type.Equals("ת\"ז"))
                clients = (from x in cdal.clientsList where x.ID.Equals(toSearch) select x).ToList<Client>();
            else if (type.Equals("דוא\"ל"))
                clients = (from x in cdal.clientsList where x.Email.Equals(toSearch) select x).ToList<Client>();
            else if (type.Equals("טלפון"))
                clients = (from x in cdal.clientsList where x.Phone.Equals(toSearch) select x).ToList<Client>();
            else if (type.Equals("חברה"))
                clients = (from x in cdal.clientsList where x.Company.Equals(toSearch) select x).ToList<Client>();
            else if (type.Equals("עיר"))
                clients = (from x in cdal.clientsList where x.City.Equals(toSearch) select x).ToList<Client>();
            else if (type.Equals("כתובת"))
                clients = (from x in cdal.clientsList where x.Address.Equals(toSearch) select x).ToList<Client>();
            else
                clients = (from x in cdal.clientsList select x).ToList<Client>();

            UserViewModel uvm = getUserViewModel();
            uvm.clients = clients;
            return View("UserMainPage", uvm);
        }
        
        public ExportViewModel getExportViewModel(string row)
        {
            int i = Int32.Parse(row);
            Project project = pdal.projectList.Where(x => x.ProjectID == i).First();
            List<Report> reports = (from x in rdal.reportslist where x.ProjectID == i select x ).ToList<Report>();
            i = project.ClientID;
            Client client = cdal.clientsList.Where(x => x.CID == i).First();
            ExportViewModel toExport = new ExportViewModel
            {
                project = project,
                reports = reports,
                client = client
            };

            return toExport;
        }
        public UserViewModel getUserViewModel()
        {
            string usr = Session["username"].ToString();
            User user = udal.userLst.Where(x => x.Username.Equals(usr)).First();
            List<User> users = (from x in udal.userLst select x).ToList<User>();
            int id = user.UserID;
            List<Employee> emps = (from x in empdal.empLst where x.UserID == id select x).ToList<Employee>();
            int roleid = emps[0].RoleID;
            List<Roles> roles = (from x in roldal.rolesLst select x).ToList<Roles>();
            List<Roles> myrole = (from x in roldal.rolesLst where x.RoleID == roleid select x).ToList<Roles>();
            List<Client> clients = (from x in cdal.clientsList select x).ToList<Client>();
            List<Project> projects = (from x in pdal.projectList select x).ToList<Project>();
            List<Report> reports = (from x in rdal.reportslist select x).ToList<Report>();
       
            List<Report> myReports = (from x in rdal.reportslist where x.UserID == id  select x).ToList<Report>();

            UserViewModel uvm = new UserViewModel
            {
                employee = emps[0],
                user = user,
                roles = roles,
                clients = clients,
                projects = projects,
                reports = reports,
                myReports = myReports,
                role = myrole[0],
                users = users
                
            };
            return uvm;
        }

        public ActionResult test()
        {
            return View();
        }

        public ActionResult newEmployeePage()
        {

            return View(getUserViewModel());
        }
        public ActionResult newProjectPage()
        {
            return View(getUserViewModel());
        }

        public ActionResult ReportsManagementPage()
        {
            return View(getUserViewModel());
        }
        public ActionResult newEmployee()
        {
            newEmp.FirstName = Request.Form["eFName"];
            newEmp.LastName = Request.Form["eLName"];
            newEmp.Phone = Request.Form["ePhone"];
            newEmp.Email = Request.Form["eEmail"];
            newEmp.ID = Request.Form["eID"];
            newEmp.StartDate = DateTime.Now;
            string equalRole = Request.Form["eRole"];
            List<Roles> rol = (from x in roldal.rolesLst where x.Role.Equals(equalRole) select x).ToList<Roles>();
            newEmp.RoleID = rol[0].RoleID;

            Session["newEmp"] = newEmp;
            return RedirectToAction("newUserPage","User");
        }

        public ActionResult newProject(Project project)

        {
            string value = Request.Form["clnt"];
            string[] split = value.Split(new Char[] { ' ' });
            string client = split[0];
            project.ClientID = Int32.Parse(client);
            pdal.projectList.Add(project);
            pdal.SaveChanges();
            return View("ProjectsManagementPage", getUserViewModel());
        }

        public ActionResult newReport(Report report)
        {
            string value = Request.Form["pid"];
            string[] split = value.Split(new Char[] { ' ' });
            string projectID = split[0];
            int id = Int32.Parse(projectID);
            report.ProjectID = id;
            report.UserID = getUserViewModel().user.UserID;
            rdal.reportslist.Add(report);
            rdal.SaveChanges();

            Project project = pdal.projectList.Where(x => x.ProjectID == id).First();
            project.SumHours += report.WorkHours;
            pdal.SaveChanges();
            return View("ReportsManagementPage", getUserViewModel());
        }
        public ActionResult newUserPage()
        {
            return View(getUserViewModel());
        }

        public ActionResult newUser()
        {
            User usr = new User()
            {
                Username = Request.Form["nUsername"],
                Password = Request.Form["nPassword"]
            };
            udal.userLst.Add(usr);
            udal.SaveChanges();
            List<User> usrs = (from x in udal.userLst where x.Username.Equals(usr.Username) select x).ToList<User>();
            newEmp = (Employee)Session["newEmp"];
            newEmp.UserID = usrs[0].UserID;
            empdal.empLst.Add(newEmp);
            empdal.SaveChanges();

            return RedirectToAction("UsersManagement","User");
        }

        public ActionResult UsersPage()
        {
            UserViewModel uvm = getUserViewModel();
            EmpLstViewModel empvm = new EmpLstViewModel
            {
                employees = (from x in empdal.empLst select x).ToList<Employee>(),
                users = (from x in udal.userLst select x).ToList<User>(),
                uvm = uvm
            };
            return View(empvm);
        }

        public ActionResult deleteUser()
        {
            string value = Request.Form["row"];
            int id = Int32.Parse(value);
            empdal.empLst.RemoveRange(empdal.empLst.Where(x => x.UserID == id));
            empdal.SaveChanges();
            udal.userLst.RemoveRange(udal.userLst.Where(x => x.UserID == id));
            udal.SaveChanges();
            return RedirectToAction("UsersManagement","User");
        }

        public ActionResult deleteReport()
        {
            string value = Request.Form["row"];
            int id = Int32.Parse(value);
            rdal.reportslist.RemoveRange(rdal.reportslist.Where(x => x.ReportID == id));
            rdal.SaveChanges();
            return View("ReportsManagementPage", getUserViewModel());
        }
        public ActionResult deleteProject()
        {
            string value = Request.Form["row"];
            int id = Int32.Parse(value);
            pdal.projectList.RemoveRange(pdal.projectList.Where(x => x.ProjectID == id));
            pdal.SaveChanges();
            return RedirectToAction("ProjectsManagement","User");
        }
        [HttpPost]
        [Route("User/deleteClient")]
        public ActionResult deleteClient(string value)
        {
            int id = Int32.Parse(value);
            cdal.clientsList.RemoveRange(cdal.clientsList.Where(x => x.CID == id));
            cdal.SaveChanges();
            return RedirectToAction("index", "User");
        }

        public bool getUser(string username)
        {
            List<User> usrs = (from x in udal.userLst where x.Username.Equals(username) select x).ToList<User>();
            return usrs.Count == 1;
        }



        public JsonResult CheckLogInInformation(string username1)
        {

            System.Threading.Thread.Sleep(2000);
            bool GetIfUserExist = getUser(username1);
            if (GetIfUserExist)
            {
                return Json(1);
            }
            return Json(0);
           

        }

        public JsonResult checkNewID(string nID)
        {
            System.Threading.Thread.Sleep(2000);
            List<Employee> usrs = (from x in empdal.empLst where x.ID.Equals(nID) select x).ToList<Employee>();
            if (nID.Length != 9)
                return Json(3);
            else if (usrs.Count == 0)
                return Json(0);
            else return Json(1);
        }

        public ActionResult newClientPage()
        {
            return View(getUserViewModel());
        }

        public ActionResult newClient(Client client)
        {

            cdal.clientsList.Add(client);
            cdal.SaveChanges();

            return RedirectToAction("index", "User");
        }

        public void updateProjectStatus(string id,string update)
        {
            int i = Int32.Parse(id);
            Project project = pdal.projectList.Where(x => x.ProjectID == i).First();
            project.Status = update;
            pdal.SaveChanges();
        }

        public void updateRole(string id, string role)
        {
            int i = Int32.Parse(id);
            Employee emp = empdal.empLst.Where(x => x.UserID == i).First();
            Roles newRole = roldal.rolesLst.Where(x => x.Role.Equals(role)).First();
            emp.RoleID = newRole.RoleID;
            empdal.SaveChanges();
        }

        public void updatePassword(string id, string pass)
        {
            int i = Int32.Parse(id);
            User usr = udal.userLst.Where(x => x.UserID == i).First();
            usr.Password = pass;
            udal.SaveChanges();
          
        }
    }
}