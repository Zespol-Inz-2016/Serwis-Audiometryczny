using SerwisAudiometryczny.ActionFilters;
using SerwisAudiometryczny.Models;
using System.Linq;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obsługująca panel administratora. Dziedziczy po Controller.
    /// </summary>
    [IsAdministrator]
    public class AdminController : Controller
    {       
        /// <summary>
        /// Metoda wyświetlająca panel administratora.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}