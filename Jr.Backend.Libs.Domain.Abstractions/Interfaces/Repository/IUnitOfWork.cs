using System;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Domain.Abstractions.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit transaction to save.
        /// </summary>
        /// <returns></returns>
        Task<bool> CommitAsync();
    }
}