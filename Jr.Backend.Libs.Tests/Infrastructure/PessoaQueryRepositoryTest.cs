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
        public void Should_Return_True_When_ID_Exists()
        {
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var PessoaDeveExisitir = pessoaQueryRepository.ExistsAsync(x => x.Cpf == "1").Result;

            Assert.True(PessoaDeveExisitir);
        }

        [Fact]
        public void Should_Return_False_When_ID_No_Exists()
        {
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var PessoaNaoDeveExisitir = pessoaQueryRepository.ExistsAsync(x => x.Cpf == "2").Result;

            Assert.False(PessoaNaoDeveExisitir);
        }

        [Fact]
        public void Should_Return_True_When_Any_ID_Exists()
        {
            Pessoa pessoa = new Pessoa { Cpf = "1", Nome = "Nome" };
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var PessoaDeveExisitir = pessoaQueryRepository.ExistsAsync().Result;

            Assert.True(PessoaDeveExisitir);
        }

        [Fact]
        public void Should_Return_Person_When_Person_Exists()
        {
            Pessoa pessoa = new Pessoa { Cpf = "1", Nome = "Nome" };
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var retorno = pessoaQueryRepository.GetAsync(x => x.Cpf == pessoa.Cpf).Result;

            Assert.NotNull(retorno);
            Assert.Equal(pessoa.Cpf, retorno.Cpf);
            Assert.Equal(pessoa.Nome, retorno.Nome);
        }

        [Fact]
        public void Should_No_Return_Person_When_Person_No_Exists()
        {
            Pessoa pessoa = new Pessoa { Cpf = "2", Nome = "Nome" };
            using var context = new PessoaDbContext(ContextOptions);
            pessoaQueryRepository = new PessoaQueryRepository(context);
            var retorno = pessoaQueryRepository.GetAsync(x => x.Cpf == pessoa.Cpf).Result;

            Assert.Null(retorno);
        }
    }
}