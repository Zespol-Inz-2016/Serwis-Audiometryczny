using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using System.IO;
using SerwisAudiometryczny.ActionFilters;

namespace SerwisAudiometryczny.Controllers
{ 
    /// <summary>
    /// Kontroler służący do wykonywania kopii zapasowej bazy danych
    /// oraz jej przywracania. Dziedziczy po Controller.
    /// </summary>
    [IsAdministrator]
    public class BackupController : Controller
    {
        DatabaseBackuper databaseBackuper = new DatabaseBackuper();
        
        /// <summary>
        /// Widok strony służącej do przywracania bazy danych.
        /// </summary>
        /// <returns></returns>
        public ActionResult Import()
        {
            return View();
        }

        /// <summary>
        /// Przywraca bazę danych z przesłanej kopii zapasowej.
        /// </summary>
        /// <param name="backup">Kopia zapasowa bazy danych</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Import(HttpPostedFileBase backup)
        {
            if (backup != null)
            {
                databaseBackuper.Restore(backup.InputStream);
                ViewBag.Message = "Pomyślnie przywrócono bazę danych!";
            }
            else
                ViewBag.Message = "Nie przesłano pliku!";

            return View();
        }

        /// <summary>
        /// Widok strony służącej do wykonania kopii zapasowej bazy danych.
        /// </summary>
        /// <returns></returns>
        public ActionResult Export()
        {
            return View();
        }

        /// <summary>
        /// Wysyła administatorowi wykonaną kopię zapasową bazy danych.
        /// </summary>
        /// <returns>Archiwum (.zip) ze wszystkimi plikami XML.</returns>
        public FileResult SendFile()
        {
            Stream stream = databaseBackuper.Backup();
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath("~/App_Data/backup.zip"));
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_SerwisAudiometryczny.zip";
            FileStreamResult fileStreamResult = new FileStreamResult(stream, contentType) { FileDownloadName = fileName };
   
            return fileStreamResult;
        }
    }
}
