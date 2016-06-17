using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using PagedList;
using SerwisAudiometryczny.ActionFilters;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Security;

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Klasa obsługująca logowanie akcji użytkowników. Dziedziczy po Controller.
    /// </summary>
    public class LogController : Controller
    {
        private ModelsDbContext db;
        private ApplicationDbContext dba;

        /// <summary>
        /// Konstruktor
        /// </summary>
        public LogController()
        {
            db = new ModelsDbContext();
            dba = new ApplicationDbContext();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="dbContext"></param>
        public LogController(ModelsDbContext dbContext)
        {
            db = dbContext;
        }

        /// <summary>
        /// Zwraca widok strony głównej logów z uwzględnieniem aktualnego użytkownika.
        /// </summary>
        /// <param name="page">Określa numer strony</param>
        /// <returns></returns>
        // GET: Log
        [Authorize(Roles = "Administrator, Patient, Researcher, User")]
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            int currentUserId = User.Identity.GetUserId<int>();

            UserManager<ApplicationUser, int> UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(new ApplicationDbContext()));

            if (User != null && UserManager.IsInRole(currentUserId, "Administrator"))
                return View(db.LogModels.OrderByDescending(i => i.Time).ToPagedList(pageNumber, pageSize));
            if (User == null)
                return new ViewResult { ViewName = "Unauthorized" };

            return View(db.LogModels.Where(x => x.UserId == currentUserId).OrderByDescending(i => i.Time).ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// Zwraca widok wyszukiwarki.
        /// </summary>
        /// <returns></returns>
        // GET: Search
        public ActionResult Search()
        {
            return View();
        }
        /// <summary>
        /// Zwraca widok wyszukiwarki po wysłaniu zapytania.
        /// </summary>
        /// <param name="model">Określa zmienne z modelu LogSearchViewModel</param>
        /// <returns></returns>
        // POST: Search
        [HttpPost]
        public ActionResult Search(LogSearchViewModel model)
        {
            UserManager<ApplicationUser, int> UserManager = new UserManager<ApplicationUser, int>(new CustomUserStore(new ApplicationDbContext()));
            var logs = from i in db.LogModels
                       select i;
            if (!UserManager.IsInRole(User.Identity.GetUserId<int>(), AppRoles.Administrator.ToString()))
            {
                logs = from i in db.LogModels
                       where i.UserId == User.Identity.GetUserId<int>()
                       select i;
            }

            if (model.UserId != null)
            {
                logs = from i in logs
                       where i.UserId == model.UserId
                       select i;
            }
            if (model.Controller != null)
            {
                logs = from i in logs
                       where i.Controller.Contains(model.Controller)
                       select i;
            }
            if (model.Action != null)
            {
                logs = from i in logs
                       where i.Action.Contains(model.Action)
                       select i;
            }
            if (model.FromTime != null)
            {
                logs = from i in logs
                       where i.Time >= model.FromTime
                       select i;
            }
            if (model.ToTime != null)
            {
                logs = from i in logs
                       where i.Time <= model.ToTime
                       select i;
            }

            return View(logs.OrderByDescending(x => x.Time).ToList());
        }
    }
}