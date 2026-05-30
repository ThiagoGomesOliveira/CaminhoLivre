using CaminhoLivre.Compartilhado.Exceptions;
using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Modulo.Catalogo.Application.Services;

public class CategoriaService(ICategoriaRepository categoriaRepository) : ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public async Task AtivarAsync(long id)
    {
        var categoria = _categoriaRepository.ObterPorIdAsync(id)
            .GetAwaiter()
            .GetResult() ?? throw new NotFoundException("Categoria não encontrada.");

        categoria.Ativar();
        await _categoriaRepository.AtualizarAsync(categoria);
        await _categoriaRepository.SalvarAlteracoesAsync();
    }

    public async Task AtualizarAsync(long id, AtualizarCategoriaDto dto)
    {
        var categoria = _categoriaRepository.ObterPorIdAsync(id)
            .GetAwaiter()
            .GetResult() ?? throw new NotFoundException("Categoria não encontrada.");

        categoria.Nome = dto.Nome;
        categoria.Descricao = dto.Descricao;
        categoria.Ativo = dto.Ativo;
        await _categoriaRepository.AtualizarAsync(categoria);
        await _categoriaRepository.SalvarAlteracoesAsync();
    }

    public async Task<long> CriarAsync(CategoriaDto dto)
    {
        var categoria = Categoria.Criar(dto.Nome, dto.Descricao);
        await _categoriaRepository.AdicionarAsync(categoria);

        var sucesso = await _categoriaRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new BusinessRuleException("Não foi possível salvar a categoria no banco de dados.");

        return categoria.Id;
    }

    public async Task DesativarAsync(long id)
    {
        var categoria = _categoriaRepository.ObterPorIdAsync(id)
            .GetAwaiter()
            .GetResult() ?? throw new NotFoundException("Categoria não encontrada.");


        categoria.Desativar();
        await _categoriaRepository.AtualizarAsync(categoria);
        await _categoriaRepository.SalvarAlteracoesAsync();

    }

    public async Task<ResultadoPaginadoDto<CategoriaResponseDto>> ObterTodasAsync(int pagina, int quantidadePorPagina)
    {
        var (categorias, total) = await _categoriaRepository.ObterTodasPaginadasAsync(pagina, quantidadePorPagina);

        var dtos = categorias.Select(c => new CategoriaResponseDto
        {
            Id = c.Id,
            Nome = c.Nome,
            Ativo = c.Ativo,
            DataCadastro = c.DataCadastro,
            DataAlteracao = c.DataAlteracao,
            Descricao = c.Descricao,
            Slug = c.Slug
        }).ToList();

        return new ResultadoPaginadoDto<CategoriaResponseDto>(dtos, total, pagina, quantidadePorPagina);
    }
}
