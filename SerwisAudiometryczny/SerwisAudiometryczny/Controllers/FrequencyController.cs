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
    /// <summary>
    /// Klasa obsługująca modele częstotliwości. Dziedziczy po Controller.
    /// </summary>
    public class FrequencyController : Controller
    {
        private ModelsDbContext db;

        public FrequencyController()
        {
            db = new ModelsDbContext();
        }

        public FrequencyController(ModelsDbContext dbContext)
        {
            db = dbContext;
        }
        /// <summary>
        /// Metoda wyświetlająca spis wszystkich FrequencyModel.
        /// </summary>
        /// <param name="page"></param>
        public ActionResult Index(int? page)
        {
            return View(db.FrequencyModels.ToList());
        }

        /// <summary>
        /// Metoda przekazująca FrequencyModel do widoku Details.
        /// </summary>
        /// <param name="id"></param>
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

        /// <summary>
        /// Metoda wyświetlająca widok /Frequency/Create.
        /// </summary>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Metoda odbierająca FrequencyModel z widoku Create.
        /// </summary>
        /// <param name = "model" ></ param >
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Frequency")] FrequencyModel model)
        {
            if (ModelState.IsValid)
            {
                db.FrequencyModels.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// Metoda przekazująca FrequencyModel do widoku Edit.
        /// </summary>
        /// <param name = "id" ></ param >
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

        /// <summary>
        /// Metoda odbierająca FrequencyModel z widoku Edit.
        /// </summary>
        /// <param name = "model" ></ param >
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Frequency")] FrequencyModel model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// Metoda sprawdzająca możliwość usunięcia i wysyłająca żądanie usunięcia FrequencyModel.
        /// </summary>
        /// <param name = "id" ></ param >
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

        /// <summary>
        /// Metoda usuwająca FrequencyModel.
        /// </summary>
        /// <param name = "id" ></ param >
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
