using Microsoft.EntityFrameworkCore;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public class InMemoryPessoaTest : PessoaEFBaseTest
    {
        protected InMemoryPessoaTest() : base(new DbContextOptionsBuilder<PessoaDbContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options)
        {
        }
    }
}