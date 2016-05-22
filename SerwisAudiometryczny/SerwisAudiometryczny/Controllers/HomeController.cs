using SerwisAudiometryczny.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers
{
    public class HomeController : Controller
    {
        [LogActionFilterAttribute] //atrybut tworzący logi

        public ActionResult Index()
        {
            return View();
        }
        [IsAdministratorAttribute]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}