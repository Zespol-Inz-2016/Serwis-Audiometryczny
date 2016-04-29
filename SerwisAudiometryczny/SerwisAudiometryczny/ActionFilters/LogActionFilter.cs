using System;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using System.IO;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.ActionFilters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        protected DateTime start_time;

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            ///zapisujemy kiedy zacze³a siê dana czynnoœæ
            start_time = DateTime.Now;            
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RouteData route_data = filterContext.RouteData;     ///pobieramy dane z wykonanej czynnoœci     
            LogModel log = new LogModel();
            if (filterContext.RouteData.DataTokens.ContainsKey("username"))
                log.IdUser = (string)route_data.Values["username"];   ///zapisujemy kto zrobi³
            else
                log.IdUser = "anonymous";
            log.Controller = (string)route_data.Values["controller"];    ///zapisujemy gdzie to wykona³ (na jakiej stronie)
            log.Action = (string)route_data.Values["action"];    ///zapisujemy co zrobi³
            log.Time = start_time;  ///zapisujemy o której zrobi³

            Log("C:\\logfile.txt", log.ToString() ); //sciezka do ustalenia, powinno sie zapisywac do bazy danych !!
        }
        public void Log(string path, string message)
        {
            /*
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write); ///tworzymy strumieñ do logów, bêdziemy tylko dopisywaæ kolejne logi
            StreamWriter sw = new StreamWriter(fs); ///tworzymy strumieñ zapisu 
            sw.WriteLine(message);  ///zapisujemy logi do plików
            sw.Flush(); ///czyœcimy bufor strumienia z danych

            ///zamykamy strumienie
            sw.Close(); 
            fs.Close(); 
            */
        }

    }
}