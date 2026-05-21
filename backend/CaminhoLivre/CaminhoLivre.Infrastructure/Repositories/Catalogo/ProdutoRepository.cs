using CaminhoLivre.Infrastructure.Persistence;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Infrastructure.Repositories.Catalogo;

public class ProdutoRepository : IProdutoRepository
{
    private readonly CaminhoLivreDbContext _context;
    public ProdutoRepository(CaminhoLivreDbContext context)
    {
        _context = context;
    }
    public async Task AdicionarAsync(Produto produto)
    {
        await _context.Produtos.AddAsync(produto);
    }

    public void Atualizar(Produto produto)
    {
        _context.Produtos.Update(produto);
    }

    public async Task<Produto> ObterPorIdAsync(long id)
     => await _context.Produtos.FindAsync(id);

    public async Task<bool> SalvarAlteracoesAsync()
     => await _context.SaveChangesAsync() > 0;
}
