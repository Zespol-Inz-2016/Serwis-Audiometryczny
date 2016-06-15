using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SerwisAudiometryczny.Models;
using Moq;
using System.Data.Entity;

namespace SerwisAudiometryczny.Controllers.Tests
{

    /// <summary>
    /// Klasa obsługująca testy związane z zarządzaniem audiogramami.
    /// </summary>
    [TestClass()]
    public class AudiogramControllerTests
    {
        /// <summary>
        /// Sprawdzenie czy metoda Search bez argumentów wyświetla widok.
        /// </summary>
        [TestMethod()]
        public void SearchTest()
        {
            var AC = new AudiogramController();

            var result = AC.Search() as ViewResult;

            Assert.IsNotNull(result);
        }

        /// <summary>
        /// Sprawdzenie czy metoda Search zwraca nic dla wartości null.
        /// </summary>
        public void SearchTest1()
        {
            var AC = new AudiogramController();

            var ASVRM = new AudiogramSearchViewModel();

            var result = AC.Search(null, ASVRM) as ViewResult;

            Assert.IsNull(result);
        }

        /// <summary>
        /// Sprawdzenie czy metoda Search zwraca dane dla zadanych argumentów.
        /// </summary>
        [TestMethod()]
        public void SearchTest2()
        {
            // Przetestowanie metody nie jest możliwe ponieważ właściwość AudiogramModels w ModelsDbContext nie jest wirtualna. 
            // Uniemożliwa to mockowanie właściwości niekomercyjnymi frameworkami.
        }

        /// <summary>
        /// Sprawdzenie czy audiogramy są wyświetlane.
        /// </summary>
        [TestMethod()]
        public void IndexTest()
        {
            // Działanie metody nie może zostać poprawnie przetestowane ze względu na istniejące w niej odwołnie do prywatnej metody GetUser().

            // Przetestowanie metody nie jest możliwe ponieważ właściwość AudiogramModels w ModelsDbContext nie jest wirtualna. 
            // Uniemożliwa to mockowanie właściwości niekomercyjnymi frameworkami.
        }

        /// <summary>
        /// Metoda sprawdza czy żaden audiogram nie jest wyświetlany dla argumentu null. 
        /// </summary>
        [TestMethod()]
        public void DetailsTest()
        {
            var AC = new AudiogramController();

            var ASVRM = new AudiogramSearchViewModel();

            var result = AC.Details(null) as ViewResult;

            Assert.IsNull(result);
        }

        /// <summary>
        /// Metoda sprawdza czy audiogram o podanym ID jest wyświetlany.
        /// </summary>
        [TestMethod()]
        public void DetailsTest1()
        {
            // Działanie metody nie może zostać poprawnie przetestowane ze względu na istniejące w niej odwołnie do prywatnej metody GetUser().

            // Przetestowanie metody nie jest możliwe ponieważ właściwość AudiogramModels w ModelsDbContext nie jest wirtualna. 
            // Uniemożliwa to mockowanie właściwości niekomercyjnymi frameworkami.
        }
        
        /// <summary>
        /// Sprawdzenie czy nowy audiogram jest tworzony.
        /// </summary>
        [TestMethod()]
        public void CreateTest()
        {
            // Przetestowanie metody nie jest możliwe ponieważ właściwość AudiogramModels w ModelsDbContext nie jest wirtualna. 
            // Uniemożliwa to mockowanie właściwości niekomercyjnymi frameworkami.
        }

        /// <summary>
        /// Sprawdzenie czy metoda Delete nie pozawala na usunięcie żadnego audiogramu gdy argumentem jest null.
        /// </summary>
        [TestMethod()]
        public void DeleteTest()
        {
            var AC = new AudiogramController();

            var result = AC.Delete(null) as ViewResult;

            Assert.IsNull(result);
        }

        /// <summary>
        /// Sprawdzenie czy metoda Delete pozwala na usunięcie audiogramu o ID podanym jako argument.
        /// </summary>
        [TestMethod()]
        public void DeleteTest1()
        {
            // Przetestowanie metody nie jest możliwe ponieważ właściwość AudiogramModels w ModelsDbContext nie jest wirtualna. 
            // Uniemożliwa to mockowanie właściwości niekomercyjnymi frameworkami.
        }

        /// <summary>
        /// Metoda sprawdza czy audiogram o podanym ID został usunięty.
        /// </summary>
        [TestMethod()]
        public void DeleteConfirmedTest()
        {
            // Przetestowanie metody nie jest możliwe ponieważ właściwość AudiogramModels w ModelsDbContext nie jest wirtualna. 
            // Uniemożliwa to mockowanie właściwości niekomercyjnymi frameworkami.
        }
    }
}