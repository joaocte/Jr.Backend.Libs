using MongoDB.Driver;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public abstract class InfrastructureTestBase
    {
        protected IMongoCollection<T> CreateMockCollection<T>()
        {
            var settings = new MongoCollectionSettings();
            var mockCollection = Substitute.For<IMongoCollection<T>>();
            mockCollection.DocumentSerializer.Returns(settings.SerializerRegistry.GetSerializer<T>());
            mockCollection.Settings.Returns(settings);
            return mockCollection;
        }
    }
}