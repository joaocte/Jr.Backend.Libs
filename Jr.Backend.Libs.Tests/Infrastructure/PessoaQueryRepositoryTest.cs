using Jr.Backend.Libs.Tests.TestObjjects.Domain;
using Jr.Backend.Libs.Tests.TestObjjects.Infra;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Xunit;

namespace Jr.Backend.Libs.Tests.Infrastructure
{
    public class PessoaQueryRepositoryTest : InMemoryPessoaTest
    {
        private IPessoaQueryRepository pessoaQueryRepository;

        [Fact]
        public void WhenVerifyThatAPersonExistsByIdReturnTrue()
        {
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var PessoaDeveExisitir = pessoaQueryRepository.ExistsAsync(x => x.Cpf == "1").Result;

            Assert.True(PessoaDeveExisitir);
        }

        [Fact]
        public void WhenVerifyThatAPersonNotExistsByIdReturnFalse()
        {
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var PessoaNaoDeveExisitir = pessoaQueryRepository.ExistsAsync(x => x.Cpf == "2").Result;

            Assert.False(PessoaNaoDeveExisitir);
        }

        [Fact]
        public void WhenVerifyThatAPersonWithoutParametersExistsReturnTrue()
        {
            Pessoa pessoa = new Pessoa { Cpf = "1", Nome = "Nome" };
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var PessoaDeveExisitir = pessoaQueryRepository.ExistsAsync().Result;

            Assert.True(PessoaDeveExisitir);
        }
    }
}