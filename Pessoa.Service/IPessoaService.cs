using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pessoa.Service
{
    public interface IPessoaService
    {
        Task<IEnumerable<Entity.Pessoa>> BuscarPessoasPorNomeAsync(string nome = "");

        Task<Entity.Pessoa> BuscarPessoaPorIdAsync(int id);

        Task<int> InserirPessoaAsync(Entity.Pessoa pessoa);

        Task<bool> AtualizaPessoaAsync(Entity.Pessoa pessoa);

        Task<bool> ExcluiPessoaAsync(int idPessoa);
    }
}
