using Microsoft.EntityFrameworkCore;
using MyWorkingHours.Data.Models;

namespace MyWorkingHours.Data.DataAccess
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<StatusTimeStamp> StatusTimeStamps { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<SessionSwitch> SessionSwitches { get; set; }

        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }
    }
}