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
            FileStreamResult fileStreamResult = new FileStreamResult(stream, contentType) { FileDownloadName = fileName };

            FileResult wynik = TestSend.SendFile() as FileResult;
            Assert.AreEqual(wynik,fileStreamResult);
        }

        [TestMethod()]
        public void CreateTest()
        {
            BackupController TestCreate = new BackupController();
            FormCollection collection = new FormCollection();
            ViewResult wynik = TestCreate.Create(collection) as ViewResult;

            Assert.IsNotNull(wynik);

        }

        [TestMethod()]
        public void EditTest1()
        {
            BackupController TestEdit = new BackupController();
            int PodaneID = 2; //należy podać ID 
            ViewResult wynik = TestEdit.Edit(PodaneID) as ViewResult;

            Assert.IsNotNull(wynik);
        }

        [TestMethod()]
        public void EditTest2()
        {
            BackupController TestEdit = new BackupController();
            int PodaneID = 2; //należy podać ID 
            FormCollection collect = new FormCollection();
            ViewResult wynik = TestEdit.Edit(PodaneID,collect) as ViewResult;

            Assert.IsNotNull(wynik);
        }


        [TestMethod()]
        public void DeleteTest1()
        {
            BackupController TestDelete = new BackupController();
            int PodaneID = 5;
            ViewResult wynik = TestDelete.Delete(PodaneID) as ViewResult;

        }

        [TestMethod()]
        public void DeleteTest2()
        {
            BackupController TestDelete = new BackupController();
            int PodaneID = 3;
            FormCollection collect = new FormCollection();
            ViewResult wynik = TestDelete.Delete(PodaneID, collect) as ViewResult;

        }
    }
}