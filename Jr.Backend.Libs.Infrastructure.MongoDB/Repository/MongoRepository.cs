using Jr.Backend.Libs.Domain.Abstractions.Interfaces.Repository;
using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using MongoDB.Driver;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.MongoDB.Repository
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
        public virtual async Task AddAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            await _context.AddCommand(() => _dbSet.InsertOneAsync(obj, null, cancellationToken)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            var data = await _dbSet
                .FindAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()), null, cancellationToken)
                .ConfigureAwait(false);

            return await data.SingleOrDefaultAsync(cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _dbSet.AsQueryable().Where(condition).FirstNonDefault());
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _dbSet.AsQueryable().Any(condition));
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _dbSet.
                FindAsync(Builders<TEntity>.Filter.Empty, null, cancellationToken)
                .ConfigureAwait(false);
            return await all.ToListAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task UpdateAsync(TEntity obj, CancellationToken cancellationToken = default)
        {
            await _context.AddCommand(() => _dbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq("_id", obj.GetId()), obj)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task RemoveAsync(object id, CancellationToken cancellationToken = default)
        {
            await _context.AddCommand(() => _dbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()), cancellationToken)).ConfigureAwait(false);
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

        public async Task<bool> ExistsAsync(object id, CancellationToken cancellationToken = default)
        {
            var data = await _dbSet
                .FindAsync(Builders<TEntity>.Filter.Eq("_id", id.ToString()), null, cancellationToken)
                .ConfigureAwait(false);

            return await data.AnyAsync(cancellationToken);
        }

        public async Task<IQueryable<TEntity>> GetAllAsQueryableAsync(CancellationToken cancellationToken = default)
        {
            return await Task.Run(() => _dbSet.AsQueryable());
        }
    }
}