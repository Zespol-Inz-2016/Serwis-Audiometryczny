using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Controllers.Tests
{
    [TestClass()]
    public class AdminControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            AdminController controller = new AdminController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);

        }

        [TestMethod()]
        public void CreateTest()
        {
            AdminController controller = new AdminController();
            ViewResult result = controller.Create() as ViewResult;
            Assert.IsNotNull(result);
        }

    }
}