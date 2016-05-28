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
    public class InstrumentsController : Controller
    {
        private ModelsDbContext db = new ModelsDbContext();

        public InstrumentsController()
        {
            db = new ModelsDbContext();
        }

        public InstrumentsController(ModelsDbContext dbContext)
        {
            db = dbContext;
        }
        // GET: InstrumentModels
        public ActionResult Index(int? page)
        {
            return View(db.InstrumentModels.ToList());
        }

        // GET: InstrumentModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentModel instrumentModel = db.InstrumentModels.Find(id);
            if (instrumentModel == null)
            {
                return HttpNotFound();
            }
            return View(instrumentModel);
        }

        // GET: InstrumentModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InstrumentModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name")] InstrumentModel instrumentModel)
        {
            if (ModelState.IsValid)
            {
                db.InstrumentModels.Add(instrumentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(instrumentModel);
        }

        // GET: InstrumentModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentModel instrumentModel = db.InstrumentModels.Find(id);
            if (instrumentModel == null)
            {
                return HttpNotFound();
            }
            return View(instrumentModel);
        }

        // POST: InstrumentModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name")] InstrumentModel instrumentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(instrumentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(instrumentModel);
        }

        // GET: InstrumentModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InstrumentModel instrumentModel = db.InstrumentModels.Find(id);
            if (instrumentModel == null)
            {
                return HttpNotFound();
            }
            return View(instrumentModel);
        }

        // POST: InstrumentModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InstrumentModel instrumentModel = db.InstrumentModels.Find(id);
            db.InstrumentModels.Remove(instrumentModel);
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
