using System;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Specialized;
using System.IO;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SerwisAudiometryczny.Models
{
    public class DatabaseBackuper
    {
        private string databaseName = "SerwisAudiometryczny"; // nazwa bazy danych

        /// <summary>
        /// Właściwość zwracająca nazwę serwera(Data Source)
        /// </summary>
        private string serverName
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
                SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder(connectionString);

                return sqlBuilder.DataSource;
            }
        }

        /// <summary>
        /// Metoda wykonująca backup bazy danych do skryptu (.sql).
        /// </summary>
        /// <returns>Stream zawierający wykonany backup bazy danych.</returns>
        public Stream Backup()
        {
            ServerConnection connection = new ServerConnection(serverName);
            Server sqlServer = new Server(connection);
            Database database = sqlServer.Databases["SerwisAudiometryczny"];
            StringCollection databaseScript = database.Script();

            string backupDir = HttpContext.Current.Server.MapPath($"~/App_Data/backup.sql");
            FileStream fileStream = new FileStream(backupDir, FileMode.Create);
            StreamWriter sw = new StreamWriter(fileStream);

            //Dodanie daty wykonania skryptu 
            sw.WriteLine($"/*********     Skrypt wygenerowany: {DateTime.Now}    *********/\n");

            //Umieszczenie skryptu tworzącego bazę danych
            foreach (var item in databaseScript)
            {
                sw.WriteLine(item);
            }
            sw.WriteLine($"GO\n"); //Trzeba dodac GO, ponieważ najpierw trzeba stworzyć bazę, aby móc stworzyć w niej tabele

            Scripter script = new Scripter(sqlServer);
            ScriptingOptions scriptOptions = new ScriptingOptions()
            {
                IncludeDatabaseContext = true,
                ScriptData = true,
                ScriptSchema = true,
                ScriptDrops = false,
            };
            script.Options = scriptOptions;

            foreach (Table table in database.Tables)
            {
                foreach (var item in table.EnumScript(scriptOptions))
                {
                    sw.WriteLine(item);
                }
            }
            sw.Close();
            fileStream = new FileStream(backupDir, FileMode.Open, FileAccess.Read, FileShare.Read, 512, FileOptions.DeleteOnClose);

            return fileStream;
        }

        /// <summary>
        /// Metoda przywracajacą całą bazę danych.
        /// </summary>
        /// <param name="stream">Stream zawierający backup (.sql) z którego chcesz przywrócić bazę danych.</param>
        public void Restore(Stream stream)
        {         
            ServerConnection connection = new ServerConnection(serverName);
            Server sqlServer = new Server(connection);
            StreamReader streamReader = new StreamReader(stream);
            string script = streamReader.ReadToEnd();

            sqlServer.KillDatabase(databaseName);
            sqlServer.ConnectionContext.ExecuteNonQuery(script);
            connection.Disconnect();
        }
    }

}