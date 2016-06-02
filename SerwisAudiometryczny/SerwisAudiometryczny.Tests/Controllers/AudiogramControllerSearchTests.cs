using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SerwisAudiometryczny;

namespace AudiogramController.Search.Tests
{
    [TestClass]
    public class AudiogramControllerSearchTests
    {
        //Nie mam pojęcia co robię (y)
        [TestMethod]
        public void SearchTest()
        {
            SerwisAudiometryczny.Controllers.AudiogramController Con = new SerwisAudiometryczny.Controllers.AudiogramController();

            ViewResult result = Con.Search() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}