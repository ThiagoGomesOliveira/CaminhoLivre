using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Compras.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Persistence;

public class CaminhoLivreDbContext(DbContextOptions<CaminhoLivreDbContext> options) : DbContext(options)
{
    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Estado> Estados => Set<Estado>();
    public DbSet<Municipio> Municipios => Set<Municipio>();
    public DbSet<Logradouro> Logradouros => Set<Logradouro>();
    public DbSet<Fornecedor> Fornecedores => Set<Fornecedor>();  

    // Mapeia suas entidades como tabelas pesquisáveis no banco

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Esta linha varre este projeto procurando classes que herdam de IEntityTypeConfiguration
        // Isso vai ativar automaticamente o ProdutoMapping e CategoriaMapping que criamos!
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CaminhoLivreDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
