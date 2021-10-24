using Jr.Backend.Libs.Infrastructure.EntityFramework.Abstractions.QueryRepository;
using Jr.Backend.Libs.Tests.TestObjjects.Domain;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public interface IPessoaQueryRepository : IQueryRepository<Pessoa>
    {
    }
}