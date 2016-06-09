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
    /// Klasa filtruj¹ca, zapisuje akcje u¿ytkowników do bazy danych
    /// </summary>
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        protected DateTime start_time;

        /// <summary>
        /// Przypisuje czas rozpoczêcia akcji do zmiennej start_time
        /// </summary>
        /// <param name="filterContext">Akcja która siê rozpoczê³a</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            start_time = DateTime.Now;
        }

        /// <summary>
        /// Pobiera ID aktualnego u¿ytkownika, kontroler, akcjê i datê zdarzenia, po czym wywo³ujê metodê zapisuj¹c¹ dany log do bazy
        /// </summary>
        /// <param name="filterContext">Akcja, która siê zakoñczy³a</param>
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