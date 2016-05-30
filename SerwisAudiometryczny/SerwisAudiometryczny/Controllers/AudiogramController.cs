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
        private ModelsDbContext db = new ModelsDbContext();

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
                    ApplicationUser user = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById<ApplicationUser, string>(t.ID.ToString());
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
                          where t.PatientID.ToString() == model.Patient.Id
                          select t;
            }
            if (model.Editor != null)
            {
                results = from t in results
                          where t.EditorID.ToString() == model.Editor.Id
                          select t;
            }
            return View(results.OrderByDescending(x => x.ID).ToList());

        }

        // GET: AudiogramModels
        public ActionResult Index(int? page)
        {
            return View();
        }

        // GET: AudiogramModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AudiogramModel audiogramModel = db.AudiogramModels.Find(id);
            if (audiogramModel == null || (audiogramModel.EditorID != User.Identity.GetUserId() && audiogramModel.PatientID != User.Identity.GetUserId()))
            {
                return HttpNotFound();
            }
            AudiogramDisplayViewModel audiogramDisplay = new AudiogramDisplayViewModel();
            audiogramDisplay.Audiogram = audiogramModel;

            var datab = ApplicationDbContext.Create();
            ApplicationUser Editor = datab.Users.FirstOrDefault(x => x.Id == audiogramModel.EditorID);
            audiogramDisplay.Editor = Editor;

            ApplicationUser Patient = datab.Users.FirstOrDefault(x => x.Id == audiogramModel.PatientID);
            audiogramDisplay.Patient = Patient;


            return View(audiogramDisplay);
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
            if (audiogramModel == null || audiogramModel.EditorID != User.Identity.GetUserId())
            {
                return HttpNotFound();
            }
            AudiogramCreateEditViewModel audiogramEdit = new AudiogramCreateEditViewModel();
            audiogramEdit.Audiogram = audiogramModel;
            return View(audiogramEdit);
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
