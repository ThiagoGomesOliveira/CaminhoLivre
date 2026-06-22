using CaminhoLivre.Compartilhado.Entities;

namespace CaminhoLivre.Compartilhado.Repositories;

public interface IEstadoRepository
{
    Task<Estado> ObterPorIdAsync(long id);
    Task AdicionarAsync(Estado estado);
    void Atualizar(Estado estado);
    Task<bool> SalvarAlteracoesAsync();
    Task<(IEnumerable<Estado> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina);
    Task AtualizarAsync(Estado estado);

    Task<List<Estado>> ObterTodosEstados();
}
