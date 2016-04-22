using System;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Configuration;
using System.Data.SqlClient;

namespace SerwisAudiometryczny.Models
{
    // Backupy jak narazie wykonywane do ścieżki C:\\nazwa.bak
    public class MyBackupAndRestore
    {
        private string databaseName = "SerwisAudiometryczny"; // nazwa bazy danych

        /// <summary>
        /// Właściwość pobierająca nazwę serwera(Data Source)
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
        /// Metoda wykonująca pełną kopię zapasową do pliku
        /// o nazwie składającej się z aktualnej daty oraz nazwy bazy danych ('yyyy-MM-dd_nazwaBazy.bak')
        /// </summary>
        public void BackupDatabase()
        {
            Backup sqlBackup = new Backup()
            {
                Action = BackupActionType.Database,
                BackupSetDescription = $"Kopia zapasowa '{databaseName}' wykonana {DateTime.Now.ToShortDateString()}",
                BackupSetName = "Pełna kopia zapasowa",
                Database = databaseName
            };
            ServerConnection connection = new ServerConnection(serverName);
            Server sqlServer = new Server(connection);
            string filename = DateTime.Now.ToString("yyyy-MM-dd") + "_" + databaseName;
            string dbDir = $"C:\\{filename}.bak";
            BackupDeviceItem deviceItem = new BackupDeviceItem(dbDir, DeviceType.File);

            sqlServer.ConnectionContext.StatementTimeout = 60;
            sqlBackup.Devices.Add(deviceItem);
            sqlBackup.Incremental = false;
            sqlBackup.Initialize = true;

            try
            {
                sqlBackup.SqlBackup(sqlServer);
            }
            catch (Exception error)
            {
                // Tutaj będzie kod to wyświetlenia komunikatu o błędzie użytkownikowi
                throw new ApplicationException(error.Message);
            }
            sqlBackup.Devices.Remove(deviceItem);
            connection.Disconnect();
        }

        /// <summary>
        /// Metoda wykonująca pełną kopię zapasową do pliku
        /// o nazwie podanej w parametrze metody.
        /// </summary>
        /// <param name="backupFileName">Nazwa kopii zapasowej</param>
        public void BackupDatabase(string backupFileName)
        {
            Backup sqlBackup = new Backup()
            {
                Action = BackupActionType.Database,
                BackupSetDescription = $"Kopia zapasowa '{databaseName}' wykonana {DateTime.Now.ToShortDateString()}",
                BackupSetName = "Pełna kopia zapasowa",
                Database = databaseName
            };
            ServerConnection connection = new ServerConnection(serverName);
            Server sqlServer = new Server(connection);
            string dbDir = $"C:\\{backupFileName}.bak";
            BackupDeviceItem deviceItem = new BackupDeviceItem(dbDir, DeviceType.File);

            sqlServer.ConnectionContext.StatementTimeout = 60;
            sqlBackup.Devices.Add(deviceItem);
            sqlBackup.Incremental = false;
            sqlBackup.Initialize = true;

            try
            {
                sqlBackup.SqlBackup(sqlServer);
            }
            catch (Exception error)
            {
                // Tutaj będzie kod to wyświetlenia komunikatu o błędzie użytkownikowi
                throw new ApplicationException(error.Message);
            }
            sqlBackup.Devices.Remove(deviceItem);
            connection.Disconnect();
        }

        /// <summary>
        /// Metoda wykonująca przywracanie bazy danych z kopii zapasowej.
        /// </summary>
        /// <param name="backupFileName">Nazwa kopii zapasowej</param>
        public void RestoreDatabase(string backupFileName)
        {
            Restore sqlRestore = new Restore()
            {
                Action = RestoreActionType.Database,
                Database = databaseName,
                ReplaceDatabase = true
            };
            ServerConnection connection = new ServerConnection(serverName);
            Server sqlServer = new Server(connection);
            BackupDeviceItem deviceItem = new BackupDeviceItem($"C:\\{backupFileName}.bak", DeviceType.File);

            sqlServer.ConnectionContext.StatementTimeout = 60;
            sqlRestore.Devices.Add(deviceItem);
            sqlServer.KillAllProcesses(databaseName); //trzeba zakończyć wszystkie połączenia z bazą, aby ją przywrócić
            try
            {
                sqlRestore.SqlRestore(sqlServer);
            }
            catch (Exception error)
            {
                // Tutaj będzie kod to wyświetlenia komunikatu o błędzie użytkownikowi
                throw new ApplicationException(error.Message);
            }
            sqlRestore.Devices.Remove(deviceItem);
            connection.Disconnect();
        }
    }

}