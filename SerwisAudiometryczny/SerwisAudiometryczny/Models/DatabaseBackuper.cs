using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.IO.Compression;

namespace SerwisAudiometryczny.Models
{
    public class DatabaseBackuper
    {
        /// <summary>
        /// Metoda wykonująca kopię zapasową bazy danych (serializacja do XML).
        /// </summary>
        /// <returns>Stream zawierający archiwum z plikami xml</returns>
        public Stream Backup()
        {
            ModelsDbContext dbContext = new ModelsDbContext();

            string zipPath = HttpContext.Current.Server.MapPath("~/App_Data/backup.zip");
            string backupPath = HttpContext.Current.Server.MapPath("~/App_Data/Backup");
            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath); // Utworzenie tymczasowego katalogu Backup

            // Kopia zapasowa logów
            serializeXMLFromObject(dbContext.LogModels.ToList(), "logModels");
            // Kopia zapasowa audiogramów
            serializeXMLFromObject(dbContext.AudiogramModels.ToList(), "audiogramModels");
            // Kopia zapasowa instrumentów 
            serializeXMLFromObject(dbContext.InstrumentModels.ToList(), "instrumentModels");
            // Kopia zapasowa częstotliwości
            serializeXMLFromObject(dbContext.FrequencyModels.ToList(), "frequencyModels");

            ZipFile.CreateFromDirectory(backupPath, zipPath);
            Directory.Delete(backupPath, true); // usunięcie tymczasowego katalogu Backup wraz z zawartością

            return new FileStream(zipPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.DeleteOnClose);
        }

        private void serializeXMLFromObject(object obj, string filename)
        {
            string path = HttpContext.Current.Server.MapPath($"~/App_Data/Backup/{filename}.xml");
            using (StreamWriter writer = new StreamWriter(path))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(writer, obj);
            };
        }

        /// <summary>
        /// Metoda przywracająca bazę danych (deserializacja z plików XML).
        /// </summary>
        /// <param name="stream">Stream zawierający archiwum kopii zapasowej (.zip)
        /// wraz ze wszystkimi plikami (.xml).</param>
        public void Restore(Stream stream)
        {
            ModelsDbContext dbContext = new ModelsDbContext();
            dbContext.Database.Delete();

            string zipPath = HttpContext.Current.Server.MapPath("~/App_Data/backup.zip");
            string backupPath = HttpContext.Current.Server.MapPath("~/App_Data/Backup");
            if (!Directory.Exists(backupPath))
                Directory.CreateDirectory(backupPath);

            // utworzenie archiwum przesłanego w streamie na serwerze
            using (FileStream fileStream = new FileStream(zipPath, FileMode.Create))
            {
                stream.CopyTo(fileStream);
            }
            ZipFile.ExtractToDirectory(zipPath, backupPath); // rozpakowanie archiwum do katalogu Backup
            
            // Przywracanie logów
            List<LogModel> listOfLogModels = deserializeXMLToObject<LogModel>("logModels");
            listOfLogModels.ForEach(item => dbContext.LogModels.Add(item));
            dbContext.SaveChanges();

            // Przywracanie audiogramów
            List<AudiogramModel> listOfAudiogramModels = deserializeXMLToObject<AudiogramModel>("audiogramModels");
            listOfAudiogramModels.ForEach(item => dbContext.AudiogramModels.Add(item));
            dbContext.SaveChanges();

            // Przywracanie instrumentów 
            List<InstrumentModel> listOfInstrumentModels = deserializeXMLToObject<InstrumentModel>("instrumentModels");
            listOfInstrumentModels.ForEach(item => dbContext.InstrumentModels.Add(item));
            dbContext.SaveChanges();

            // Przywracanie częstotliwości
            List<FrequencyModel> listOfFrequencyModels = deserializeXMLToObject<FrequencyModel>("frequencyModels");
            listOfFrequencyModels.ForEach(item => dbContext.FrequencyModels.Add(item));
            dbContext.SaveChanges();

            // usunięcie plików tymczasowych potrzebnych do przywrócenia bazy
            Directory.Delete(backupPath, true); 
            File.Delete(zipPath);
        }

        private List<T> deserializeXMLToObject<T>(string filename)
        {
            string path = HttpContext.Current.Server.MapPath($"~/App_Data/Backup/{filename}.xml");
            List<T> list;
            using (Stream reader = new FileStream(path, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<T>));
                list = (List<T>) xmlSerializer.Deserialize(reader);
            }
            return list;
        }
    }

}