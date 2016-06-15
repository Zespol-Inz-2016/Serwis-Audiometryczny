using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using SerwisAudiometryczny.ActionFilters;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obsługująca modele Instrumentów. Dziedziczy po Controller.
    /// </summary>
    public class InstrumentsController : Controller
    {
        private ModelsDbContext db;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public InstrumentsController()
        {
            db = new ModelsDbContext();
        }

        /// <summary>
        /// Kontruktor
        /// </summary>
        /// <param name="dbContext"></param>
        public InstrumentsController(ModelsDbContext dbContext)
        {
            db = dbContext;
        }

        /// <summary>
        /// Metoda wyświetlająca spis wszystkich InstrumentModel.
        /// </summary>
        /// <param name="page"></param>
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int? page)
        {
            return View(db.InstrumentModels.ToList());
        }

        /// <summary>
        /// Metoda przekazująca InstrumentModel do widoku Details.
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "Administrator")]
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

        /// <summary>
        /// Metoda wyświetlająca widok /Instruments/Create.
        /// </summary>
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Metoda odbierająca InstrumentModel z widoku Create.
        /// </summary>
        /// <param name = "model" ></ param >
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

        /// <summary>
        /// Metoda przekazująca InstrumentModel do widoku Edit.
        /// </summary>
        /// <param name = "id" ></ param >
        [Authorize(Roles = "Administrator")]
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

        /// <summary>
        /// Metoda odbierająca InstrumentModel z widoku Edit.
        /// </summary>
        /// <param name = "model" ></ param >
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

        /// <summary>
        /// Metoda sprawdzająca możliwość usunięcia i wysyłająca żądanie usunięcia InstrumentModel.
        /// </summary>
        /// <param name = "id" ></ param >
        [Authorize(Roles = "Administrator")]
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

        /// <summary>
        /// Metoda usuwająca InstrumentModel.
        /// </summary>
        /// <param name = "id" ></ param >
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
