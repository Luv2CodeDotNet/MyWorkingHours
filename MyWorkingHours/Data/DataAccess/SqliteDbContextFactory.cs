using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyWorkingHours.Data.DataAccess
{
    public class SqliteDbContextFactory : IDesignTimeDbContextFactory<SqliteDbContext>
    {
        public SqliteDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<SqliteDbContext>();
            options.UseSqlite(SqliteConStringBuilder.GetSqliteConnString());
            return new SqliteDbContext(options.Options);
        }
    }
}