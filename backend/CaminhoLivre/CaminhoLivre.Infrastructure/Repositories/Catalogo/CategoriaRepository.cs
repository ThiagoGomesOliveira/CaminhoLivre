using CaminhoLivre.Infrastructure.Persistence;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Infrastructure.Repositories.Catalogo;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly CaminhoLivreDbContext _context;

    public CategoriaRepository(CaminhoLivreDbContext context) => _context = context;
    public async Task AdicionarAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
    }

    public void Atualizar(Categoria categoria)
    {
       _context.Categorias.Update(categoria);
    }

    public async Task<Categoria> ObterPorIdAsync(long id) 
        => await _context.Categorias.FindAsync(id);

    public async Task<bool> SalvarAlteracoesAsync()
        => await _context.SaveChangesAsync() > 0;
}
