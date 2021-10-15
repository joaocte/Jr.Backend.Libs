using System;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Domain.Interfaces.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> CommitAsync();
    }
}