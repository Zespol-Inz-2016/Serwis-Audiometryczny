using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Collections.Generic;
using System.Web.Security;
using System;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obługująca zarządzanie użytkownikami. Dziedziczy po Controller.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class UserManagementController : Controller
    {
        ApplicationDbContext db = ApplicationDbContext.Create();
        //public ApplicationUserManager UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
        /// <summary>
        /// Metoda wyświetlająca spis wszystkich użytkowników.
        /// </summary>
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        /// <summary>
        /// Metoda przygotowywująca do tworzenia użytkowników.
        /// </summary>
        public ActionResult CreateUser()
        {
            return View();
        }

        /// <summary>
        /// Metoda tworząca użytkowników.
        /// </summary>
        /// <param name="model">Model użytkownika z widoku</param>
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateModelView model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { Name = model.Name, UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber, Address = model.Address };
                ApplicationDbContext context = new ApplicationDbContext();
                var userManager = new ApplicationUserManager(new CustomUserStore(context));
                userManager.UserValidator = new UserValidator<ApplicationUser, int>(userManager) { AllowOnlyAlphanumericUserNames = false };
                IdentityResult userResult = userManager.Create(user, model.Password);
                db.SaveChanges();

                List<AppRoles> roles = new List<AppRoles>();
                if (model.Administrator)
                    roles.Add(AppRoles.Administrator);
                if (model.Patient)
                    roles.Add(AppRoles.Patient);
                if (model.Researcher)
                    roles.Add(AppRoles.Researcher);
                if (model.User)
                    roles.Add(AppRoles.User);
                userManager.AddToRoles(user.Id, roles.Select(role => role.ToString()).ToArray());

                return RedirectToAction("Index", "UserManagement");
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        //public ActionResult DeactivateUser(int? id)
        //{
        //    return View();
        //}

        //public ActionResult DeactivateUserConfirmed(int id)
        //{
        //    return View();
        //}
        /// <summary>
        /// Metoda edytująca użytkownika.
        /// </summary>
        /// <param name="myId">Id użytkownika</param>
        public ActionResult EditUser(int myId)
        {
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == myId);
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEditModelView model = new UserEditModelView() { Id = myId, Name = currentUser.Name, Address = currentUser.Address, Email = currentUser.DecryptedEmail, PhoneNumber = currentUser.PhoneNumber };
            ViewBag.UserName = currentUser.UserName;
            return View(model);
        }
        /// <summary>
        /// Metoda resetująca hasło.
        /// </summary>
        /// <param name="model">Model użytkownika z widoku</param>
        [HttpPost]
        public ActionResult ResetUserPassword(UserEditModelView model)
        {
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == model.Id);

            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUserManager userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            userManager.RemovePassword(model.Id);

            userManager.AddPassword(model.Id, model.Password);
            return RedirectToAction("Index");
        }
        /// <summary>
        /// Metoda edytująca użytkownika.
        /// </summary>
        /// <param name="model">Model użytkownika z widoku</param>
        [HttpPost]
        public ActionResult EditUser(UserEditModelView model)
        {
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == model.Id);
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            currentUser.Address = model.Address;
            currentUser.Email = model.Email;
            currentUser.Name = model.Name;
            currentUser.PhoneNumber = model.PhoneNumber;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        /// <summary>
        /// Metoda edytująca role użytkownika.
        /// </summary>
        /// <param name="form">Zawartość formularza z widoku</param>
        public ActionResult EditRole(FormCollection form)
        {
            string name = form["Name"];
            string submit = form["Submit"];
            List<AppRoles> userRoles = new List<AppRoles>(4);
            bool administrator = FormParser(form["Administrator"]);
            bool patient = FormParser(form["Pacjent"]);
            bool researcher = FormParser(form["Badacz"]);
            bool user = FormParser(form["Edytor"]);
            if (administrator) userRoles.Add(AppRoles.Administrator);
            if (patient) userRoles.Add(AppRoles.Patient);
            if (researcher) userRoles.Add(AppRoles.Researcher);
            if (user) userRoles.Add(AppRoles.User);
            var userManager = new ApplicationUserManager(new CustomUserStore(db));
            var currentUser = userManager.FindByName(name);
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppRoles[] appRoles = (AppRoles[])Enum.GetValues(typeof(AppRoles));
            switch (submit)
            {
                case "Save":
                    foreach (AppRoles role in appRoles)
                    {
                        IdentityResult result = null;
                        if (userRoles.Contains(role))
                            result = userManager.AddToRole(currentUser.Id, role.ToString());
                        else
                            result = userManager.RemoveFromRole(currentUser.Id, role.ToString());
                    }
                    break;
                case "Deactivate":
                    userManager.RemoveFromRoles(currentUser.Id, appRoles.Select(role => role.ToString()).ToArray());
                    break;
                case "Edit":
                    return RedirectToAction("EditUser", new { myId = currentUser.Id });
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private bool FormParser(string value)
        {
            if (value == "on")
            {
                return true;
            }
            return false;
        }
    }
}