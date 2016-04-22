using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.Controllers
{
    public class BackupController : Controller
    {
        MyBackupAndRestore backupAndRestore = new MyBackupAndRestore();
           
        // GET: Backup
        public ActionResult Index()
        {
            return View();
        }

        // GET: Backup/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Backup/Create
        public ActionResult Create()
        {
            return View();
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
