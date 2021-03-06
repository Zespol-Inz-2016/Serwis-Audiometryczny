﻿using System.Threading.Tasks;
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
    /// Klasa obługująca użytkowników. Dziedziczy po Controller.
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AccountController()
        {
        }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        /// <summary>
        /// Zarządca logowania
        /// </summary>
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
        /// <summary>
        /// Metoda odpowiedzialna za przygotowanie do logowania.
        /// </summary>
        /// <param name="returnUrl">Odnośnik URL</param>
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        /// <summary>
        /// Metoda odpowiedzialna za logowanie.
        /// </summary>
        /// <param name="returnUrl">Odnośnik URL</param>
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
            int id;
            ApplicationUser user;
            SignInStatus result = SignInStatus.Failure;
            if (int.TryParse(model.ID, out id))
            {
                user = UserManager.FindById(id);
                string userName = user.UserName;
                if (user != null)
                {
                    result = await SignInManager.PasswordSignInAsync(userName, model.Password, isPersistent: false/*tu było RememberMe z LoginViewModel*/, shouldLockout: false);
                }
            }
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
                    ModelState.AddModelError("Error", "Nie udało się zalogować, sprawdź dane.");
                    return View(model);
            }
        }
        /// <summary>
        /// Metoda odpowiedzialna za wylogowywanie.
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
        /// Metoda przygotowywująca edycje użytkownika.
        /// </summary>
        public ActionResult Edit()
        {
            ApplicationUser user = UserManager.FindById<ApplicationUser, int>(User.Identity.GetUserId<int>());
            AccountEditViewModel model = new AccountEditViewModel();
            model.Name = user.Decrypted.Name;
            model.Address = user.Decrypted.Address;
            model.Email = user.Decrypted.Email;
            model.PhoneNumber = user.Decrypted.PhoneNumber;
            return View(model);
        }
        /// <summary>
        /// Metoda odpowiedzialna za edycje użytkownika.
        /// </summary>
        /// <param name="model">Model użytkownika z widoku</param>
        [HttpPost]
        public ActionResult Edit(AccountEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.FindById<ApplicationUser, int>(User.Identity.GetUserId<int>());
                user.Decrypted.Name = model.Name;
                user.Decrypted.Address = model.Address;
                user.Decrypted.Email = model.Email;
                user.Decrypted.PhoneNumber = model.PhoneNumber;

                var sth = UserManager.Update(user);
                if (model.Password != null && model.Password != string.Empty)
                    if (model.Password.CompareTo(model.ConfirmPassword) == 0)
                    {
                        UserManager.RemovePassword(User.Identity.GetUserId<int>());
                        UserManager.AddPassword(user.Id, model.Password);
                    }
            }
            return RedirectToAction("Details");
        }
        /// <summary>
        /// Metoda zwracająca szczegóły użytkownika.
        /// </summary>
        public ActionResult Details()
        {
            if (User.Identity.Name == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser model = UserManager.FindById(User.Identity.GetUserId<int>());
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