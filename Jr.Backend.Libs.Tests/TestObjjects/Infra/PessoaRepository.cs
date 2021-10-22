using Jr.Backend.Libs.Infrastructure.MongoDB.Abstractions.Interfaces;
using Jr.Backend.Libs.Infrastructure.MongoDB.Repository;
using Jr.Backend.Libs.Tests.TestObjjects.Domain;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public class PessoaRepository : MongoRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(IMongoContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}