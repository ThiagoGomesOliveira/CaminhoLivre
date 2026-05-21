using CaminhoLivre.Modulo.Catalogo.Entities;

namespace CaminhoLivre.Modulo.Catalogo.Repositories;

public interface IProdutoRepository
{
    Task<Produto> ObterPorIdAsync(long id);
    Task AdicionarAsync(Produto produto);
    void Atualizar(Produto produto);
    Task<bool> SalvarAlteracoesAsync();
}
