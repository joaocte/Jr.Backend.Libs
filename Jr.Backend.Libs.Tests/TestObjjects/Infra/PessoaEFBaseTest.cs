using Jr.Backend.Libs.Tests.TestObjjects.Domain;
using Microsoft.EntityFrameworkCore;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public abstract class PessoaEFBaseTest
    {
        protected readonly DbContextOptions<PessoaDbContext> ContextOptions;

        protected PessoaEFBaseTest(DbContextOptions<PessoaDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        private void Seed()
        {
            using var context = new PessoaDbContext(ContextOptions);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var one = new Pessoa { Cpf = "1", Nome = "Nome" };

            context.AddRange(one);

            context.SaveChanges();
        }
    }
}