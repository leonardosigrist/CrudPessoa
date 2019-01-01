using Microsoft.EntityFrameworkCore;
using Pessoa.Repository;
using Pessoa.Service;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Pessoa.Domain.Test
{
    public class PessoaTest
    {
        private readonly DbContextOptions<PessoaContext> dbContextOptions;

        public PessoaTest()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;Database=PessoaCrudTest;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<PessoaContext>();
            optionsBuilder.UseSqlServer(connectionString);

            this.dbContextOptions = optionsBuilder.Options;
        }

        [Fact]
        public void ValidaInsercaoDePessoa()
        {
            using (var pessoaContext = new PessoaContext(dbContextOptions))
            {
                IPessoaService pessoaService = new PessoaService(pessoaContext);
                Entity.Pessoa pessoa = new Entity.Pessoa
                {
                    Nome = "José Almeida",
                    Idade = 53
                };

                int idPessoaInserida = pessoaService.InserirPessoaAsync(pessoa).Result;

                Assert.True(idPessoaInserida > 0); 
            }
        }

        [Fact]
        public void ValidaBuscaDePessoasPorNome()
        {
            using (var pessoaContext = new PessoaContext(dbContextOptions))
            {
                IPessoaService pessoaService = new PessoaService(pessoaContext);
                string nomePessoa = "José";

                IEnumerable<Entity.Pessoa> resultadoPessoas = pessoaService.BuscarPessoasPorNomeAsync(nomePessoa).Result;

                if (resultadoPessoas.Any())
                {
                    Assert.Contains(resultadoPessoas.First().Nome, nomePessoa);
                } 
            }
        }

        [Fact]
        public void ValidaAtualizacaoDePessoa()
        {
            using (var pessoaContext = new PessoaContext(dbContextOptions))
            {
                IPessoaService pessoaService = new PessoaService(pessoaContext);
                string nomePessoaASerAlterada = "José Almeida";
                Entity.Pessoa pessoaASerAlterada = pessoaService.BuscarPessoasPorNomeAsync(nomePessoaASerAlterada).Result.FirstOrDefault();

                if (pessoaASerAlterada != null)
                {
                    pessoaASerAlterada.Nome = "José Luis Almeida";

                    bool atualizadoComSucesso = pessoaService.AtualizaPessoaAsync(pessoaASerAlterada).Result;

                    Assert.True(atualizadoComSucesso);
                } 
            }
        }

        [Fact]
        public void ValidaExclusaoDePessoa()
        {
            using (var pessoaContext = new PessoaContext(dbContextOptions))
            {
                IPessoaService pessoaService = new PessoaService(pessoaContext);
                string nomePessoaASerExcluida = "José Luis Almeida";
                int? idPessoaExcluida = pessoaService.BuscarPessoasPorNomeAsync(nomePessoaASerExcluida).Result.FirstOrDefault()?.Id;

                if (idPessoaExcluida.HasValue)
                {
                    bool excluidoComSucesso = pessoaService.ExcluiPessoaAsync(idPessoaExcluida.Value).Result;

                    Assert.True(excluidoComSucesso);
                } 
            }
        }
    }
}
