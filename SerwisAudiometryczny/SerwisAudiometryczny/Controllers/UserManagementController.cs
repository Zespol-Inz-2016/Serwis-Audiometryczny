using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SerwisAudiometryczny.Controllers
{
    public class UserManagementController : Controller
    {
        //public ApplicationUserManager UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        public ActionResult Index()
        {
            ApplicationDbContext db = ApplicationDbContext.Create();
            return View(db.Users.ToList());
            //return View();
        }
        public ActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateModelView model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, Administrator = model.Administrator,Address = model.Address, User = model.User,Researcher = model.Researcher,Patient = model.Patient };
                ApplicationDbContext context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                IdentityResult userResult = userManager.Create(user, model.Password);
                return RedirectToAction("Index", "UserManagement");
                //}
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        public ActionResult DeactivateUser(int? id)
        {
            return View();
        }

        public ActionResult DeactivateUserConfirmed(int id)
        {
            return View();
        }

        public ActionResult EditUser(int? id)
        {
            return View();
        }

        public ActionResult EditUser(UserEditModelView model)
        {
            return View();
        }
    }
}