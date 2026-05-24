using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Modulo.Catalogo.Application.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public async Task<long> CriarAsync(CategoriaDto dto)
    {
        var categoria =  Categoria.Criar(dto.Nome, dto.Descricao);
        await _categoriaRepository.AdicionarAsync(categoria);

        var sucesso = await _categoriaRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new Exception("Não foi possível salvar a categoria no banco de dados.");
        
        return categoria.Id;
    }
}
