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

namespace SerwisAudiometryczny.Controllers
{
    /// <summary>
    /// Kontroler logów
    /// </summary>
    public class LogController : Controller
    {
        private ModelsDbContext db = new ModelsDbContext();

        /// <summary>
        /// Zwraca widok strony głównej logów
        /// </summary>
        /// <param name="page">Określa numer strony</param>
        /// <returns></returns>
        // GET: Log
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(db.LogModels.OrderByDescending(i => i.Time).ToPagedList(pageNumber, pageSize));
        }
        /// <summary>
        /// Zwraca widok wyszukiwarki
        /// </summary>
        /// <returns></returns>
        // GET: Search
        public ActionResult Search()
        {
            return View();
        }
        /// <summary>
        /// Zwraca widok wyszukiwarki po wysłaniu zapytania
        /// </summary>
        /// <param name="model">Określa zmienne z modelu LogSearchViewModel</param>
        /// <returns></returns>
        // POST: Search
        [HttpPost]
        public ActionResult Search(LogSearchViewModel model)
        {
            var logs = from i in db.LogModels
                       select i;
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
