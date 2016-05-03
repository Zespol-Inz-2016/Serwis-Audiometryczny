using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.Controllers
{
    public class FrequencyModelsController : Controller
    {
        private ModelsDbContext db = new ModelsDbContext();

        // GET: FrequencyModels
        public ActionResult Index()
        {
            return View(db.FrequencyModels.ToList());
        }

        // GET: FrequencyModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrequencyModel frequencyModel = db.FrequencyModels.Find(id);
            if (frequencyModel == null)
            {
                return HttpNotFound();
            }
            return View(frequencyModel);
        }

        // GET: FrequencyModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FrequencyModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Frequency")] FrequencyModel frequencyModel)
        {
            if (ModelState.IsValid)
            {
                db.FrequencyModels.Add(frequencyModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(frequencyModel);
        }

        // GET: FrequencyModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrequencyModel frequencyModel = db.FrequencyModels.Find(id);
            if (frequencyModel == null)
            {
                return HttpNotFound();
            }
            return View(frequencyModel);
        }

        // POST: FrequencyModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Frequency")] FrequencyModel frequencyModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(frequencyModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(frequencyModel);
        }

        // GET: FrequencyModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FrequencyModel frequencyModel = db.FrequencyModels.Find(id);
            if (frequencyModel == null)
            {
                return HttpNotFound();
            }
            return View(frequencyModel);
        }

        // POST: FrequencyModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FrequencyModel frequencyModel = db.FrequencyModels.Find(id);
            db.FrequencyModels.Remove(frequencyModel);
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
