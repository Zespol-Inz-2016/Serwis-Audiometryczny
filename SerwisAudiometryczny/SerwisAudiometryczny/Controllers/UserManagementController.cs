using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.Controllers
{
    public class UserManagementController : Controller
    {
        public ApplicationUserManager UserManager;

        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult CreateUser(UserCreateModelView model)
        {
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