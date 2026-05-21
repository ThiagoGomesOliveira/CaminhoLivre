using CaminhoLivre.Modulo.Catalogo.Application.DTOs;

namespace CaminhoLivre.Modulo.Catalogo.Application.Interfaces;

public interface ICategoriaService
{
    Task<long> CriarAsync(CriarCategoriaDto dto);
}
