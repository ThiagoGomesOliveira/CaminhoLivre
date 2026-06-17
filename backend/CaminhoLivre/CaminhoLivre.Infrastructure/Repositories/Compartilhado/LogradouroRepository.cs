using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Compartilhado.Repositories;
using CaminhoLivre.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Repositories.Compartilhado;

public class LogradouroRepository(CaminhoLivreDbContext context) : ILogradouroRepository
{
    public async Task AdicionarAsync(Logradouro logradouro)
    {
        await context.Logradouros.AddAsync(logradouro);
    }

    public void Atualizar(Logradouro logradouro)
    {
        context.Logradouros.Update(logradouro);
    }

    public async Task AtualizarAsync(Logradouro logradouro)
    {
        context.Logradouros.Update(logradouro);
        await Task.CompletedTask;
    }

    public async Task<Logradouro> ObterPorIdAsync(long id)
    {
        return await context.Logradouros.FindAsync(id);
    }

    public async Task<(IEnumerable<Logradouro> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina)
    {
        var total = await context.Logradouros.CountAsync();

        var itens = await context.Logradouros
            .AsNoTracking()
            .OrderBy(c => c.Id)
            .Skip((pagina - 1) * quantidadePorPagina)
            .Take(quantidadePorPagina)
            .ToListAsync();

        return (itens, total);
    }

    public async Task<bool> SalvarAlteracoesAsync()
      => await context.SaveChangesAsync() > 0;
}
