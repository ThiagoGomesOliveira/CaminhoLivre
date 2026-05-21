using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Persistence;

public class CaminhoLivreDbContextFactory : IDesignTimeDbContextFactory<CaminhoLivreDbContext>
{
    public CaminhoLivreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CaminhoLivreDbContext>();

        // Coloque a sua string de conexão local do PostgreSQL diretamente aqui
        var connectionString = "Host=localhost;Port=5432;Database=CaminhoLivre;Username=postgres;Password=123456";

        optionsBuilder.UseNpgsql(connectionString);

        return new CaminhoLivreDbContext(optionsBuilder.Options);
    }
}
