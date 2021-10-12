using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWorkingHours.Data.DataAccess;
using MyWorkingHours.Data.Models;
using MyWorkingHours.Data.Repository.Contracts;

namespace MyWorkingHours.Data.Repository.Implementations
{
    public class SessionSwitchRepository : ISessionSwitchRepository
    {
        private readonly SqliteDbContext _dbContext;

        public SessionSwitchRepository(SqliteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IList<SessionSwitch>> FindAllAsync()
        {
            var result = await _dbContext.SessionSwitches.ToListAsync();
            return result;
        }

        /// <inheritdoc />
        public async Task<SessionSwitch> FindByIdAsync(object[] keys)
        {
            var result = await _dbContext.SessionSwitches.FindAsync(keys);
            return result;
        }

        /// <inheritdoc />
        public async Task<SessionSwitch> FindByIdAsync(object key)
        {
            var result = await _dbContext.SessionSwitches.FindAsync(key);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CreateAsync(SessionSwitch entity)
        {
            await _dbContext.SessionSwitches.AddAsync(entity);
            return await SaveAsync();
        }

        /// <inheritdoc />
        public async Task<bool> IsExistsAsync(int key)
        {
            var result = await _dbContext.SessionSwitches.AnyAsync(c => c.Id == key);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(SessionSwitch entity)
        {
            await Task.Run(() => _dbContext.SessionSwitches.Update(entity));
            return await SaveAsync();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(SessionSwitch entity)
        {
            await Task.Run(() => _dbContext.SessionSwitches.Remove(entity));
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