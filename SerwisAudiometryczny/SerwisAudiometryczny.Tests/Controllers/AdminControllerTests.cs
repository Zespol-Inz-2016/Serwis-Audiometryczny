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
        /// <summary>
        /// Test metody wyświetlającej panel administratora.
        /// Sprawdza czy po wykonaniumetoda nie zwraca wartości "null".
        /// </summary>
        [TestMethod()]
        public void IndexTest()
        {
            AdminController controller = new AdminController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);

        }
    }
}