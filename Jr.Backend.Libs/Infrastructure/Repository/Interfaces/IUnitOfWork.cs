using System;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// makes changes to the database.
        /// </summary>
        /// <returns>True when successful, otherwise false.</returns>
        Task<bool> CommitAsync();
    }
}