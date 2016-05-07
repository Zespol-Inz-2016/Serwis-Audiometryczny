using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using PagedList;

namespace SerwisAudiometryczny.Controllers
{
    public class LogController : Controller
    {
        private ModelsDbContext db = new ModelsDbContext();

        // GET: Log
        public ActionResult Index(int? page, LogSearchViewModel model)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(db.LogModels.OrderBy(i => i.Time).ToPagedList(pageNumber, pageSize));
        }

        //niektóre metody poniżej do późniejszego usunięcia
        // GET: Log/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel logModel = db.LogModels.Find(id);
            if (logModel == null)
            {
                return HttpNotFound();
            }
            return View(logModel);
        }

        // GET: Log/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Log/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,IdUser,Controller,Action,Time")] LogModel logModel)
        {
            if (ModelState.IsValid)
            {
                db.LogModels.Add(logModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(logModel);
        }

        // GET: Log/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel logModel = db.LogModels.Find(id);
            if (logModel == null)
            {
                return HttpNotFound();
            }
            return View(logModel);
        }

        // POST: Log/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,IdUser,Controller,Action,Time")] LogModel logModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(logModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(logModel);
        }

        // GET: Log/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LogModel logModel = db.LogModels.Find(id);
            if (logModel == null)
            {
                return HttpNotFound();
            }
            return View(logModel);
        }

        // POST: Log/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LogModel logModel = db.LogModels.Find(id);
            db.LogModels.Remove(logModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
