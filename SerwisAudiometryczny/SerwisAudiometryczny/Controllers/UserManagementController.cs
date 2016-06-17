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
using System.IO;
using System.Text;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obługująca zarządzanie użytkownikami. Dziedziczy po Controller.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class UserManagementController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        /// <summary>
        /// Zarządca użytkowników
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationDbContext DbContext { get; set; }

        public UserManagementController()
        {
            DbContext = new ApplicationDbContext();
        }

        public UserManagementController(ApplicationUserManager userManager, ApplicationDbContext dbContext)
        {
            UserManager = userManager;
            DbContext = dbContext;
        }

        /// <summary>
        /// Metoda wyświetlająca spis wszystkich użytkowników.
        /// </summary>
        public ActionResult Index()
        {
            return View(DbContext.Users.ToList());
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
                UserManager.UserValidator = new UserValidator<ApplicationUser, int>(UserManager) { AllowOnlyAlphanumericUserNames = false };
                if (UserManager.FindByEmail(model.Email) != null)
                {
                    model.Password = String.Empty;
                    ModelState.AddModelError("Error", "Podany email istnieje w bazie!");
                    return View(model);
                }
                IdentityResult userResult = await UserManager.CreateAsync(user, model.Password);
                DbContext.SaveChanges();

                List<AppRoles> roles = new List<AppRoles>();
                if (model.Administrator)
                    roles.Add(AppRoles.Administrator);
                if (model.Patient)
                    roles.Add(AppRoles.Patient);
                if (model.Researcher)
                    roles.Add(AppRoles.Researcher);
                if (model.User)
                    roles.Add(AppRoles.User);
                UserManager.AddToRoles(user.Id, roles.Select(role => role.ToString()).ToArray());

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
            ApplicationUser currentUser = DbContext.Users.FirstOrDefault(x => x.Id == myId);
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserEditModelView model = new UserEditModelView() { Id = myId, Name = currentUser.Name, Address = currentUser.Address, Email = currentUser.Email, PhoneNumber = currentUser.PhoneNumber, SensitiveDataIds = currentUser.SensitiveDataAccessStorage?.Replace(';','\n') };
            ViewBag.UserName = currentUser.UserName;
            return View(model);
        }

        /// <summary>
        /// Metoda edytująca użytkownika.
        /// </summary>
        /// <param name="model">Model użytkownika z widoku</param>
        [HttpPost]
        public ActionResult EditUser(UserEditModelView model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser currentUser = DbContext.Users.FirstOrDefault(x => x.Id == model.Id);
                if (currentUser == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                currentUser.Address = model.Address;
                currentUser.Email = model.Email;
                currentUser.Name = model.Name;
                currentUser.PhoneNumber = model.PhoneNumber;

                if (model.Password != null && model.Password != String.Empty)
                {
                    UserManager.RemovePassword(model.Id);
                    UserManager.AddPassword(model.Id, model.Password);
                }

                if (model.SensitiveDataIds != null && model.SensitiveDataIds != String.Empty)
                {
                    using (StringReader reader = new StringReader(model.SensitiveDataIds))
                    {
                        List<int> ids = new List<int>();
                        while (reader.Peek() > -1)
                        {
                            int nextInt;
                            if (int.TryParse(reader.ReadLine(), out nextInt))
                            {
                                ids.Add(nextInt);
                            }
                            else
                            {
                                return View();
                            }
                        }
                        currentUser.SensitiveDataAccessIds = ids.ToArray();
                    }
                }

                DbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }
        /// <summary>
        /// Metoda edytująca role użytkownika.
        /// </summary>
        /// <param name="form">Zawartość formularza z widoku</param>
        public ActionResult EditRole(FormCollection form)
        {
            int id = int.Parse(form["Id"]);
            string submit = form["Submit"];
            bool administrator = FormParser(form["Administrator"]);
            bool patient = FormParser(form["Pacjent"]);
            bool researcher = FormParser(form["Badacz"]);
            bool user = FormParser(form["Edytor"]);

            List<AppRoles> userRoles = new List<AppRoles>(4);
            AppRoles[] appRoles = (AppRoles[])Enum.GetValues(typeof(AppRoles));
            if (administrator) userRoles.Add(AppRoles.Administrator);
            if (patient) userRoles.Add(AppRoles.Patient);
            if (researcher) userRoles.Add(AppRoles.Researcher);
            if (user) userRoles.Add(AppRoles.User);

            var currentUser = UserManager.FindById(id);
            if (currentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            switch (submit)
            {
                case "Save":
                    foreach (AppRoles role in appRoles)
                    {
                        IdentityResult result = null;
                        if (userRoles.Contains(role))
                            result = UserManager.AddToRole(currentUser.Id, role.ToString());
                        else
                            result = UserManager.RemoveFromRole(currentUser.Id, role.ToString());
                    }
                    break;
                case "Deactivate":
                    UserManager.RemoveFromRoles(currentUser.Id, appRoles.Select(role => role.ToString()).ToArray());
                    break;
                case "Edit":
                    return RedirectToAction("EditUser", new { myId = currentUser.Id });
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DbContext.SaveChanges();
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