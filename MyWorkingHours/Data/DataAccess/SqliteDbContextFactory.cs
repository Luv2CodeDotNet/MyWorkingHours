using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyWorkingHours.Common;

namespace MyWorkingHours.Data.DataAccess
{
    public class SqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
    {
        public SqliteDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<SqliteDbContext>();
            options.UseSqlite(ApplicationDirectory.GetApplicationFilePath(SpecialAppFile.SqliteDbFile));
            return new SqliteDbContext(options.Options);
        }
    }
}