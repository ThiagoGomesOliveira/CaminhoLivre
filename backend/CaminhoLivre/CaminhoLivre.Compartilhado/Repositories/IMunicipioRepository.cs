using CaminhoLivre.Compartilhado.Entities;

namespace CaminhoLivre.Compartilhado.Repositories;

public interface IMunicipioRepository
{
    Task<Municipio> ObterPorIdAsync(long id);
    Task AdicionarAsync(Municipio municipio);
    void Atualizar(Municipio municipio);
    Task<bool> SalvarAlteracoesAsync();
    Task<(IEnumerable<Municipio> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina);
    Task AtualizarAsync(Municipio municipio);
    Task<IEnumerable<Municipio>> ObterTodosMunicipiosAsync();
}
