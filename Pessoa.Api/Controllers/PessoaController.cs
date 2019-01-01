using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pessoa.Service;

namespace Pessoa.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService pessoaService;

        public PessoaController(IPessoaService pessoaService)
        {
            this.pessoaService = pessoaService;
        }

        // GET: api/Pessoa
        [HttpGet]
        public async Task<IEnumerable<Entity.Pessoa>> Get()
        {
            return await this.pessoaService.BuscarPessoasPorNomeAsync();
        }

        // GET: api/Pessoa/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Entity.Pessoa> Get(int id)
        {
            return await this.pessoaService.BuscarPessoaPorIdAsync(id);
        }

        // POST: api/Pessoa
        [HttpPost]
        public async Task<int> Post([FromBody] Entity.Pessoa pessoa)
        {
            return await this.pessoaService.InserirPessoaAsync(pessoa);
        }

        // PUT: api/Pessoa/5
        [HttpPut("{id}")]
        public async Task<bool> Put(int id, [FromBody] Entity.Pessoa pessoa)
        {
            return await this.pessoaService.AtualizaPessoaAsync(pessoa);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(int id)
        {
            return await this.pessoaService.ExcluiPessoaAsync(id);
        }
    }
}
