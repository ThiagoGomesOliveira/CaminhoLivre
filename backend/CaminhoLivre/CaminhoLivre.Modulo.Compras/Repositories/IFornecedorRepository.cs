using CaminhoLivre.Modulo.Compras.Entities;

namespace CaminhoLivre.Modulo.Compras.Repositories;

public interface IFornecedorRepository
{
    Task<Fornecedor> ObterPorIdAsync(long id);
    Task AdicionarAsync(Fornecedor fornecedor);
    void Atualizar(Fornecedor fornecedor);
    Task<bool> SalvarAlteracoesAsync();
    Task<(IEnumerable<Fornecedor> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina);
    Task AtualizarAsync(Fornecedor fornecedor);
}
