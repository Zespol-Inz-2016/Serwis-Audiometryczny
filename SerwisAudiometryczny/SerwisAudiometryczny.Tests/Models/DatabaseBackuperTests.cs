using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerwisAudiometryczny.Models;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SerwisAudiometryczny.Models.Tests
{
    [TestClass()]
    public class DatabaseBackuperTests       
    {
        string zipPath = HttpContext.Current.Server.MapPath("~/App_Data/backup.zip");

        /// <summary>
        /// Test sprawdza czy utworzony plik jest taki sam jak utworzony w teście.
        /// </summary>
        [TestMethod()]
        public void BackupTest()
        {
            DatabaseBackuper TestBackup = new DatabaseBackuper();

            FileStream wynik = new FileStream(zipPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.DeleteOnClose);
            FileStream oczekiwany = TestBackup.Backup() as FileStream;

            Assert.AreSame(oczekiwany, wynik);


        }


        [TestMethod()]
        public void RestoreTest()
        {

        }
    }
}
