using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Compartilhado.Repositories;
using CaminhoLivre.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Repositories.Compartilhado;

public class EstadoRepository(CaminhoLivreDbContext context) : IEstadoRepository
{
    public async Task AdicionarAsync(Estado estado)
    {
       await context.Estados.AddAsync(estado);
    }

    public void Atualizar(Estado estado)
    {
        context.Estados.Update(estado);
    }

    public async Task AtualizarAsync(Estado estado)
    {
        context.Estados.Update(estado);
        await Task.CompletedTask;
    }

    public async Task<Estado> ObterPorIdAsync(long id)
        => await context.Estados.FindAsync(id);

    public async Task<(IEnumerable<Estado> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina)
    {
        var total = await context.Estados.CountAsync();

        var itens = await context.Estados
            .AsNoTracking()
            .OrderBy(c => c.Nome)
            .Skip((pagina - 1) * quantidadePorPagina)
            .Take(quantidadePorPagina)
            .ToListAsync();

        return (itens, total);
    }

    public async Task<List<Estado>> ObterTodosEstados()
     => await context.Estados.ToListAsync();

    public async Task<bool> SalvarAlteracoesAsync()
        => await context.SaveChangesAsync() > 0;
}
