using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Controllers;
using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers.Tests
{

    [TestClass()]
    public class LogControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            LogController controller = new LogController();
            int page = 2;
            Assert.IsNotNull(controller.Index(page));
        }

        [TestMethod()]
        public void LogSearchTest()
        {
            LogController controller;
            int id = 123;
            var model = new LogSearchViewModel() { UserId = id };
            controller = new LogController();
            ViewResult view = controller.Search(model) as ViewResult;
            Assert.IsNotNull(view);
        }


    }
}