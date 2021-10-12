using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWorkingHours.Data.DataAccess;
using MyWorkingHours.Data.Models;
using MyWorkingHours.Data.Repository.Contracts;

namespace MyWorkingHours.Data.Repository.Implementations
{
    public class StatusTimeStampRepository : IStatusTimeStampRepository
    {
        private readonly SqliteDbContext _dbContext;

        public StatusTimeStampRepository(SqliteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IList<StatusTimeStamp>> FindAllAsync()
        {
            var result = await _dbContext.StatusTimeStamps.ToListAsync();
            return result;
        }

        /// <inheritdoc />
        public async Task<StatusTimeStamp> FindByIdAsync(object[] keys)
        {
            var result = await _dbContext.StatusTimeStamps.FindAsync(keys);
            return result;
        }

        /// <inheritdoc />
        public async Task<StatusTimeStamp> FindByIdAsync(object key)
        {
            var result = await _dbContext.StatusTimeStamps.FindAsync(key);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CreateAsync(StatusTimeStamp entity)
        {
            await _dbContext.StatusTimeStamps.AddAsync(entity);
            return await SaveAsync();
        }

        /// <inheritdoc />
        public async Task<bool> IsExistsAsync(int key)
        {
            var result = await _dbContext.StatusTimeStamps.AnyAsync(c => c.Id == key);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(StatusTimeStamp entity)
        {
            await Task.Run(() => _dbContext.StatusTimeStamps.Update(entity));
            return await SaveAsync();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(StatusTimeStamp entity)
        {
            await Task.Run(() => _dbContext.StatusTimeStamps.Remove(entity));
            return await SaveAsync();
        }

        /// <inheritdoc />
        public async Task<bool> SaveAsync()
        {
            var changes = await _dbContext.SaveChangesAsync();
            return changes > 0;
        }
    }
}