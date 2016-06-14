using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;



namespace SerwisAudiometryczny.Controllers.Tests
{
    [TestClass()]
    public class BackupControllerTests
    {

        [TestMethod()]
        public void ImportTest()
        {
            BackupController TestImport = new BackupController();
            ViewResult wynik = TestImport.Import() as ViewResult;

            Assert.IsNotNull(wynik);
        }


        [TestMethod()]
        public void ExportTest()
        {
            BackupController TestExport = new BackupController();
            ViewResult wynik = TestExport.Export() as ViewResult;

            Assert.IsNotNull(wynik);
            
        }

        [TestMethod()]
        public void SendFileTest()
        {
            BackupController TestSend = new BackupController();
            FileResult oczekiwany = TestSend.SendFile() as FileResult;
            bool wynik = Convert.ToBoolean(oczekiwany);
            Assert.IsFalse(wynik);
        }
    }
}