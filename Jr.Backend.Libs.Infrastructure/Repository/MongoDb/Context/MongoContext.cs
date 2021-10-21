using Jr.Backend.Libs.Infrastructure.Abstractions.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> commands;

        private readonly IConfiguration configuration;

        /// <inheritdoc/>
        public MongoContext(IConfiguration configuration)
        {
            this.configuration = configuration;

            commands = new List<Func<Task>>();
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

            using (Session = await MongoClient.StartSessionAsync().ConfigureAwait(false))
            {
                Session.StartTransaction();

                var commandTasks = commands.Select(c => c());

                await Task.WhenAll(commandTasks).ConfigureAwait(false);

                await Session.CommitTransactionAsync().ConfigureAwait(false);
            }

            return commands.Count;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }

            MongoClient = new MongoClient(configuration["MongoSettings:Connection"]);

            Database = MongoClient.GetDatabase(configuration["MongoSettings:DatabaseName"]);
        }

        /// <inheritdoc/>
        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        /// <inheritdoc/>
        public async Task AddCommand(Func<Task> func) => await Task.Run(() => commands.Add(func)).ConfigureAwait(false);
    }
}