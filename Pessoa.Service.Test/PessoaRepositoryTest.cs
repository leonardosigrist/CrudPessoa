using Microsoft.EntityFrameworkCore;
using Pessoa.Repository;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pessoa.Domain.Test
{
    public class PessoaRepositoryTest
    {
        private readonly DbContextOptions<PessoaContext> dbContextOptions;

        public PessoaRepositoryTest()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=PessoaCrudTest;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<PessoaContext>();
            optionsBuilder.UseSqlServer(connectionString);

            this.dbContextOptions = optionsBuilder.Options;
        }

        [Fact]
        public void ValidaInsercaoDePessoa()
        {
            IPessoaRepository pessoaRepository = ObtemRepositorioDeTeste();
            Entity.Pessoa pessoa = new Entity.Pessoa
            {
                Nome = "José Almeida",
                Idade = 53
            };

            int idPessoaInserida = pessoaRepository.InserirAsync(pessoa).Result;
            Entity.Pessoa pessoaInserida = pessoaRepository.BuscarPorIdAsync(idPessoaInserida).Result;

            Assert.True(idPessoaInserida > 0);
            Assert.Equal("José Almeida", pessoaInserida.Nome);
            Assert.Equal(53, pessoaInserida.Idade);
        }

        [Fact]
        public void ValidaBuscaDePessoasPorNome()
        {
            IPessoaRepository pessoaRepository = ObtemRepositorioDeTeste();
            var pessoa = new Entity.Pessoa
            {
                Nome = "José Almeida",
                Idade = 53
            };
            string nomePessoa = "José";

            int idPessoaInserida = pessoaRepository.InserirAsync(pessoa).Result;
            IEnumerable<Entity.Pessoa> resultadoPessoas = pessoaRepository.BuscarPorNomeAsync(nomePessoa).Result;

            Assert.Single(resultadoPessoas);
            Assert.Contains(nomePessoa, resultadoPessoas.First().Nome);
        }

        [Fact]
        public void ValidaAtualizacaoDePessoa()
        {
            IPessoaRepository pessoaRepository = ObtemRepositorioDeTeste();
            var pessoa = new Entity.Pessoa
            {
                Nome = "José Almeida",
                Idade = 53
            };
            string nomeAlterado = "José Carlos Almeida";
            int idadeAlterada = 42;

            int idPessoaASerAlterada = pessoaRepository.InserirAsync(pessoa).Result;
            Entity.Pessoa pessoaASerAlterada = pessoaRepository.BuscarPorIdAsync(idPessoaASerAlterada).Result;
            pessoaASerAlterada.Nome = nomeAlterado;
            pessoaASerAlterada.Idade = idadeAlterada;
            bool atualizadoComSucesso = pessoaRepository.AtualizaAsync(pessoaASerAlterada).Result;
            Entity.Pessoa pessoaAlterada = pessoaRepository.BuscarPorIdAsync(idPessoaASerAlterada).Result;

            Assert.True(atualizadoComSucesso);
            Assert.NotNull(pessoaAlterada);
            Assert.Equal(nomeAlterado, pessoaAlterada.Nome);
            Assert.Equal(idadeAlterada, pessoaAlterada.Idade);
        }

        [Fact]
        public void ValidaExclusaoDePessoa()
        {
            IPessoaRepository pessoaRepository = ObtemRepositorioDeTeste();
            var pessoa = new Entity.Pessoa
            {
                Nome = "José Almeida",
                Idade = 53
            };

            int idPessoaInserida = pessoaRepository.InserirAsync(pessoa).Result;
            Assert.NotNull(pessoaRepository.BuscarPorIdAsync(idPessoaInserida).Result);
            bool excluidoComSucesso = pessoaRepository.ExcluiAsync(idPessoaInserida).Result;

            Assert.True(excluidoComSucesso);
            Assert.Null(pessoaRepository.BuscarPorIdAsync(idPessoaInserida).Result);
        }

        private IPessoaRepository ObtemRepositorioDeTeste()
        {
            DbContextOptions<PessoaContext> options;
            var builder = new DbContextOptionsBuilder<PessoaContext>();
            builder.UseInMemoryDatabase("TestDatabase");
            options = builder.Options;

            PessoaContext pessoaContext = new PessoaContext(options);
            pessoaContext.Database.EnsureDeleted();
            pessoaContext.Database.EnsureCreated();
            return new PessoaRepository(pessoaContext);
        }
    }
}
