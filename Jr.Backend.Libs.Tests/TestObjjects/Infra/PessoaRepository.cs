using Jr.Backend.Libs.Infrastructure.Repository.MongoDb;
using Jr.Backend.Libs.Infrastructure.Repository.MongoDb.Interfaces;
using Jr.Backend.Libs.Tests.Domain;
using Jr.Backend.Libs.Tests.TestObjjects.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public class PessoaRepository : MongoRepository<Pessoa>, IPessoaRepository
    {
        public PessoaRepository(IMongoContext context, string collectionName) : base(context, collectionName)
        {
        }
    }
}