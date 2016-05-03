using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Controllers;

namespace SerwisAudiometryczny.ActionFilters
{
    /// <summary>
    /// Filter akcji
    /// </summary>
    public class LogActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// zmienna przechowywuj¹ca czas rozpoczêcia akcji
        /// </summary>
        protected DateTime start_time;

        /// <summary>
        /// Kiedy zaczêta akcjê zapisujemy o której siê zacze³a
        /// </summary>
        /// <param name="filterContext">Akcja która siê rozpoczê³a</param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            start_time = DateTime.Now;
        }

        /// <summary>
        /// Kiedy wykonano akcjê zapisujemy j¹ do logów
        /// </summary>
        /// <param name="filterContext">Akcja która siê zakoñczy³a</param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RouteData route_data = filterContext.RouteData;
            LogModel log = new LogModel();

            if (filterContext.RouteData.DataTokens.ContainsKey("iduser"))
                log.IdUser = (int)route_data.Values["iduser"];
            else
                log.IdUser = -1;
            log.Controller = (string)route_data.Values["controller"];
            log.Action = (string)route_data.Values["action"];
            log.Time = start_time;

            Log(log);
        }

        /// <summary>
        /// Metoda log wpisuje dany log do bazy
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