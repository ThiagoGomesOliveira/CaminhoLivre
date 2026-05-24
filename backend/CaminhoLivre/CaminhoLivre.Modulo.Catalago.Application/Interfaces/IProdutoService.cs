using CaminhoLivre.Modulo.Catalogo.Application.DTOs;

namespace CaminhoLivre.Modulo.Catalogo.Application.Interfaces;

public interface IProdutoService
{
    Task<long> CriarAsync(ProdutoDto dto);
}
