using System;
using System.IO;

namespace MyWorkingHours.Common
{
    public static class ApplicationDirectory
    {
        /// <summary>
        ///     Path to Users\[username]\AppData\Local\
        /// </summary>
        private const string AppPath = "%LocalAppData%";

        /// <summary>
        ///     Path to application directory
        /// </summary>
        private const string AppFolderName = "Luv2Code/MyWorkingHours";

        /// <summary>
        ///     Path to logs folder
        /// </summary>
        private const string LogsFolderName = "Logs";

        /// <summary>
        ///     Path to sqlite folder
        /// </summary>
        private const string SqliteFolderName = "Database";

        /// <summary>
        ///     Logfile name: log_[date].txt
        /// </summary>
        private const string LogFileName = "log_.txt";

        /// <summary>
        ///     Sqlite Db file name: db.sqlite
        /// </summary>
        private const string SqliteDbFileName = "db.sqlite";

        /// <summary>
        ///     Get app directory, create if not exists
        /// </summary>
        /// <param name="specialAppDirectory">Enum for application directories</param>
        /// <param name="subFolderName">Optional parameter for the first sub directory level</param>
        /// <returns>Application path. AppData/Local/MyWorkingHours</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetApplicationDirectory(SpecialAppDirectory specialAppDirectory,
            string subFolderName = null!)
        {
            return specialAppDirectory switch
            {
                SpecialAppDirectory.Database => AppDirectorySqliteDatabase(),
                SpecialAppDirectory.Logs => AppDirectoryLogs(),
                SpecialAppDirectory.Root => AppDirectoryRoot(),
                SpecialAppDirectory.SubDirectory => AppSubDirectory(subFolderName),
                _ => throw new ArgumentOutOfRangeException(nameof(specialAppDirectory), specialAppDirectory, null)
            };
        }


        /// <summary>
        ///     Get specific file path
        /// </summary>
        /// <param name="specialAppFile">Enum for application files</param>
        /// <returns>Full path to specific file</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string GetApplicationFilePath(SpecialAppFile specialAppFile)
        {
            return specialAppFile switch
            {
                SpecialAppFile.LogsFile => GetLogFile(),
                SpecialAppFile.SqliteDbFile => GetSqliteDbFile(),
                _ => throw new ArgumentOutOfRangeException(nameof(specialAppFile), specialAppFile, null)
            };
        }

        /// <summary>
        ///     Application SubDirectory
        /// </summary>
        /// <param name="subFolderName"></param>
        /// <returns></returns>
        private static string AppSubDirectory(string subFolderName)
        {
            var subDir = Path.Combine(AppDirectoryRoot(), subFolderName);
            CreateDirIfNotExists(subDir);
            return subDir;
        }

        /// <summary>
        ///     Application root directory
        /// </summary>
        /// <returns></returns>
        private static string AppDirectoryRoot()
        {
            var localAppData = Environment.ExpandEnvironmentVariables(AppPath);
            var dir = Path.Combine(localAppData, AppFolderName);
            CreateDirIfNotExists(dir);
            return dir;
        }

        /// <summary>
        ///     Application Logs directory
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private static string AppDirectoryLogs()
        {
            return Path.Combine(AppDirectoryRoot(), LogsFolderName);
        }

        /// <summary>
        ///     Application Sqlite DB
        /// </summary>
        /// <returns></returns>
        private static string AppDirectorySqliteDatabase()
        {
            var subDir = Path.Combine(AppDirectoryRoot(), SqliteFolderName);
            CreateDirIfNotExists(subDir);
            return subDir;
        }

        /// <summary>
        ///     Check if directory exists, create if directory doesn't exists
        /// </summary>
        /// <param name="directoryPath">Valid System.IO path. For example: C:/MyApp/</param>
        private static void CreateDirIfNotExists(string directoryPath)
        {
            var exists = Directory.Exists(directoryPath);
            if (!exists) Directory.CreateDirectory(directoryPath);
        }

        /// <summary>
        ///     Get the logfile location and name
        /// </summary>
        /// <returns>Path to the logfile, logfile name is included</returns>
        private static string GetLogFile()
        {
            return Path.Combine(AppDirectoryLogs(), LogFileName);
        }

        /// <summary>
        ///     Get the logfile location and name
        /// </summary>
        /// <returns>Path to the logfile, logfile name is included</returns>
        private static string GetSqliteDbFile()
        {
            const string prefix = "FileName=";
            var path = Path.Combine(AppDirectorySqliteDatabase(), SqliteDbFileName);
            var connString = string.Concat(prefix, path);
            return connString;
        }
    }
}