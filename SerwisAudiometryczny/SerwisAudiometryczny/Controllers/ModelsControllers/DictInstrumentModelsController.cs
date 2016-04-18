using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.Models.Dicts;

namespace SerwisAudiometryczny.Controllers.ModelsControllers
{
    public class DictInstrumentModelsController : Controller
    {
        private MyBaseModelDBContext db = new MyBaseModelDBContext();

        // GET: DictInstrumentModels
        public ActionResult Index()
        {
            return View(db.DictInstrumentObjects.ToList());
        }

        // GET: DictInstrumentModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictInstrumentModel dictInstrumentModel = db.DictInstrumentObjects.Find(id);
            if (dictInstrumentModel == null)
            {
                return HttpNotFound();
            }
            return View(dictInstrumentModel);
        }

        // GET: DictInstrumentModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DictInstrumentModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictInstrumentModel dictInstrumentModel)
        {
            if (ModelState.IsValid)
            {
                db.MyBaseObjects.Add(dictInstrumentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dictInstrumentModel);
        }

        // GET: DictInstrumentModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictInstrumentModel dictInstrumentModel = db.DictInstrumentObjects.Find(id);
            if (dictInstrumentModel == null)
            {
                return HttpNotFound();
            }
            return View(dictInstrumentModel);
        }

        // POST: DictInstrumentModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictInstrumentModel dictInstrumentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dictInstrumentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dictInstrumentModel);
        }

        // GET: DictInstrumentModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictInstrumentModel dictInstrumentModel = db.DictInstrumentObjects.Find(id);
            if (dictInstrumentModel == null)
            {
                return HttpNotFound();
            }
            return View(dictInstrumentModel);
        }

        // POST: DictInstrumentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DictInstrumentModel dictInstrumentModel = db.DictInstrumentObjects.Find(id);
            db.MyBaseObjects.Remove(dictInstrumentModel);
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
