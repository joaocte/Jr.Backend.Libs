using Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository;
using Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository.Extensions;
using Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.EntityFramework.QueryRepository
{
    public class QueryRepository<T> : IQueryRepository<T> where T : class

    {
        private readonly DbContext _dbContext;

        public QueryRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> ExistsAsync(CancellationToken cancellationToken = default)
        {
            return ExistsAsync(null, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition == null)
            {
                return await query.AnyAsync(cancellationToken);
            }

            bool isExists = await query.AnyAsync(condition, cancellationToken).ConfigureAwait(false);
            return isExists;
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
        {
            return GetAsync(condition, null, false, cancellationToken);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> condition, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            return GetAsync(condition, null, asNoTracking, cancellationToken);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> condition, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, CancellationToken cancellationToken = default)
        {
            return GetAsync(condition, includes, false, cancellationToken);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> condition, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<T> GetAsync(Specification<T> specification, CancellationToken cancellationToken = default)
        {
            return GetAsync(specification, false, cancellationToken);
        }

        public async Task<T> GetAsync(Specification<T> specification, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (specification != null)
            {
                query = query.GetSpecifiedQuery(specification);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TProjectedType> GetAsync<TProjectedType>(Expression<Func<T, bool>> condition, Expression<Func<T, TProjectedType>> selectExpression, CancellationToken cancellationToken = default)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.Select(selectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TProjectedType> GetAsync<TProjectedType>(Specification<T> specification, Expression<Func<T, TProjectedType>> selectExpression, CancellationToken cancellationToken = default)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<T> query = _dbContext.Set<T>();

            if (specification != null)
            {
                query = query.GetSpecifiedQuery(specification);
            }

            return await query.Select(selectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<T> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return GetByIdAsync(id, false, cancellationToken);
        }

        public Task<T> GetByIdAsync(object id, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return GetByIdAsync(id, null, asNoTracking, cancellationToken);
        }

        public Task<T> GetByIdAsync(object id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return GetByIdAsync(id, includes, false, cancellationToken);
        }

        public async Task<T> GetByIdAsync(object id, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            IEntityType entityType = _dbContext.Model.FindEntityType(typeof(T));

            string primaryKeyName = entityType.FindPrimaryKey().Properties.Select(p => p.Name).FirstOrDefault();
            Type primaryKeyType = entityType.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();

            if (primaryKeyName == null || primaryKeyType == null)
            {
                throw new ArgumentException("Entity does not have any primary key defined", nameof(id));
            }

            object primayKeyValue = null;

            try
            {
                primayKeyValue = Convert.ChangeType(id, primaryKeyType, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new ArgumentException($"You can not assign a value of type {id.GetType()} to a property of type {primaryKeyType}");
            }

            ParameterExpression pe = Expression.Parameter(typeof(T), "entity");
            MemberExpression me = Expression.Property(pe, primaryKeyName);
            ConstantExpression constant = Expression.Constant(primayKeyValue, primaryKeyType);
            BinaryExpression body = Expression.Equal(me, constant);
            Expression<Func<T, bool>> expressionTree = Expression.Lambda<Func<T, bool>>(body, new[] { pe });

            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                query = includes(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            T enity = await query.FirstOrDefaultAsync(expressionTree, cancellationToken).ConfigureAwait(false);
            return enity;
        }

        public async Task<TProjectedType> GetByIdAsync<TProjectedType>(object id, Expression<Func<T, TProjectedType>> selectExpression, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IEntityType entityType = _dbContext.Model.FindEntityType(typeof(T));

            string primaryKeyName = entityType.FindPrimaryKey().Properties.Select(p => p.Name).FirstOrDefault();
            Type primaryKeyType = entityType.FindPrimaryKey().Properties.Select(p => p.ClrType).FirstOrDefault();

            if (primaryKeyName == null || primaryKeyType == null)
            {
                throw new ArgumentException("Entity does not have any primary key defined", nameof(id));
            }

            object primayKeyValue = null;

            try
            {
                primayKeyValue = Convert.ChangeType(id, primaryKeyType, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                throw new ArgumentException($"You can not assign a value of type {id.GetType()} to a property of type {primaryKeyType}");
            }

            ParameterExpression pe = Expression.Parameter(typeof(T), "entity");
            MemberExpression me = Expression.Property(pe, primaryKeyName);
            ConstantExpression constant = Expression.Constant(primayKeyValue, primaryKeyType);
            BinaryExpression body = Expression.Equal(me, constant);
            Expression<Func<T, bool>> expressionTree = Expression.Lambda<Func<T, bool>>(body, new[] { pe });

            IQueryable<T> query = _dbContext.Set<T>();

            return await query.Where(expressionTree).Select(selectExpression)
                .FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync(CancellationToken cancellationToken = default)
        {
            int count = await _dbContext.Set<T>().CountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.CountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<int> GetCountAsync(IEnumerable<Expression<Func<T, bool>>> conditions, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (conditions != null)
            {
                foreach (Expression<Func<T, bool>> expression in conditions)
                {
                    query = query.Where(expression);
                }
            }

            return await query.CountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T1>> GetFromRawSqlAsync<T1>(string sql, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            IEnumerable<object> parameters = new List<object>();

            return await _dbContext.GetFromQueryAsync<T1>(sql, parameters, cancellationToken);
        }

        public async Task<IEnumerable<T1>> GetFromRawSqlAsync<T1>(string sql, object parameter, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<object> parameters = new List<object>() { parameter };
            return await _dbContext.GetFromQueryAsync<T1>(sql, parameters, cancellationToken);
        }

        public async Task<IEnumerable<T1>> GetFromRawSqlAsync<T1>(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            return await _dbContext.GetFromQueryAsync<T1>(sql, parameters, cancellationToken);
        }

        public Task<IEnumerable<T>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return GetListAsync(false, cancellationToken);
        }

        public Task<IEnumerable<T>> GetListAsync(bool asNoTracking, CancellationToken cancellationToken = default)
        {
            Func<IQueryable<T>, IIncludableQueryable<T, object>> nullValue = null;
            return GetListAsync(nullValue, asNoTracking, cancellationToken);
        }

        public Task<IEnumerable<T>> GetListAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, CancellationToken cancellationToken = default)
        {
            return GetListAsync(includes, false, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetListAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (includes != null)
            {
                query = includes(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
        {
            return GetListAsync(condition, false, cancellationToken);
        }

        public Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> condition, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            return GetListAsync(condition, null, asNoTracking, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> condition, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            if (includes != null)
            {
                query = includes(query);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public Task<IEnumerable<T>> GetListAsync(Specification<T> specification, CancellationToken cancellationToken = default)
        {
            return GetListAsync(specification, false, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetListAsync(Specification<T> specification, bool asNoTracking, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (specification != null)
            {
                query = query.GetSpecifiedQuery(specification);
            }

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TProjectedType>> GetListAsync<TProjectedType>(Expression<Func<T, TProjectedType>> selectExpression, CancellationToken cancellationToken = default)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            List<TProjectedType> entities = await _dbContext.Set<T>()
                .Select(selectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);

            return entities;
        }

        public async Task<IEnumerable<TProjectedType>> GetListAsync<TProjectedType>(Expression<Func<T, bool>> condition, Expression<Func<T, TProjectedType>> selectExpression, CancellationToken cancellationToken = default)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            List<TProjectedType> projectedEntites = await query.Select(selectExpression)
                .ToListAsync(cancellationToken).ConfigureAwait(false);

            return projectedEntites;
        }

        public async Task<IEnumerable<TProjectedType>> GetListAsync<TProjectedType>(Specification<T> specification, Expression<Func<T, TProjectedType>> selectExpression, CancellationToken cancellationToken = default)
        {
            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<T> query = _dbContext.Set<T>();

            if (specification != null)
            {
                query = query.GetSpecifiedQuery(specification);
            }

            return await query.Select(selectExpression)
                .ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<PaginatedList<T>> GetListAsync(PaginationSpecification<T> specification, CancellationToken cancellationToken = default)
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            PaginatedList<T> paginatedList = await _dbContext.Set<T>().ToPaginatedListAsync(specification, cancellationToken);
            return paginatedList;
        }

        public async Task<PaginatedList<TProjectedType>> GetListAsync<TProjectedType>(PaginationSpecification<T> specification, Expression<Func<T, TProjectedType>> selectExpression, CancellationToken cancellationToken = default) where TProjectedType : class
        {
            if (specification == null)
            {
                throw new ArgumentNullException(nameof(specification));
            }

            if (selectExpression == null)
            {
                throw new ArgumentNullException(nameof(selectExpression));
            }

            IQueryable<T> query = _dbContext.Set<T>().GetSpecifiedQuery((SpecificationBase<T>)specification);

            PaginatedList<TProjectedType> paginatedList = await query.Select(selectExpression)
                .ToPaginatedListAsync(specification.PageIndex, specification.PageSize, cancellationToken);
            return paginatedList;
        }

        public async Task<long> GetLongCountAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<T>().LongCountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<long> GetLongCountAsync(Expression<Func<T, bool>> condition, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.LongCountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<long> GetLongCountAsync(IEnumerable<Expression<Func<T, bool>>> conditions, CancellationToken cancellationToken = default)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (conditions != null)
            {
                foreach (Expression<Func<T, bool>> expression in conditions)
                {
                    query = query.Where(expression);
                }
            }

            return await query.LongCountAsync(cancellationToken).ConfigureAwait(false);
        }

        public IQueryable<T> GetQueryable()
        {
            return _dbContext.Set<T>();
        }
    }
}