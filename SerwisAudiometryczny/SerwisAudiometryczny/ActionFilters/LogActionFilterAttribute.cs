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
    /// Filter akcji
    /// </summary>
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Zmienna przechowywuj�ca czas rozpocz�cia akcji
        /// </summary>
        protected DateTime start_time;

        /// <summary>
        /// Kiedy zacze�a si� akcja zapisujemy dat� rozpocz�cia
        /// </summary>
        /// <param name="filterContext">Akcja kt�ra si� rozpocz�a</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            start_time = DateTime.Now;
        }

        /// <summary>
        /// Kiedy wykonano akcj� zapisujemy j� do log�w
        /// </summary>
        /// <param name="filterContext">Akcja kt�ra si� zako�czy�a</param>
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