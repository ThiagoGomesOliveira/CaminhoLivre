using CaminhoLivre.Infrastructure.Persistence;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Repositories.Catalogo;

public class ProdutoRepository(CaminhoLivreDbContext context) : IProdutoRepository
{
    public async Task AdicionarAsync(Produto produto)
    {
        await context.Produtos.AddAsync(produto);
    }

    public void Atualizar(Produto produto)
    {
        context.Produtos.Update(produto);
    }

    public async Task AtualizarAsync(Produto produto)
    {
        context.Produtos.Update(produto);
        await Task.CompletedTask;
    }

    public async Task<Produto> ObterPorIdAsync(long id)
     => await context.Produtos.FindAsync(id);

    public async Task<(IEnumerable<Produto> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina)
    {
        var total = await context.Produtos.CountAsync();

        var itens = await context.Produtos
            .AsNoTracking()
            .OrderBy(c => c.Nome)
            .Skip((pagina - 1) * quantidadePorPagina)
            .Take(quantidadePorPagina)
            .ToListAsync();

        return (itens, total);
    }

    public async Task<bool> SalvarAlteracoesAsync()
     => await context.SaveChangesAsync() > 0;
}
