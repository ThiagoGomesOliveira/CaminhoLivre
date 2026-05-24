using CaminhoLivre.Modulo.Catalogo.Application.DTOs;

namespace CaminhoLivre.Modulo.Catalogo.Application.Interfaces;

public interface ICategoriaService
{
    Task<long> CriarAsync(CategoriaDto dto);
    Task<ResultadoPaginadoDto<CategoriaResponseDto>> ObterTodasAsync(int pagina, int quantidadePorPagina);
    Task AtivarAsync(long id);
    Task DesativarAsync(long id);
    Task AtualizarAsync(long id, AtualizarCategoriaDto dto);
}
