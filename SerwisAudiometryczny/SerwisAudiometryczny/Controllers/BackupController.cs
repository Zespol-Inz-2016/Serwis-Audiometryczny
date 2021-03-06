﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using System.IO;
using SerwisAudiometryczny.ActionFilters;
using SerwisAudiometryczny.Helpers;

namespace SerwisAudiometryczny.Controllers
{ 
    /// <summary>
    /// Kontroler służący do wykonywania kopii zapasowej bazy danych
    /// oraz jej przywracania. Dziedziczy po Controller.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class BackupController : Controller
    {
        private DatabaseBackuper databaseBackuper;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public BackupController()
        {
            databaseBackuper = new DatabaseBackuper();
        }
        
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
                Stream tempBackup = databaseBackuper.Backup("tempBackup");
                try
                {
                    databaseBackuper.Restore(backup.InputStream);             
                }
                catch (Exception)
                {
                    backup.InputStream.Close(); // zamkniecie strumienia po niepowodzeniu   
                    databaseBackuper.Restore(tempBackup);             
                    tempBackup.Close(); 
                    ViewBag.Message = "Nie udało się przywrócić bazy danych. Zostaniesz wylogowany, a baza zostanie przywrócona do poprzedniego stanu";
                    HttpContext.GetOwinContext().Authentication.SignOut();

                    return View();
                }

                tempBackup.Close();
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
            Stream stream = databaseBackuper.Backup("backup");
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath("~/App_Data/backup.zip"));
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_SerwisAudiometryczny.zip";
            FileStreamResult fileStreamResult = new FileStreamResult(stream, contentType) { FileDownloadName = fileName };
   
            return fileStreamResult;
        }
    }
}
