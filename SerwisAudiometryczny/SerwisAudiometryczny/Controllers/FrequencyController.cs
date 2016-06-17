using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obsługująca modele częstotliwości. Dziedziczy po Controller.
    /// </summary>
    [Authorize(Roles = "Administrator")]
    public class FrequencyController : Controller
    {
        private ModelsDbContext db;

        /// <summary>
        /// Kontruktor
        /// </summary>
        public FrequencyController()
        {
            db = new ModelsDbContext();
        }

        /// <summary>
        /// Kontruktor
        /// </summary>
        /// <param name="dbContext"></param>
        public FrequencyController(ModelsDbContext dbContext)
        {
            db = dbContext;
        }

        /// <summary>
        /// Metoda wyświetlająca spis wszystkich FrequencyModel.
        /// </summary>
        /// <param name="page"></param>
        [Authorize(Roles = "Administrator")]
        public ActionResult Index(int? page)
        {
            if (db.FrequencyModels.Count() == 0)
                ViewBag.GenerateButton = true;
            else
                ViewBag.GenerateButton = false;
            return View(db.FrequencyModels.ToList());
        }

        /// <summary>
        /// Metoda przekazująca FrequencyModel do widoku Details.
        /// </summary>
        /// <param name="id"></param>
        [Authorize(Roles = "Administrator")]
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
        /// Generuje 11 oktaw i wstawia do bazy
        /// </summary>
        /// <returns>Przekierowanie</returns>
        public ActionResult GenerateOctave()
        {
            FrequencyModel[] octave = new FrequencyModel[11];
            octave[0] = new FrequencyModel() { Frequency = 16 };
            octave[1] = new FrequencyModel() { Frequency = 32 };
            octave[2] = new FrequencyModel() { Frequency = 63 };
            octave[3] = new FrequencyModel() { Frequency = 125 };
            octave[4] = new FrequencyModel() { Frequency = 250 };
            octave[5] = new FrequencyModel() { Frequency = 500 };
            octave[6] = new FrequencyModel() { Frequency = 1000 };
            octave[7] = new FrequencyModel() { Frequency = 2000 };
            octave[8] = new FrequencyModel() { Frequency = 4000 };
            octave[9] = new FrequencyModel() { Frequency = 8000 };
            octave[10] = new FrequencyModel() { Frequency = 1600 };
            db.FrequencyModels.AddRange(octave);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Metoda wyświetlająca widok /Frequency/Create.
        /// </summary>
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
