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
        private object Server;
        private object databaseBackuper;

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
            DatabaseBackuper databaseBackuper = new DatabaseBackuper();
            Stream stream = databaseBackuper.Backup();
            string contentType = MimeMapping.GetMimeMapping(Server.ToString());
            string fileName = DateTime.Now.ToString("yyyy-MM-dd") + "_SerwisAudiometryczny.zip";
            FileStreamResult aktualny = new FileStreamResult(stream, contentType) { FileDownloadName = fileName };

            FileResult oczekiwany = TestSend.SendFile() as FileResult;

            Assert.AreSame(oczekiwany, aktualny);
        }
    }
}