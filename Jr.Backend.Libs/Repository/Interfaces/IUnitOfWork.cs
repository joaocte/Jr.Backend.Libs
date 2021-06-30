using System;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}