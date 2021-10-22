using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Domain.Abstractions.Interfaces.Repository
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task AddAsync(T obj, CancellationToken cancellationToken = default);

        Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

        Task UpdateAsync(T obj, CancellationToken cancellationToken = default);

        Task RemoveAsync(object id, CancellationToken cancellationToken = default);
    }
}