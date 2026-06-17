using CaminhoLivre.Compartilhado.Entities;

namespace CaminhoLivre.Compartilhado.Repositories;

public interface ILogradouroRepository
{
    Task<Logradouro> ObterPorIdAsync(long id);
    Task AdicionarAsync(Logradouro logradouro);
    void Atualizar(Logradouro logradouro);
    Task<bool> SalvarAlteracoesAsync();
    Task<(IEnumerable<Logradouro> Itens, int Total)> ObterTodasPaginadasAsync(int pagina, int quantidadePorPagina);
    Task AtualizarAsync(Logradouro logradouro);
}
