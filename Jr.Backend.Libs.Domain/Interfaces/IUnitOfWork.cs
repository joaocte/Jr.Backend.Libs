using System;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}