using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Jr.Backend.Libs.Infrastructure.MongoDB.Repository;
using Jr.Backend.Libs.Security.Abstractions.Entity;
using Jr.Backend.Libs.Security.Abstractions.Infrastructure.Interface;

namespace Jr.Backend.Libs.Security.Infrastructure.Repository.MongoDb
{
    public class TenantRepository : MongoRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(IMongoContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}