using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SerwisAudiometryczny.Models;
using System.Net;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obługująca użytkowników, dziedziczy po Controller
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }
        /// <summary>
        /// Kontruktor
        /// </summary>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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
        /// <summary>
        /// Metoda odpowiedzialna za przygotowanie do logowania
        /// </summary>
        /// <param name="returnUrl">Odnosnik url</param>
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        /// <summary>
        /// Metoda odpowiedzialna za logowanie
        /// </summary>
        /// <param name="returnUrl">Odnosnik url</param>
        /// <param name="model">Model użytkownika z widoku</param>
        // GET: /Account/Login
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false/*tu było RememberMe z LoginViewModel*/, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        /// <summary>
        /// Metoda odpowiedzialna za wylogowywanie
        /// </summary>
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Metoda przygotowywujaca edycje użytkownika
        /// </summary>
        public ActionResult Edit()
        {
            ApplicationUser user = UserManager.FindById<ApplicationUser, int>(User.Identity.GetUserId<int>());
            AccountEditViewModel model = new AccountEditViewModel();
            model.Name = user.Name;
            model.Address = user.Address;
            model.Email = user.Email;
            model.PhoneNumber = user.PhoneNumber;
            return View(model);
        }
        /// <summary>
        /// Metoda odpowiedzialna za edycje użytkownika
        /// </summary>
        /// <param name="model">Model użytkownika z widoku</param>
        [HttpPost]
        public ActionResult Edit(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById<ApplicationUser, int>(User.Identity.GetUserId<int>());
                user.Name = model.Name;
                user.Address = model.Address;
                user.Email = model.Email;
                user.UserName = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                UserManager.SetEmail<ApplicationUser, int>(User.Identity.GetUserId<int>(), model.Email);
                UserManager.Update(user);
                if (model.Password != null && model.Password != string.Empty)
                    if (model.Password.CompareTo(model.ConfirmPassword) == 0)
                    {
                        UserManager.RemovePassword<ApplicationUser, int>(User.Identity.GetUserId<int>());
                        UserManager.AddPassword(user.Id, model.Password);
                    }
                UserManager.Update<ApplicationUser, int>(user);
            }
            return View(model);
        }
        /// <summary>
        /// Metoda zwracająca szczegóły użytkownika
        /// </summary>
        public ActionResult Details()
        {
            if (User.Identity.Name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser model = UserManager.FindById<ApplicationUser, int>(User.Identity.GetUserId<int>());
            return View(model);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

    }
}