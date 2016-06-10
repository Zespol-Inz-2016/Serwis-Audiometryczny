using SerwisAudiometryczny.ActionFilters;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obsługująca stronę startową, dziedziczy po Controller.
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        /// Metoda wyświetlająca stronę startową.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Metoda wyświetlająca informacje o aplikacji.
        /// </summary>
        /// <returns></returns>
        [IsAdministrator]
        public ActionResult About()
        {
            return View();
        }
    }
}