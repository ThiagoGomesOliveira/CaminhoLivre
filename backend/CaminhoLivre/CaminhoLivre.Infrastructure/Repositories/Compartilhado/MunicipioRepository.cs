using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Compartilhado.Repositories;
using CaminhoLivre.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Repositories.Compartilhado;

public class MunicipioRepository(CaminhoLivreDbContext context) : IMunicipioRepository
{
    public async Task AdicionarAsync(Municipio municipio)
    {
        await context.Municipios.AddAsync(municipio);
    }

    public void Atualizar(Municipio municipio)
    {
        context.Municipios.Update(municipio);
    }

    public async Task AtualizarAsync(Municipio municipio)
    {
        context.Municipios.Update(municipio);
        await Task.CompletedTask;
    }

    public async Task<Municipio> ObterPorIdAsync(long id)
    {
        return await context.Municipios.FindAsync(id);
    }

    public async Task<(IEnumerable<Municipio> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina)
    {
        var total = await context.Municipios.CountAsync();

        var itens = await context.Municipios
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
