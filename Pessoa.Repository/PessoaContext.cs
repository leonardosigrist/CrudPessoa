using Microsoft.EntityFrameworkCore;

namespace Pessoa.Repository
{
    public class PessoaContext : DbContext
    {
        public PessoaContext(DbContextOptions<PessoaContext> contextOptions) : base(contextOptions)
        {
            
        }

        public DbSet<Entity.Pessoa> Pessoas { get; set; }
    }
}
