using System;
using System.IO;

namespace MyWorkingHours.Common
{
    public static class ApplicationDirectory
    {
        private const string AppPath = @"%LocalAppData%";
        private const string AppFolderName = "Luv2Code/MyWorkingHours";
        private const string LogsFolderName = @"Logs";
        private const string LogFileName = @"log_.txt";

        /// <summary>
        ///     Get app directory, create if not exists
        /// </summary>
        /// <returns>Application path. AppData/Local/MyWorkingHours</returns>
        public static string GetApplicationDirectory()
        {
            var localAppData = Environment.ExpandEnvironmentVariables(AppPath);
            var dir = Path.Combine(localAppData, AppFolderName);

            var exists = Directory.Exists(dir);
            if (!exists) Directory.CreateDirectory(dir);

            return dir;
        }

        /// <summary>
        ///     Get app sub directory, create if not exists
        /// </summary>
        /// <param name="subDirectory"></param>
        /// <returns>Application sub directory path. AppData/Local/MyWorkingHours/SubDir</returns>
        public static string GetApplicationSubDirectory(string subDirectory)
        {
            var subDir = Path.Combine(GetApplicationDirectory(), subDirectory);

            var exists = Directory.Exists(subDir);
            if (!exists) Directory.CreateDirectory(subDir);

            return subDir;
        }

        public static string GetLogsDirectory()
        {
            return Path.Combine(GetApplicationDirectory(), LogsFolderName, LogFileName);
        }
    }
}