using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerwisAudiometryczny.Models.Tests
{
    /// <summary>
    /// Klasa obsługująca testy związane z tworzeniem logów.
    /// </summary>
    [TestClass()]
    public class LogModelTests
    {
        /// <summary>
        /// Metoda sprawdzająca czy stworzenie nowego loga o podanych parametrach zadziała.
        /// </summary>
        [TestMethod()]
        public void ToStringTest()
        {
            /// <summary>
            /// Stworzenie nowego loga.
            /// </summary>
            LogModel model = new LogModel();
            model.Time = null;
            model.UserId = 333;
            model.Controller = "control" ;            
            model.Action = "usunieto uzytkownika";
            /// <summary>
            /// Porównanie wyniku działania metody ToString z wartościami oczekiwanymi.
            /// </summary>
            Assert.AreEqual(model.ToString(), "null, 333, control, usunieto uzytkownika");
        }
    }
}