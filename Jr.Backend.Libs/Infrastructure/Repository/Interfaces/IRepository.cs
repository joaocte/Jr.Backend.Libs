using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Add object to a Table / Collection.
        /// </summary>
        /// <param name="obj">Object to be added. </param>
        /// <returns></returns>
        Task AddAsync(T obj);

        /// <summary>
        /// Return object from Table / Collection.
        /// </summary>
        /// <param name="id">Object id to be get. </param>
        /// <returns></returns>
        Task<T> GetByIdAsync(string id);

        /// <summary>
        /// Return all objects from Table / Collection.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Update the object to Table / Collection
        /// </summary>
        /// <param name="obj">Object to be Updated. </param>
        /// <returns></returns>
        Task UpdateAsync(T obj);

        /// <summary>
        /// Remove the specific object from table / collection.
        /// </summary>
        /// <param name="id">Object id to be removed. </param>
        /// <returns></returns>
        Task RemoveAsync(string id);
    }
}