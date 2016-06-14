using SerwisAudiometryczny.ActionFilters;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obsługująca stronę startową. Dziedziczy po Controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Metoda wyświetlająca stronę startową.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            var user = User.Identity.GetUserId<int>();
            int currentUserId = User.Identity.GetUserId<int>();
            ApplicationUser currentUser = ApplicationDbContext.Create().Users.FirstOrDefault(x => x.Id == currentUserId);

            if (currentUser != null && currentUser.Administrator)
                return View("~/Views/Admin/Index.cshtml");

            return View();
        }

        /// <summary>
        /// Metoda wyświetlająca informacje o aplikacji.
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }
    }
}