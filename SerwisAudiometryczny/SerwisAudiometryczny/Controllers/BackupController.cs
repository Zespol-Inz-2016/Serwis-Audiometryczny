using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using System.IO;

namespace SerwisAudiometryczny.Controllers
{
    public class BackupController : Controller
    {
        DatabaseBackuper databaseBackuper = new DatabaseBackuper();

        public ActionResult Import()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase backup)
        {
            databaseBackuper.Restore(backup.InputStream);
            ViewBag.Message = "Pomyślnie przywrócono bazę danych!";
            return View();
        }

        public ActionResult Export()
        {
            return View();
        }

        public FileResult SendFile()
        {
            Stream stream = databaseBackuper.Backup();
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath("~/App_Data/backup.sql"));
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_SerwisAudiometryczny.sql";
            FileStreamResult fileStreamResult = new FileStreamResult(stream, contentType) { FileDownloadName = fileName };
   
            return fileStreamResult;
        }

        // POST: Backup/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Backup/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Backup/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Backup/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Backup/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
