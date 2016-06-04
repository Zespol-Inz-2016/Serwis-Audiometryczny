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
    public class AudiogramControllerTests
    {
        [TestMethod()]
        public void SearchTest()
        {

        }

        [TestMethod()]
        public void SearchTest1()
        {

        }

        [TestMethod()]
        public void IndexTest()
        {
            var AC = new AudiogramController();

            var result = AC.Index(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DetailsTest()
        {
            var AC = new AudiogramController();

            var result = AC.Details(1) as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var AC = new AudiogramController();

            var result = AC.Create() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void CreateTest1()
        {

        }

        [TestMethod()]
        public void EditTest()
        {

        }

        [TestMethod()]
        public void EditTest1()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {

        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {

        }
    }
}