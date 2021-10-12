using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyWorkingHours.Data.DataAccess;
using MyWorkingHours.Data.Models;
using MyWorkingHours.Data.Repository.Contracts;

namespace MyWorkingHours.Data.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly SqliteDbContext _dbContext;

        public UserRepository(SqliteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task<IList<User>> FindAllAsync()
        {
            var result = await _dbContext.Users.ToListAsync();
            return result;
        }

        /// <inheritdoc />
        public async Task<User> FindByIdAsync(object[] keys)
        {
            var result = await _dbContext.Users.FindAsync(keys);
            return result;
        }

        /// <inheritdoc />
        public async Task<User> FindByIdAsync(object key)
        {
            var result = await _dbContext.Users.FindAsync(key);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> CreateAsync(User entity)
        {
            await _dbContext.Users.AddAsync(entity);
            return await SaveAsync();
        }

        /// <inheritdoc />
        public async Task<bool> IsExistsAsync(int key)
        {
            var result = await _dbContext.Users.AnyAsync(c => c.Id == key);
            return result;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateAsync(User entity)
        {
            await Task.Run(() => _dbContext.Users.Update(entity));
            return await SaveAsync();
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(User entity)
        {
            await Task.Run(() => _dbContext.Users.Remove(entity));
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