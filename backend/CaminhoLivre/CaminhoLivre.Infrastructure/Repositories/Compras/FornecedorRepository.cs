using CaminhoLivre.Infrastructure.Persistence;
using CaminhoLivre.Modulo.Compras.Entities;
using CaminhoLivre.Modulo.Compras.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CaminhoLivre.Infrastructure.Repositories.Compras;

public class FornecedorRepository(CaminhoLivreDbContext context) : IFornecedorRepository
{
    public async Task AdicionarAsync(Fornecedor fornecedor)
    {
        await context.Fornecedores.AddRangeAsync(fornecedor);
    }

    public void Atualizar(Fornecedor fornecedor)
    {
       context.Fornecedores.Update(fornecedor);
    }

    public async Task AtualizarAsync(Fornecedor fornecedor)
    {
        context.Fornecedores.Update(fornecedor);
        await Task.CompletedTask;
    }

    public async Task<Fornecedor> ObterPorIdAsync(long id)
    {
        return await context.Fornecedores.FindAsync(id);
    }

    public async Task<(IEnumerable<Fornecedor> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina)
    {
        var total = await context.Fornecedores.CountAsync();

        var itens = await context.Fornecedores
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
