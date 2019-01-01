using Microsoft.EntityFrameworkCore;
using Pessoa.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoa.Service
{
    public class PessoaService : IPessoaService
    {
        private readonly PessoaContext pessoaContext;

        public PessoaService(PessoaContext pessoaContext)
        {
            this.pessoaContext = pessoaContext;
        }

        public async Task<bool> AtualizaPessoaAsync(Entity.Pessoa pessoa)
        {
            try
            {
                this.pessoaContext.Attach(pessoa);
                this.pessoaContext.Entry(pessoa).State = EntityState.Modified;
                await this.pessoaContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<Entity.Pessoa>> BuscarPessoasPorNomeAsync(string nome = "")
        {
            try
            {
                int quantidadeMaximaRegistros = 50;

                return await this.pessoaContext.Pessoas
                    .Where(pes => pes.Nome.Contains(nome))
                    .Take(quantidadeMaximaRegistros)
                    .ToListAsync<Entity.Pessoa>();
            }
            catch
            {
                return new List<Entity.Pessoa>();
            }
        }

        public async Task<Entity.Pessoa> BuscarPessoaPorIdAsync(int id)
        {
            return await this.pessoaContext.Pessoas.FindAsync(id);
        } 

        public async Task<bool> ExcluiPessoaAsync(int idPessoa)
        {
            try
            {
                var pessoaASerExcluida = await this.pessoaContext.Pessoas.FindAsync(idPessoa);

                if (pessoaASerExcluida == null)
                    return false;

                this.pessoaContext.Pessoas.Remove(pessoaASerExcluida);
                await this.pessoaContext.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> InserirPessoaAsync(Entity.Pessoa pessoa)
        {
            try
            {
                var pessoaIncluida = this.pessoaContext.Pessoas.Add(pessoa);
                await this.pessoaContext.SaveChangesAsync();

                return pessoaIncluida.Entity.Id;
            }
            catch
            {
                return 0;
            }
        }
    }
}
