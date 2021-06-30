using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Interfaces
{
    public interface IMongoContext : IDisposable
    {/// <summary>
     /// Add Command to execute.
     /// </summary>
     /// <param name="func">Command to execute.</param>
        void AddCommand(Func<Task> func);

        /// <summary>
        /// Salve changes in database using unitofwork.
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChanges();

        /// <summary>
        /// Get the collection name.
        /// </summary>
        /// <typeparam name="T">Type the collection</typeparam>
        /// <param name="name">Name from collection</param>
        /// <returns></returns>
        IMongoCollection<T> GetCollection<T>(string name);
    }
}