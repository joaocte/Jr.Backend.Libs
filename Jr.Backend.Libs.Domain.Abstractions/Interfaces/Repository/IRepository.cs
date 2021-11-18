using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Domain.Abstractions.Interfaces.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Add <paramref name="obj"/> to the database.
        /// </summary>
        /// <param name="obj">Object to add a database.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AddAsync(T obj, CancellationToken cancellationToken = default);

        /// <summary>
        ///Returns the Object by Id if exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        ///TODO
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        /// <summary>
        ///TODO
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpdateAsync(T obj, CancellationToken cancellationToken = default);

        /// <summary>
        ///TODO
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(object id, CancellationToken cancellationToken = default);

        Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        ///TODO
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IQueryable<T>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default);
    }
}