using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Controllers;
using Microsoft.AspNet.Identity;

namespace SerwisAudiometryczny.ActionFilters
{
    /// <summary>
    /// Klasa filtruj�ca, zapisuje akcje u�ytkownik�w do bazy danych
    /// </summary>
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        protected DateTime start_time;

        /// <summary>
        /// Przypisuje czas rozpocz�cia akcji do zmiennej start_time
        /// </summary>
        /// <param name="filterContext">Akcja kt�ra si� rozpocz�a</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            start_time = DateTime.Now;
        }

        /// <summary>
        /// Pobiera ID aktualnego u�ytkownika, kontroler, akcj� i dat� zdarzenia, po czym wywo�uj� metod� zapisuj�c� dany log do bazy
        /// </summary>
        /// <param name="filterContext">Akcja, kt�ra si� zako�czy�a</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RouteData route_data = filterContext.RouteData;
            LogModel log = new LogModel();

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
                log.UserId = filterContext.HttpContext.User.Identity.GetUserId<int>();
            else
                log.UserId = null;
            log.Controller = (string)route_data.Values["controller"];
            log.Action = (string)route_data.Values["action"];
            log.Time = start_time;

            Log(log);
        }

        /// <summary>
        /// Wpisuje dany log do bazy
        /// </summary>
        /// <param name="log">Log do wspisania</param>
        public void Log(LogModel log)
        {
            var db = new ModelsDbContext();
            db.LogModels.Add(log);
            db.SaveChanges();
        }
    }
}