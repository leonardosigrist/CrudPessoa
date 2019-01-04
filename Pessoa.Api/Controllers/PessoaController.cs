using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pessoa.Repository;

namespace Pessoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaRepository pessoaRepository;

        public PessoaController(IPessoaRepository pessoaRepository)
        {
            this.pessoaRepository = pessoaRepository;
        }

        // GET: api/Pessoa
        [HttpGet]
        public async Task<IEnumerable<Entity.Pessoa>> Get()
        {
            return await this.pessoaRepository.BuscarTodosAsync();
        }

        // GET: api/Pessoa/nome/José
        [HttpGet("nome/{nome}")]
        public async Task<IEnumerable<Entity.Pessoa>> Get(string nome)
        {
            return await this.pessoaRepository.BuscarPorNomeAsync(nome);
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Entity.Pessoa> Get(int id)
        {
            return await this.pessoaRepository.BuscarPorIdAsync(id);
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<int> Post([FromBody] Entity.Pessoa pessoa)
        {
            return await this.pessoaRepository.InserirAsync(pessoa);
        }

        // PUT: api/Pessoa/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] Entity.Pessoa pessoa)
        {
            return await this.pessoaRepository.AtualizaAsync(pessoa);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await this.pessoaRepository.ExcluiAsync(id);
        }
    }
}
