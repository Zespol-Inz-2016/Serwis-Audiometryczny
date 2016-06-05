using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace SerwisAudiometryczny.Controllers
{
    public class AudiogramController : Controller
    {
        private ModelsDbContext db;

        public AudiogramController()
        {
            db = new ModelsDbContext();
        }

        public AudiogramController(ModelsDbContext dbContext)
        {
            db = dbContext;
        }

        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// Metoda wyszukuje w bazie danych audiogramy spełniające określone warunki
        /// </summary>
        /// <param name="page"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(int? page, AudiogramSearchViewModel model)
        {
            var results = from t in db.AudiogramModels
                          select t;

            if (model.PatientName != null)
            {
                List<AudiogramModel> res = new List<AudiogramModel>();

                foreach (var t in results)
                {
                    ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById<ApplicationUser, int>(t.ID);
                    string userName = user.Name;
                    if (userName.Contains(model.PatientName))
                    {
                        res.Add(t);
                    }
                }
                results = (IQueryable<AudiogramModel>)res;
            }

            if (model.Diagnosis != null)
            {
                results = from t in results
                          where t.Diagnosis.Contains(model.Diagnosis)
                          select t;
            }
            if (model.Gender != null)
            {
                results = from t in results
                          where t.Gender == (AudiogramModel.Genders)model.Gender
                          select t;
            }
            if (model.ageFrom != null)
            {
                results = from t in results
                          where t.Age >= model.ageFrom
                          select t;
            }
            if (model.ageTo != null)
            {
                results = from t in results
                          where t.Age <= model.ageTo
                          select t;
            }
            if (model.PercentageHearingLossFrom != null)
            {
                results = from t in results
                          where t.PercentageHearingLoss >= model.PercentageHearingLossFrom
                          select t;
            }
            if (model.PercentageHearingLossTo != null)
            {
                results = from t in results
                          where t.PercentageHearingLoss <= model.PercentageHearingLossTo
                          select t;
            }
            if (model.isMusican != null)
            {
                results = from t in results
                          where t.IsMusician == model.isMusican
                          select t;
            }
            if (model.Patient != null)
            {
                results = from t in results
                          where t.PatientID == model.Patient.Id
                          select t;
            }
            if (model.Editor != null)
            {
                results = from t in results
                          where t.EditorID == model.Editor.Id
                          select t;
            }
            return View(results.OrderByDescending(x => x.ID).ToList());

        }
        /// <summary>
        /// Metoda wyświetlająca wszystkie dostępne użytkownikowi audiogramy.
        /// </summary>
        /// <param name="page"></param>
        public ActionResult Index(int? page)
        {
            return View(db.AudiogramModels.ToList());
        }

        /// <summary>
        /// Metoda przekazująca do widoku Details AudiogramDisplayViewModel.
        /// </summary>
        /// <param name="page"></param>
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (audiogramModel == null || (audiogramModel.EditorID != User.Identity.GetUserId<int>() && audiogramModel.PatientID != User.Identity.GetUserId<int>()))
            {
                return HttpNotFound();
            }
            AudiogramDisplayViewModel audiogramDisplay = new AudiogramDisplayViewModel();
            audiogramDisplay.Audiogram = audiogramModel;

            //Może być null. Wyświetlić brak edytora.
            var datab = ApplicationDbContext.Create();
            ApplicationUser Editor = datab.Users.FirstOrDefault(x => x.Id == audiogramModel.EditorID);
            audiogramDisplay.Editor = Editor;

            //Może być null. Wyświetlić brak pacjenta.
            ApplicationUser Patient = datab.Users.FirstOrDefault(x => x.Id == audiogramModel.PatientID);
            audiogramDisplay.Patient = Patient;

            FrequencyModel[] FrequencyModelArray = db.FrequencyModels.ToArray();
            if (FrequencyModelArray==null)
            {
                return HttpNotFound();
            }
            int[] FrequencyIntArray = new int[FrequencyModelArray.Length];
            for (int i = 0; i < FrequencyModelArray.Length; i++)
            {
                FrequencyIntArray[i] = FrequencyModelArray[i].Frequency;
            }
            audiogramDisplay.Frequencies = FrequencyIntArray;

            return View(audiogramDisplay);
        }

        /// <summary>
        /// Metoda przekazująca do widoku Create AudiogramCreateEditViewModel.
        /// </summary>
        public ActionResult Create()
        {
            AudiogramCreateEditViewModel audiogramCreate = new AudiogramCreateEditViewModel();
            audiogramCreate.Audiogram = new AudiogramModel();
            audiogramCreate.Audiogram.EditorID = User.Identity.GetUserId<int>();

            List<InstrumentModel> InstrumentModelList = db.InstrumentModels.ToList();
            if (InstrumentModelList == null)
            {
                return HttpNotFound();
            }
            audiogramCreate.Instruments = InstrumentModelList;

            FrequencyModel[] FrequencyModelArray = db.FrequencyModels.ToArray();
            if (FrequencyModelArray == null)
            {
                return HttpNotFound();
            }
            audiogramCreate.Frequencies = new int[FrequencyModelArray.Length];
            for (int i = 0; i < FrequencyModelArray.Length; i++)
            {
               audiogramCreate.Frequencies[i] = FrequencyModelArray[i].Frequency;
            }

            return View(audiogramCreate);
        }

        /// <summary>
        /// Metoda odbierająca z widoku Create AudiogramCreateEditViewModel.
        /// </summary>
        /// <param name = "audiogramCreate" ></ param >
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AudiogramCreateEditViewModel audiogramCreate)
        {
            if (ModelState.IsValid)
            {
                db.AudiogramModels.Add(audiogramCreate.Audiogram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            audiogramCreate.Audiogram.EditorID = User.Identity.GetUserId<int>();
            FrequencyModel[] FrequencyModelArray = db.FrequencyModels.ToArray();
            if (FrequencyModelArray == null)
            {
                return HttpNotFound();
            }
            audiogramCreate.Frequencies = new int[FrequencyModelArray.Length];
            for (int i = 0; i < FrequencyModelArray.Length; i++)
            {
                audiogramCreate.Frequencies[i] = FrequencyModelArray[i].Frequency;
            }
            return View(audiogramCreate);
        }

        /// <summary>
        /// Metoda przekazująca do widoku Edit AudiogramCreateEditViewModel.
        /// </summary>
        /// <param name = "id" ></ param >
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (audiogramModel == null || audiogramModel.EditorID != User.Identity.GetUserId<int>())
            {
                return HttpNotFound();
            }
            AudiogramCreateEditViewModel audiogramEdit = new AudiogramCreateEditViewModel();
            audiogramEdit.Audiogram = audiogramModel;

            List<InstrumentModel> InstrumentModelList = db.InstrumentModels.ToList();
            if (InstrumentModelList==null)
            {
                return HttpNotFound();
            }
            audiogramEdit.Instruments = InstrumentModelList;

            FrequencyModel[] FrequencyModelArray = db.FrequencyModels.ToArray();
            if (FrequencyModelArray == null)
            {
                return HttpNotFound();
            }
            audiogramEdit.Frequencies = new int[FrequencyModelArray.Length];
            for (int i = 0; i < FrequencyModelArray.Length; i++)
            {
                audiogramEdit.Frequencies[i] = FrequencyModelArray[i].Frequency;
            }

            return View(audiogramEdit);
        }

        /// <summary>
        /// Metoda odbierająca z widoku Edit AudiogramCreateEditViewModel.
        /// </summary>
        /// <param name = "audiogramEdit" ></ param >
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AudiogramCreateEditViewModel audiogramEdit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(audiogramEdit.Audiogram).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<InstrumentModel> InstrumentModelList = db.InstrumentModels.ToList();
            if (InstrumentModelList == null)
            {
                return HttpNotFound();
            }
            audiogramEdit.Instruments = InstrumentModelList;

            FrequencyModel[] FrequencyModelArray = db.FrequencyModels.ToArray();
            if (FrequencyModelArray == null)
            {
                return HttpNotFound();
            }
            audiogramEdit.Frequencies = new int[FrequencyModelArray.Length];
            for (int i = 0; i < FrequencyModelArray.Length; i++)
            {
                audiogramEdit.Frequencies[i] = FrequencyModelArray[i].Frequency;
            }

            return View(audiogramEdit);
        }

        /// <summary>
        /// Metoda sprawdzająca możliwość usunięcia i usuwająca audiogram.
        /// </summary>
        /// <param name = "id" ></ param >
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

        /// <summary>
        /// Metoda zapisująca usunięcie audiogramu audiogramu.
        /// </summary>
        /// <param name = "id" ></ param >
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
