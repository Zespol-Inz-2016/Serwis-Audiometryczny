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
                var user = new ApplicationUser { Name = model.Name, UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber};
                ApplicationDbContext context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser, int>(new CustomUserStore(context));
                userManager.UserValidator = new UserValidator<ApplicationUser, int>(userManager) { AllowOnlyAlphanumericUserNames = false };
                IdentityResult userResult = userManager.Create(user, model.Password);
                db.SaveChanges();

                List<string> roles = new List<string>();
                if (model.Administrator)
                    roles.Add("Administrator");
                if (model.Patient)
                    roles.Add("Patient");
                if (model.Researcher)
                    roles.Add("Researcher");
                if (model.User)
                    roles.Add("User");
                userManager.AddToRoles(user.Id, roles.ToArray<string>());

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
            UserEditModelView model = new UserEditModelView() {Id = myId, Name = currentUser.Name, Address = currentUser.Address, Email = currentUser.DecryptedEmail, PhoneNumber = currentUser.PhoneNumber };
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
            bool administrator = FormParser(form["Administrator"]);
            bool patient = FormParser(form["Pacjent"]);
            bool researcher = FormParser(form["Badacz"]);
            bool user = FormParser(form["Edytor"]);        
            var UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(new ApplicationDbContext()));
            var CurrentUser = UserManager.FindByName(name);
            if (CurrentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            switch (submit)
            {
                case "Save":
                    List<string> roles = new List<string>();
                    if (administrator)
                        roles.Add("Administrator");
                    if (patient)
                        roles.Add("Patient");
                    if (researcher)
                        roles.Add("Researcher");
                    if (user)
                        roles.Add("User");
                    UserManager.AddToRoles(CurrentUser.Id, roles.ToArray<string>());
                    break;
                case "Deactivate":
                    UserManager.RemoveFromRoles(CurrentUser.Id, "Administrator", "Patient", "Researcher", "User");
                    break;
                case "Edit":
                    return RedirectToAction("EditUser", new { myId = CurrentUser.Id });
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