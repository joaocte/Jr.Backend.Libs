using Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository.Interfaces;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository
{
    public interface IQueryRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// This method checks whether the database table contains any record.
        /// and returns <see cref="Task"/> of <see cref="bool"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="bool"/>.</returns>
        Task<bool> ExistsAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes a predicate based on which existence of the entity will be determined
        /// and returns <see cref="Task"/> of <see cref="bool"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition based on which the existence will checked.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="bool"/>.</returns>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> condition,
            bool asNoTracking,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="includes">Navigation properties to be loaded.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="includes">Navigation properties to be loaded.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> GetAsync(
            Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an <see cref="object"/> of <see cref="Specification{T}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an <see cref="object"/> of <see cref="Specification{T}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetAsync(Specification<TEntity> specification, bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <typeparamref name="TEntity"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="condition">The conditon on which entity will be returned.</param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Retuns <typeparamref name="TProjectedType"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is <see langword="null"/>.</exception>
        Task<TProjectedType> GetAsync<TProjectedType>(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an <see cref="object"/> of <see cref="Specification{T}"/> and a <see cref="System.Linq"/> select  query
        /// and returns <typeparamref name="TProjectedType"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The type of the projected entity.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select  query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Retuns <typeparamref name="TProjectedType"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is <see langword="null"/>.</exception>
        Task<TProjectedType> GetAsync<TProjectedType>(
            Specification<TEntity> specification,
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(object id, bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="includes">The navigation properties to be loaded.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(
            object id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the entity
        /// if found otherwise <see langword="null"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="includes">The navigation properties to be loaded.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<TEntity> GetByIdAsync(
            object id,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="id"/> which is the primary key value of the entity and returns the specified projected entity
        /// if found otherwise null.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="id">The primary key value of the entity.</param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <typeparamref name="TProjectedType"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is <see langword="null"/>.</exception>
        Task<TProjectedType> GetByIdAsync<TProjectedType>(
            object id,
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns all count in <see cref="int"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="int"/>.</returns>
        Task<int> GetCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes one or more <em>predicates</em> and returns the count in <see cref="int"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition based on which count will be done.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="int"/>.</returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes one or more <em>predicates</em> and returns the count in <see cref="int"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="conditions">The conditions based on which count will be done.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="int"/>.</returns>
        Task<int> GetCountAsync(IEnumerable<Expression<Func<TEntity, bool>>> conditions, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="sql"/> string as parameter and returns the result of the provided sql.
        /// </summary>
        /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
        /// <param name="sql">The sql query string.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
        Task<IEnumerable<T>> GetFromRawSqlAsync<T>(string sql, CancellationToken cancellationToken = default);

        // Context level members
        /// <summary>
        /// This method takes <paramref name="sql"/> string and the value of <paramref name="parameter"/> mentioned in the sql query as parameters
        /// and returns the result of the provided sql.
        /// </summary>
        /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
        /// <param name="sql">The sql query string.</param>
        /// <param name="parameter">The value of the paramter mention in the sql query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
        Task<IEnumerable<T>> GetFromRawSqlAsync<T>(string sql, object parameter, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <paramref name="sql"/> string and values of the <paramref name="parameters"/> mentioned in the sql query as parameters
        /// and returns the result of the provided sql.
        /// </summary>
        /// <typeparam name="T">The <see langword="type"/> to which the result will be mapped.</typeparam>
        /// <param name="sql">The sql query string.</param>
        /// <param name="parameters">The values of the parameters mentioned in the sql query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="sql"/> is <see langword="null"/>.</exception>
        Task<IEnumerable<T>> GetFromRawSqlAsync<T>(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns <see cref="IEnumerable{T}"/> without any filter. Call only when you want to pull all the data from the source.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="IEnumerable{T}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns <see cref="IEnumerable{T}"/> without any filter. Call only when you want to pull all the data from the source.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="IEnumerable{T}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns <see cref="IEnumerable{T}"/> without any filter. Call only when you want to pull all the data from the source.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="includes">Navigation properties to be loaded.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="IEnumerable{T}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns <see cref="IEnumerable{T}"/> without any filter. Call only when you want to pull all the data from the source.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="includes">Navigation properties to be loaded.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task"/> of <see cref="IEnumerable{T}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes a <see cref="Expression{Func}"/> as parameter and returns <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition on which entity list will be returned.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="IEnumerable{T}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes a <see cref="Expression{Func}"/> as parameter and returns <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition on which entity list will be returned.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="IEnumerable{TEntity}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> condition,
            bool asNoTracking,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes a <see cref="Expression{Func}"/> as parameter and returns <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition on which entity list will be returned.</param>
        /// <param name="includes">Navigation properties to be loaded.</param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="IEnumerable{TEntity}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>> condition,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes,
            bool asNoTracking = false,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an object of <see cref="Specification{TEntity}"/> as parameter and returns <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> <see cref="object"/> which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="IEnumerable{TEntity}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(Specification<TEntity> specification, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an object of <see cref="Specification{TEntity}"/> as parameter and returns <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> <see cref="object"/> which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="asNoTracking">A <see cref="bool"/> value which determines whether the return entity will be tracked by
        /// EF Core context or not. Defualt value is false i.e trackig is enabled by default.
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="IEnumerable{TEntity}"/>.</returns>
        Task<IEnumerable<TEntity>> GetListAsync(Specification<TEntity> specification, bool asNoTracking, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns <see cref="IEnumerable{TProjectedType}"/> without any filter.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The type to which <typeparamref name="TEntity"/> will be projected.</typeparam>
        /// <param name="selectExpression">A <b>LINQ</b> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        Task<IEnumerable<TProjectedType>> GetListAsync<TProjectedType>(
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> as parameter and returns <see cref="IEnumerable{TProjectedType}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="condition">The condition on which entity list will be returned.</param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is <see langword="null"/>.</exception>
        Task<IEnumerable<TProjectedType>> GetListAsync<TProjectedType>(
            Expression<Func<TEntity, bool>> condition,
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes an <see cref="object"/> of <see cref="Specification{T}"/> and <paramref name="selectExpression"/> as parameters and
        /// returns <see cref="List{TProjectedType}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="specification">A <see cref="Specification{TEntity}"/> object which contains all the conditions and criteria
        /// on which data will be returned.
        /// </param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Return <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is <see langword="null"/>.</exception>
        Task<IEnumerable<TProjectedType>> GetListAsync<TProjectedType>(
            Specification<TEntity> specification,
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns a <see cref="PaginatedList{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="specification">An object of <see cref="PaginationSpecification{T}"/>.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="PaginatedList{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="specification"/> is smaller than 1.</exception>
        Task<PaginatedList<TEntity>> GetListAsync(
            PaginationSpecification<TEntity> specification,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// This method returns a <see cref="PaginatedList{T}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <typeparam name="TProjectedType">The projected type.</typeparam>
        /// <param name="specification">An object of <see cref="PaginationSpecification{T}"/>.</param>
        /// <param name="selectExpression">The <see cref="System.Linq"/> select query.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Returns <see cref="Task{TResult}"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="specification"/> is smaller than 1.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="selectExpression"/> is smaller than 1.</exception>
        Task<PaginatedList<TProjectedType>> GetListAsync<TProjectedType>(
            PaginationSpecification<TEntity> specification,
            Expression<Func<TEntity, TProjectedType>> selectExpression,
            CancellationToken cancellationToken = default)
            where TProjectedType : class;

        /// <summary>
        /// This method returns all count in <see cref="long"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Retuns <see cref="Task"/> of <see cref="long"/>.</returns>
        Task<long> GetLongCountAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes one or more <em>predicates</em> and returns the count in <see cref="long"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="condition">The condition based on which count will be done.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Retuns <see cref="Task"/> of <see cref="long"/>.</returns>
        Task<long> GetLongCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);

        /// <summary>
        /// This method takes one or more <em>predicates</em> and returns the count in <see cref="long"/> type.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="conditions">The conditions based on which count will be done.</param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>Retuns <see cref="Task"/> of <see cref="long"/>.</returns>
        Task<long> GetLongCountAsync(IEnumerable<Expression<Func<TEntity, bool>>> conditions, CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets <see cref="IQueryable{T}"/> of the entity.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Returns <see cref="IQueryable{T}"/>.</returns>
        IQueryable<TEntity> GetQueryable();
    }
}