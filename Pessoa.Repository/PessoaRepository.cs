using Microsoft.EntityFrameworkCore;
using Pessoa.Entity;
using Pessoa.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pessoa.Repository
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly PessoaContext pessoaContext;

        public PessoaRepository(PessoaContext pessoaContext)
        {
            this.pessoaContext = pessoaContext;
        }

        public async Task<IEnumerable<Entity.Pessoa>> BuscarTodosAsync(string nome = "")
        {
            try
            {
                return await this.pessoaContext.Pessoas.ToListAsync<Entity.Pessoa>();
            }
            catch
            {
                return new List<Entity.Pessoa>();
            }
        }

        public async Task<Entity.Pessoa> BuscarPorIdAsync(int id)
        {
            return await this.pessoaContext.Pessoas.FindAsync(id);
        }

        public async Task<IEnumerable<Entity.Pessoa>> BuscarPorNomeAsync(string nome = "")
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

        public async Task<int> InserirAsync(Entity.Pessoa pessoa)
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

        public async Task<bool> AtualizaAsync(Entity.Pessoa pessoa)
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

        public async Task<bool> ExcluiAsync(int idPessoa)
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
    }
}
