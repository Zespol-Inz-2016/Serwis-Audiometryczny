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
    public class DictNuisanceModelsController : Controller
    {
        private MyBaseModelDBContext db = new MyBaseModelDBContext();

        // GET: DictNuisanceModels
        public ActionResult Index()
        {
            return View(db.DictNuisanceObjects.ToList());
        }

        // GET: DictNuisanceModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictNuisanceModel dictNuisanceModel = db.DictNuisanceObjects.Find(id);
            if (dictNuisanceModel == null)
            {
                return HttpNotFound();
            }
            return View(dictNuisanceModel);
        }

        // GET: DictNuisanceModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DictNuisanceModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictNuisanceModel dictNuisanceModel)
        {
            if (ModelState.IsValid)
            {
                db.MyBaseObjects.Add(dictNuisanceModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dictNuisanceModel);
        }

        // GET: DictNuisanceModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictNuisanceModel dictNuisanceModel = db.DictNuisanceObjects.Find(id);
            if (dictNuisanceModel == null)
            {
                return HttpNotFound();
            }
            return View(dictNuisanceModel);
        }

        // POST: DictNuisanceModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictNuisanceModel dictNuisanceModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dictNuisanceModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dictNuisanceModel);
        }

        // GET: DictNuisanceModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictNuisanceModel dictNuisanceModel = db.DictNuisanceObjects.Find(id);
            if (dictNuisanceModel == null)
            {
                return HttpNotFound();
            }
            return View(dictNuisanceModel);
        }

        // POST: DictNuisanceModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DictNuisanceModel dictNuisanceModel = db.DictNuisanceObjects.Find(id);
            db.MyBaseObjects.Remove(dictNuisanceModel);
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
