using System;
using System.IO;

namespace MyWorkingHours.Common
{
    public static class ApplicationDirectory
    {
        private const string AppPath = @"%LocalAppData%";
        private const string FolderName = "MyWorkingHours";

        /// <summary>
        ///     Get app directory, create if not exists
        /// </summary>
        /// <returns>Application path. AppData/Local/MyWorkingHours</returns>
        public static string GetApplicationDirectory()
        {
            var localAppData = Environment.ExpandEnvironmentVariables(AppPath);
            var dir = Path.Combine(localAppData, FolderName);

            var exists = Directory.Exists(dir);
            if (!exists) Directory.CreateDirectory(dir);

            return dir;
        }
    }
}