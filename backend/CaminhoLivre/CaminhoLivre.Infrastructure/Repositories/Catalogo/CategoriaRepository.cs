using CaminhoLivre.Infrastructure.Persistence;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Repositories.Catalogo;

public class CategoriaRepository(CaminhoLivreDbContext context) : ICategoriaRepository
{
    public async Task AdicionarAsync(Categoria categoria)
    {
        await context.Categorias.AddAsync(categoria);
    }

    public void Atualizar(Categoria categoria)
    {
       context.Categorias.Update(categoria);
    }

    public async Task<(IEnumerable<Categoria> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina)
    {
        var total = await context.Categorias.CountAsync();
        
        var itens = await context.Categorias
            .AsNoTracking() 
            .OrderBy(c => c.Nome)
            .Skip((pagina - 1) * quantidadePorPagina)
            .Take(quantidadePorPagina)
            .ToListAsync();

        return (itens, total);
    }

    public async Task<Categoria> ObterPorIdAsync(long id) 
        => await context.Categorias.FindAsync(id);

    public async Task<bool> SalvarAlteracoesAsync()
        => await context.SaveChangesAsync() > 0;

    public async Task AtualizarAsync(Categoria categoria)
    {
        context.Categorias.Update(categoria);
        await Task.CompletedTask;
    }
     
}
