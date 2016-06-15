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
    /// <summary>
    /// Klasa obsługująca testy związane z zarządzaniem logami.
    /// </summary>
    [TestClass()]
    public class LogControllerTests
    {
        /// <summary>
        /// Metoda sprawdzająca metodę Index
        /// </summary>
        [TestMethod()]
        public void IndexTest()
        {
            LogController controller = new LogController();
            int page = 2;
            /// <summary>
            /// Test sprawdza czy widok z wybranej strony logów nie jest pusty.
            /// </summary>
            Assert.IsNotNull(controller.Index(page));
        }
        /// <summary>
        /// Metoda sprawdzająca działanie metody LogSearch
        /// </summary>
        [TestMethod()]
        public void LogSearchTest()
        {
            LogController controller;
            int id = 123;
            var model = new LogSearchViewModel() { UserId = id };
            controller = new LogController();
            ViewResult view = controller.Search(model) as ViewResult;
            /// <summary>
            /// Metoda sprawdzająca czy widok po wyszukaniu logów o podanym id nie jest pusty.
            /// </summary>
            Assert.IsNotNull(view);
        }


    }
}