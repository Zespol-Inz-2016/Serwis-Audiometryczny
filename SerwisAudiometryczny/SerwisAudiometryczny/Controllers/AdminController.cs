using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers
{
    public class AdminController : Controller
    {
        ApplicationDbContext db = ApplicationDbContext.Create();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }
        public ActionResult Edit(FormCollection form)
        {
            string name = form["Name"];
            string submit = form["Submit"];
            bool administrator = FormParser(form["Administrator"]);
            bool patient = FormParser(form["Patient"]);
            bool researcher = FormParser(form["Researcher"]);
            bool user = FormParser(form["User"]);
            var CurrentUser = db.Users.FirstOrDefault(x => x.UserName == name);
            if (CurrentUser == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            switch (submit)
            {
                case "Save":
                    CurrentUser.Administrator = administrator;
                    CurrentUser.Patient = patient;
                    CurrentUser.Researcher = researcher;
                    CurrentUser.User = user;
                    break;
                default:
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: InstrumentModels/Create
        public ActionResult Create()
        {
            return View();
        }
        private bool FormParser(string value)
        {
            if (value == "on")
            {
                return true;
            }
            return false;
        }

    }
    
}