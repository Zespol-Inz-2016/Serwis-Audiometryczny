using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.ActionFilters;

namespace SerwisAudiometryczny
{
    /// <summary>
    /// Konfiguracja filtrów
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// Rejestruje globalne filtry
        /// </summary>
        /// <param name="filters">Kolekcja filtrów</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new LogActionFilterAttribute());
        }
    }
}
