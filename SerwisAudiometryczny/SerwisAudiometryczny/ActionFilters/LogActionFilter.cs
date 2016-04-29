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
            ///zapisujemy kiedy zacze�a si� dana czynno��
            start_time = DateTime.Now;            
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            RouteData route_data = filterContext.RouteData;     ///pobieramy dane z wykonanej czynno�ci     
            LogModel log = new LogModel();
            if (filterContext.RouteData.DataTokens.ContainsKey("username"))
                log.IdUser = (string)route_data.Values["username"];   ///zapisujemy kto zrobi�
            else
                log.IdUser = "anonymous";
            log.Controller = (string)route_data.Values["controller"];    ///zapisujemy gdzie to wykona� (na jakiej stronie)
            log.Action = (string)route_data.Values["action"];    ///zapisujemy co zrobi�
            log.Time = start_time;  ///zapisujemy o kt�rej zrobi�

            Log("C:\\logfile.txt", log.ToString() ); //sciezka do ustalenia, powinno sie zapisywac do bazy danych !!
        }
        public void Log(string path, string message)
        {
            /*
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write); ///tworzymy strumie� do log�w, b�dziemy tylko dopisywa� kolejne logi
            StreamWriter sw = new StreamWriter(fs); ///tworzymy strumie� zapisu 
            sw.WriteLine(message);  ///zapisujemy logi do plik�w
            sw.Flush(); ///czy�cimy bufor strumienia z danych

            ///zamykamy strumienie
            sw.Close(); 
            fs.Close(); 
            */
        }

    }
}