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
    public class AudiogramController : Controller
    {
        private ModelsDbContext db = new ModelsDbContext();

        public ActionResult Search()
        {
            return View();
        }

        //:TODO
        public ActionResult Search(int? page, AudiogramSearchViewModel model)
        {
            return View();
        }

        // GET: AudiogramModels
        public ActionResult Index(int? page)
        {
            return View(db.AudiogramModels.ToList());
        }

        // GET: AudiogramModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (audiogramModel == null)
            {
                return HttpNotFound();
            }
            return View(audiogramModel);
        }

        // GET: AudiogramModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AudiogramModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LeftEar,RightEar,Diagnosis,Sex,Nuisance,Age,PercentageHearingLoss,IsMusician,PatientID,EditorID")] AudiogramModel audiogramModel)
        {
            if (ModelState.IsValid)
            {
                db.AudiogramModels.Add(audiogramModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(audiogramModel);
        }

        // GET: AudiogramModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (audiogramModel == null)
            {
                return HttpNotFound();
            }
            return View(audiogramModel);
        }

        // POST: AudiogramModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LeftEar,RightEar,Diagnosis,Sex,Nuisance,Age,PercentageHearingLoss,IsMusician,PatientID,EditorID")] AudiogramModel audiogramModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(audiogramModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(audiogramModel);
        }

        // GET: AudiogramModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (audiogramModel == null)
            {
                return HttpNotFound();
            }
            return View(audiogramModel);
        }

        // POST: AudiogramModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            db.AudiogramModels.Remove(audiogramModel);
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
