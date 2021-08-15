using Jr.Backend.Libs.Infrastructure.Repository.Interfaces;
using Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Interfaces;
using MongoDB.Driver;
using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.Repository.MongoDb
{
    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly IMongoContext _context;
        protected IMongoCollection<TEntity> _dbSet;

        /// <inheritdoc/>
        protected MongoRepository(IMongoContext context, string collectionName)
        {
            this._context = context;

            _dbSet = this._context.GetCollection<TEntity>(collectionName);
        }

        /// <inheritdoc/>
        public virtual async Task AddAsync(TEntity obj)
        {
            await _context.AddCommand(() => _dbSet.InsertOneAsync(obj)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> GetByIdAsync(string id)
        {
            var data = await _dbSet
                .FindAsync(Builders<TEntity>.Filter.Eq("_id", id))
                .ConfigureAwait(false);

            return data.SingleOrDefault();
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var all = await _dbSet.
                FindAsync(Builders<TEntity>.Filter.Empty)
                .ConfigureAwait(false);
            return await all.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TEntity obj)
        {
            await _context.AddCommand(() => _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task RemoveAsync(string id)
        {
            await _context.AddCommand(() => _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id))).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _dbSet = null;
        }

        /// <inheritdoc/>
        public void Dispose() => Dispose(true);
    }
}