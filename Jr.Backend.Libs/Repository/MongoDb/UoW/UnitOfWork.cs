using Jr.Backend.Libs.Infrastructure.Repository.Interfaces;
using Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Interfaces;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.Repository.MongoDb.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;
        private bool disposedValue;

        /// <inheritdoc/>
        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<bool> CommitAsync()
        {
            var changeAmount = await _context
                .SaveChanges()
                .ConfigureAwait(false);

            return changeAmount > 0;
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context?.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <inheritdoc/>
        public void Dispose() => Dispose(disposing: true);
    }
}