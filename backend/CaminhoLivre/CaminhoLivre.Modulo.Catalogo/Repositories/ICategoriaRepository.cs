using CaminhoLivre.Modulo.Catalogo.Entities;

namespace CaminhoLivre.Modulo.Catalogo.Repositories;

public interface ICategoriaRepository
{
    Task<Categoria> ObterPorIdAsync(long id);
    Task AdicionarAsync(Categoria categoria);
    void Atualizar(Categoria categoria);
    Task<bool> SalvarAlteracoesAsync();
}
