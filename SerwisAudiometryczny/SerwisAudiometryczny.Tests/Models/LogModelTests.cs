using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models.Tests
{
    [TestClass()]
    public class LogModelTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            LogModel model = new LogModel();
            model.Time = null;
            model.UserId = 333;
            model.Controller = "control" ;            
            model.Action = "usunieto uzytkownika";

            Assert.AreEqual(model.ToString(), "null, 333, control, usunieto uzytkownika");
        }
    }
}