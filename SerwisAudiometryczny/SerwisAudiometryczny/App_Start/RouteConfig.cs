using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SerwisAudiometryczny
{
    /// <summary>
    /// Konfiguracja trasowania
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// Rejestracja tras
        /// </summary>
        /// <param name="routes">Kolekcja tras</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
