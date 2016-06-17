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
            if (User != null && User.IsInRole("Administrator"))
                return View("~/Views/Admin/Index.cshtml");
            if (User.Identity.IsAuthenticated)
                return View("~/Views/Home/Start.cshtml");

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