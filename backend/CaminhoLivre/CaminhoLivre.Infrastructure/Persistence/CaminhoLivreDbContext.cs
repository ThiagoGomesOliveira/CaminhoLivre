using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Persistence
{
    public class CaminhoLivreDbContext : DbContext
    {
        // O construtor recebe as opções de configuração (como a string de conexão) vindas da API
        public CaminhoLivreDbContext(DbContextOptions<CaminhoLivreDbContext> options) : base(options)
        {
        }

        // Mapeia suas entidades como tabelas pesquisáveis no banco

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Esta linha varre este projeto procurando classes que herdam de IEntityTypeConfiguration
            // Isso vai ativar automaticamente o ProdutoMapping e CategoriaMapping que criamos!
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CaminhoLivreDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
