using SerwisAudiometryczny.ActionFilters;
using SerwisAudiometryczny.Models;
using System.Linq;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obsługująca panel administratora, dziedziczy po Controller.
    /// </summary>
    [IsAdministrator]
    public class AdminController : Controller
    {
        ApplicationDbContext db = ApplicationDbContext.Create();
        
        /// <summary>
        /// Metoda wyświetlająca wszystkich użytkowników.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
    }
}