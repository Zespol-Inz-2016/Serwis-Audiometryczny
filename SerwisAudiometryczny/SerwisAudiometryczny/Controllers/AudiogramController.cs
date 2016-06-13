using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SerwisAudiometryczny.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

/*! \namespace SerwisAudiometryczny.Controllers
    \brief Przestrzeń nazw dla kontrolerów.

    Przestrzeń nazw zawierająca wszystkie kontrolery w programie.
*/
namespace SerwisAudiometryczny.Controllers
{

    /// <summary>
    /// Klasa obługująca audiogramy i ich przypisywanie do pacjentów. Przekazuje dane związanie z audiogramami do widoków. Dziedziczy po Controller.
    /// </summary>
    public class AudiogramController : Controller
    {
        private ModelsDbContext db;
        private ApplicationDbContext datab;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public AudiogramController()
        {
            db = new ModelsDbContext();
            datab = new ApplicationDbContext();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="appDbContext"></param>
        public AudiogramController(ModelsDbContext dbContext, ApplicationDbContext appDbContext)
        {
            db = dbContext;
            datab = appDbContext;
        }

        /// <summary>
        /// Metoda zwracająca widok wyszukiwarki audiogramów.
        /// </summary>
        /// <returns></returns>
        public ActionResult Search()
        {
            return View();
        }

        /// <summary>
        /// Metoda wyszukuje w bazie danych audiogramy spełniające określone warunki.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Search(int? page, AudiogramSearchViewModel model)
        {
            var results = from t in db.AudiogramModels
                          select t;

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

            List<AudiogramModel> res = new List<AudiogramModel>();

            if (model.PatientName != null)
            {

                foreach (var t in results)
                {
                    ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById<ApplicationUser, int>(t.ID);
                    string userName = user.Name;
                    if (userName != null && userName.Contains(model.PatientName))
                    {
                        res.Add(t);
                    }
                }
            }

            return View(res.OrderByDescending(x => x.ID).ToList());

        }

        private ApplicationUser GetUser()
        {
            int userid = User.Identity.GetUserId<int>();
            return datab.Users.FirstOrDefault(x => x.Id == userid);
        }

        /// <summary>
        /// Metoda wyświetlająca wszystkie dostępne użytkownikowi audiogramy.
        /// </summary>
        /// <param name="page"></param>
        public ActionResult Index(int? page)
        {
            ApplicationUser CurrentUser = GetUser();

            if (CurrentUser != null)
            {
                if (CurrentUser.Researcher)
                {
                    return View(db.AudiogramModels.Include("Instrument").ToList());
                }
                if (CurrentUser.User && CurrentUser.Patient)
                {
                    var results = from t in db.AudiogramModels.Include("Instrument")
                                  where t.EditorID == CurrentUser.Id || t.PatientID == CurrentUser.Id
                                  select t;

                    return View(results.ToList());
                }
                if (CurrentUser.User)
                {
                    var results = from t in db.AudiogramModels.Include("Instrument")
                                  where t.EditorID == CurrentUser.Id
                                  select t;
                    return View(results.ToList());
                }
                if (CurrentUser.Patient)
                {
                    var results = from t in db.AudiogramModels.Include("Instrument")
                                  where t.PatientID == CurrentUser.Id
                                  select t;
                    return View(results.ToList());
                }
            }
            return new ViewResult { ViewName = "Unauthorized" };

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
            ApplicationUser CurrentUser = GetUser();

            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (audiogramModel == null)
            {
                return HttpNotFound();
            }

            if (CurrentUser != null && (CurrentUser.Researcher || (CurrentUser.User && audiogramModel.EditorID == CurrentUser.Id) || (CurrentUser.Patient && audiogramModel.PatientID == CurrentUser.Id)))
            {
                AudiogramDisplayViewModel audiogramDisplay = new AudiogramDisplayViewModel();
                audiogramDisplay.Audiogram = audiogramModel;

                ApplicationUser Editor = datab.Users.FirstOrDefault(x => x.Id == audiogramModel.EditorID);
                audiogramDisplay.Editor = Editor;

                ApplicationUser Patient = datab.Users.FirstOrDefault(x => x.Id == audiogramModel.PatientID);
                audiogramDisplay.Patient = Patient;

                FrequencyModel[] FrequencyModelArray = db.FrequencyModels.ToArray();
                if (FrequencyModelArray == null)
                {
                    return HttpNotFound();
                }
                int[] FrequencyIntArray = new int[FrequencyModelArray.Length];
                for (int i = 0; i < FrequencyModelArray.Length; i++)
                {
                    FrequencyIntArray[i] = FrequencyModelArray[i].Frequency;
                }
                audiogramDisplay.Frequencies = FrequencyIntArray;

                List<InstrumentModel> InstrumentModelList = db.InstrumentModels.ToList();
                if (InstrumentModelList == null)
                {
                    return HttpNotFound();
                }

                return View(audiogramDisplay);
            }
            return new ViewResult { ViewName = "Unauthorized" };
        }
        /// <summary>
        /// Metoda przekazująca do widoku Create AudiogramCreateEditViewModel.
        /// </summary>
        public ActionResult Create()
        {
            ApplicationUser CurrentUser = GetUser();

            if (CurrentUser != null && CurrentUser.User)
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
            return new ViewResult { ViewName = "Unauthorized" };

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
                if (audiogramCreate.Audiogram.IsMusician == true)
                {
                    InstrumentModel instrument = db.InstrumentModels.FirstOrDefault<InstrumentModel>(x => x.Name == audiogramCreate.Audiogram.Instrument.Name);
                    if (instrument == null)
                    {
                        db.InstrumentModels.Add(audiogramCreate.Audiogram.Instrument);
                        db.SaveChanges();
                    }
                    else
                    {
                        audiogramCreate.Audiogram.Instrument = instrument;
                    }
                }
                else
                {
                    audiogramCreate.Audiogram.Instrument = null;
                }
                db.AudiogramModels.Add(audiogramCreate.Audiogram);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            List<InstrumentModel> InstrumentModelList = db.InstrumentModels.ToList();
            if (InstrumentModelList == null)
            {
                return HttpNotFound();
            }
            audiogramCreate.Instruments = InstrumentModelList;

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
            ApplicationUser CurrentUser = GetUser();

            if (CurrentUser != null && CurrentUser.User)
            {
                AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
                if (audiogramModel == null)
                {
                    return HttpNotFound();
                }
                AudiogramCreateEditViewModel audiogramEdit = new AudiogramCreateEditViewModel();
                audiogramEdit.Audiogram = audiogramModel;

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
            return new ViewResult { ViewName = "Unauthorized" };
        }

        private AudiogramModel RewriteAudiogram(AudiogramModel TrackedAudiogram, AudiogramModel ReceivedAudiogram)
        {
            TrackedAudiogram.Age = ReceivedAudiogram.Age;
            TrackedAudiogram.Diagnosis = ReceivedAudiogram.Diagnosis;
            TrackedAudiogram.Gender = ReceivedAudiogram.Gender;
            TrackedAudiogram.IsMusician = ReceivedAudiogram.IsMusician;
            TrackedAudiogram.LeftEar = ReceivedAudiogram.LeftEar;
            TrackedAudiogram.RightEar = ReceivedAudiogram.RightEar;
            TrackedAudiogram.PercentageHearingLoss = ReceivedAudiogram.PercentageHearingLoss;
            TrackedAudiogram.Nuisance = ReceivedAudiogram.Nuisance;
            TrackedAudiogram.PatientID = ReceivedAudiogram.PatientID;
            return TrackedAudiogram;
        }

        /// <summary>
        /// Metoda odbierająca z widoku Edit AudiogramCreateEditViewModel.
        /// </summary>
        /// <param name = "audiogramEdit" ></ param >
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AudiogramCreateEditViewModel audiogramEdit)
        {
            AudiogramModel TrackedAudiogram = db.AudiogramModels.FirstOrDefault(x => x.ID == audiogramEdit.Audiogram.ID);
            TrackedAudiogram = RewriteAudiogram(TrackedAudiogram, audiogramEdit.Audiogram);
            if (ModelState.IsValid)
            {
                if (audiogramEdit.Audiogram.IsMusician == true)
                {
                    InstrumentModel instrument = db.InstrumentModels.FirstOrDefault<InstrumentModel>(x => x.Name == audiogramEdit.Audiogram.Instrument.Name);
                    if (instrument == null)
                    {
                        db.InstrumentModels.Add(audiogramEdit.Audiogram.Instrument);
                        db.SaveChanges();
                        TrackedAudiogram.Instrument = audiogramEdit.Audiogram.Instrument;
                    }
                    else
                    {
                        TrackedAudiogram.Instrument = instrument;
                    }
                }
                else
                {
                    InstrumentModel instr = db.InstrumentModels.FirstOrDefault<InstrumentModel>(x => x.Name == null);
                    if (instr == null)
                    {
                        TrackedAudiogram.Instrument = new InstrumentModel();
                    }
                    else
                    {
                        TrackedAudiogram.Instrument = instr;
                    }
                }

                db.Entry(TrackedAudiogram).State = EntityState.Modified;
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
        /// Metoda sprawdzająca możliwość usunięcia i wysyłająca żądanie usunięcia audiogramu.
        /// </summary>
        /// <param name = "id" ></ param >
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser CurrentUser = GetUser();

            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (CurrentUser != null && (CurrentUser.User && CurrentUser.Id == audiogramModel.EditorID))
            {
                if (audiogramModel == null)
                {
                    return HttpNotFound();
                }
                return View(audiogramModel);
            }
            return new ViewResult { ViewName = "Unauthorized" };
        }

        /// <summary>
        /// Metoda usuwająca audiogram.
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
