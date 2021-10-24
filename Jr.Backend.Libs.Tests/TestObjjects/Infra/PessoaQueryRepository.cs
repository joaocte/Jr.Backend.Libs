using Jr.Backend.Libs.Infrastructure.EntityFramework.QueryRepository;
using Jr.Backend.Libs.Tests.TestObjjects.Domain;
using Microsoft.EntityFrameworkCore;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public class PessoaQueryRepository : QueryRepository<Pessoa>, IPessoaQueryRepository
    {
        public PessoaQueryRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}