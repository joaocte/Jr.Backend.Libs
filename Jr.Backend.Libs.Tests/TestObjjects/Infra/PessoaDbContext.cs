using Jr.Backend.Libs.Tests.TestObjjects.Domain;
using Microsoft.EntityFrameworkCore;

namespace Jr.Backend.Libs.Tests.TestObjjects.Infra
{
    public class PessoaDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pessoa>(
                b =>
                {
                    b.Property("Cpf");
                    b.HasKey("Cpf");

                    b.Property("Nome");
                });
        }

        public PessoaDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}