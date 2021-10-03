using System.IO;
using MyWorkingHours.Common;

namespace MyWorkingHours.Data.DataAccess
{
    public static class SqliteConStringBuilder
    {
        private const string DbName = "db.sqlite";
        private const string DbFileName = "Filename=";
        private const string DbFolderName = "Database";

        /// <summary>
        ///     Generate Sqlite connection string.
        /// </summary>
        /// <returns>Returns connection string in AppData/Local/MyWorkingHours/db.sqlite. Db name: db.sqlite</returns>
        /// <exception cref="DirectoryNotFoundException">Throws DirectoryNotFoundException if directory is not found</exception>
        public static string GetSqliteConnString()
        {
            var subDir = ApplicationDirectory.GetApplicationSubDirectory(DbFolderName);
            var dbDir = Path.Combine(subDir, DbName);

            var exists = Directory.Exists(subDir);
            if (!exists) throw new DirectoryNotFoundException();

            return string.Concat(DbFileName, dbDir);
        }
    }
}