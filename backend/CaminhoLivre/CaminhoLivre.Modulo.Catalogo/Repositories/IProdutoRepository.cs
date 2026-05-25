using CaminhoLivre.Modulo.Catalogo.Entities;

namespace CaminhoLivre.Modulo.Catalogo.Repositories;

public interface IProdutoRepository
{
    Task<Produto> ObterPorIdAsync(long id);
    Task AdicionarAsync(Produto produto);
    void Atualizar(Produto produto);
    Task<bool> SalvarAlteracoesAsync();
    Task AtualizarAsync(Produto produto);
    Task<(IEnumerable<Produto> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina);
}
