using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;

namespace SerwisAudiometryczny.ActionFilters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        protected DateTime start_time;

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            start_time = DateTime.Now;
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RouteData route_data = filterContext.RouteData;
            TimeSpan duration = (DateTime.Now - start_time);
            string controller = (string)route_data.Values["controller"];
            string action = (string)route_data.Values["action"];
            DateTime created_at = DateTime.Now;
            var message = String.Format("created at: {0}, controller: {1}, action: {2}, duration: {3}", created_at, controller,
                    action, duration);
            Log("logfile.txt", message); //sciezka do ustalenia

        }
        public void Log(string path, string message)
        {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
        }

    }
}