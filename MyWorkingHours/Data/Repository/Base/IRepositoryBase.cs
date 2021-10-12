using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWorkingHours.Data.Repository.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        ///     Find all entities from table.
        /// </summary>
        /// <returns>List of entities of type T.</returns>
        Task<IList<T>> FindAllAsync();

        /// <summary>
        ///     Find entity by composite primary key.
        /// </summary>
        /// <param name="keys">Primary key values as array</param>
        /// <returns>Entity of type T.</returns>
        Task<T> FindByIdAsync(object[] keys);

        /// <summary>
        ///     Find entity by id.
        /// </summary>
        /// <param name="key">Primary key.</param>
        /// <returns>Entity of type T.</returns>
        Task<T> FindByIdAsync(object key);

        /// <summary>
        ///     Save new entity to database.
        /// </summary>
        /// <param name="entity">Entity of type T which represents a data record in the database.</param>
        /// <returns>True if the record has been saved, otherwise false.</returns>
        Task<bool> CreateAsync(T entity);

        /// <summary>
        ///     Check if entity exists. Find by primary key.
        /// </summary>
        /// <param name="key">Primary key.</param>
        /// <returns>True if record exists, otherwise false.</returns>
        Task<bool> IsExistsAsync(int key);

        /// <summary>
        ///     Update entity.
        /// </summary>
        /// <param name="entity">Entity of type T to be updated in the database.</param>
        /// <returns>True if updated, otherwise false.</returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        ///     Delete entity.
        /// </summary>
        /// <param name="entity">Entity of type T to be deleted from the database.</param>
        /// <returns>True if deleted, otherwise false.</returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        ///     Save changes to database.
        /// </summary>
        /// <returns>True if saved, otherwise false.</returns>
        Task<bool> SaveAsync();
    }
}