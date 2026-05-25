using CaminhoLivre.Modulo.Catalogo.Application.DTOs;

namespace CaminhoLivre.Modulo.Catalogo.Application.Interfaces;

public interface IProdutoService
{
    Task<long> CriarAsync(ProdutoDto dto);
    Task DesativarAsync(long id);
    Task AtivarAsync(long id);
    Task AtualizarAsync(long id, ProdutoDto dto);
    Task<ResultadoPaginadoDto<ProdutoResponseDto>> ObterTodasAsync(int pagina, int quantidadePorPagina);
}
