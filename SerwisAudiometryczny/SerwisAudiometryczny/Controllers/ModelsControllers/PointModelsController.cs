using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.Controllers.ModelsControllers
{
    public class PointModelsController : Controller
    {
        private MyBaseModelDBContext db = new MyBaseModelDBContext();

        // GET: PointModels
        public ActionResult Index()
        {
            return View(db.PointModels.ToList());
        }

        // GET: PointModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointModel pointModel = db.PointModels.Find(id);
            if (pointModel == null)
            {
                return HttpNotFound();
            }
            return View(pointModel);
        }

        // GET: PointModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PointModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,AudiogramID,Frequency,HearingThresholdLevel,CreationDate,Type")] PointModel pointModel)
        {
            if (ModelState.IsValid)
            {
                db.PointModels.Add(pointModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pointModel);
        }

        // GET: PointModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointModel pointModel = db.PointModels.Find(id);
            if (pointModel == null)
            {
                return HttpNotFound();
            }
            return View(pointModel);
        }

        // POST: PointModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,AudiogramID,Frequency,HearingThresholdLevel,CreationDate,Type")] PointModel pointModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pointModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pointModel);
        }

        // GET: PointModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointModel pointModel = db.PointModels.Find(id);
            if (pointModel == null)
            {
                return HttpNotFound();
            }
            return View(pointModel);
        }

        // POST: PointModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PointModel pointModel = db.PointModels.Find(id);
            db.PointModels.Remove(pointModel);
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
