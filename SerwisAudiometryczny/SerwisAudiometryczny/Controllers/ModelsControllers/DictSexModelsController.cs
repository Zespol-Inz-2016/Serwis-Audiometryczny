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
    public class DictSexModelsController : Controller
    {
        private MyBaseModelDBContext db = new MyBaseModelDBContext();

        // GET: DictSexModels
        public ActionResult Index()
        {
            return View(db.DictSexObjects.ToList());
        }

        // GET: DictSexModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictSexModel dictSexModel = db.DictSexObjects.Find(id);
            if (dictSexModel == null)
            {
                return HttpNotFound();
            }
            return View(dictSexModel);
        }

        // GET: DictSexModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DictSexModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictSexModel dictSexModel)
        {
            if (ModelState.IsValid)
            {
                db.DictSexObjects.Add(dictSexModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dictSexModel);
        }

        // GET: DictSexModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictSexModel dictSexModel = db.DictSexObjects.Find(id);
            if (dictSexModel == null)
            {
                return HttpNotFound();
            }
            return View(dictSexModel);
        }

        // POST: DictSexModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CreationDate,Type,Code,Description,Name")] DictSexModel dictSexModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dictSexModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dictSexModel);
        }

        // GET: DictSexModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DictSexModel dictSexModel = db.DictSexObjects.Find(id);
            if (dictSexModel == null)
            {
                return HttpNotFound();
            }
            return View(dictSexModel);
        }

        // POST: DictSexModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DictSexModel dictSexModel = db.DictSexObjects.Find(id);
            db.DictSexObjects.Remove(dictSexModel);
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
