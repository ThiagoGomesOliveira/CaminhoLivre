using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Modulo.Catalogo.Application.Services;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task<long> CriarAsync(CriarCategoriaDto dto)
    {

        var categoria = new Categoria(dto.Nome);
        await _categoriaRepository.AdicionarAsync(categoria);

        var sucesso = await _categoriaRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new Exception("Não foi possível salvar a categoria no banco de dados.");
        
        return categoria.Id;
    }
}
