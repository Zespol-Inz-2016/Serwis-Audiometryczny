using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;




namespace SerwisAudiometryczny.Controllers.Tests
{
    [TestClass()]
    public class BackupControllerTests:Controller
    {
        DatabaseBackuper databaseBackuper = new DatabaseBackuper();

        /// <summary>
        /// Test sprawdza czy po wykonaniu metoda nie zwraca wartości "null".
        /// </summary>
        [TestMethod()]
        public void ImportTest()
        {
            BackupController TestImport = new BackupController();
            ViewResult wynik = TestImport.Import() as ViewResult;

            Assert.IsNotNull(wynik);
        }

        /// <summary>
        /// Test sprawdza czy po wykonaniu metoda nie zwraca wartości "null".
        /// </summary>
        [TestMethod()]
        public void ExportTest()
        {
            BackupController TestExport = new BackupController();
            ViewResult wynik = TestExport.Export() as ViewResult;

            Assert.IsNotNull(wynik);
            
        }

        /// <summary>
        /// Test sprawdza czy plik utworzony przez metodę jest taki sam jak utworzony w teście.
        /// </summary>
        [TestMethod()]
        public void SendFileTest()
        {
            BackupController TestSend = new BackupController();

            Stream stream = databaseBackuper.Backup("bp");
            string contentType = MimeMapping.GetMimeMapping(Server.MapPath("~/App_Data/backup.zip"));
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_SerwisAudiometryczny.zip";
            FileStreamResult wynik = new FileStreamResult(stream, contentType) { FileDownloadName = fileName };

            FileResult oczekiwany = TestSend.SendFile() as FileResult;
            Assert.AreSame(oczekiwany, wynik);
        }
    }
}