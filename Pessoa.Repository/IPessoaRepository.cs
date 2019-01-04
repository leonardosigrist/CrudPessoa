using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pessoa.Repository
{
    public interface IPessoaRepository
    {
        Task<IEnumerable<Entity.Pessoa>> BuscarTodosAsync(string nome = "");

        Task<IEnumerable<Entity.Pessoa>> BuscarPorNomeAsync(string nome = "");

        Task<Entity.Pessoa> BuscarPorIdAsync(int id);

        Task<int> InserirAsync(Entity.Pessoa pessoa);

        Task<bool> AtualizaAsync(Entity.Pessoa pessoa);

        Task<bool> ExcluiAsync(int idPessoa);
    }
}
