using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Modulo.Catalogo.Application.Services;

public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
{
    public async Task<long> CriarAsync(ProdutoDto dto)
    {
        var produto = Produto.Criar(dto.Nome, dto.Sku, dto.Descricao, dto.PrecoCusto, dto.PrecoVenda, dto.CategoriaId);
        await produtoRepository.AdicionarAsync(produto);
        var sucesso = await produtoRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new Exception("Não foi possível salvar o produto no banco de dados.");

        return produto.Id;
    }

    public async Task DesativarAsync(long id)
    {
        var produto = produtoRepository.ObterPorIdAsync(id)
            .GetAwaiter()
            .GetResult() ?? throw new Exception("Produto não encontrado.");

        produto.Desativar();
        await produtoRepository.AtualizarAsync(produto);
        await produtoRepository.SalvarAlteracoesAsync();
    }

    public async Task AtivarAsync(long id)
    {
        var produto = produtoRepository.ObterPorIdAsync(id)
            .GetAwaiter()
            .GetResult() ?? throw new Exception("Produto não encontrado.");

        produto.Ativar();
        await produtoRepository.AtualizarAsync(produto);
        await produtoRepository.SalvarAlteracoesAsync();
    }

    public async Task AtualizarAsync(long id, ProdutoDto dto)
    {
        var produto = produtoRepository.ObterPorIdAsync(id)
            .GetAwaiter()
            .GetResult() ?? throw new Exception("Produto não encontrado.");

        produto.Nome = dto.Nome;
        produto.Sku = dto.Sku;
        produto.Descricao = dto.Descricao;
        produto.PrecoCusto = dto.PrecoCusto;
        produto.PrecoVenda = dto.PrecoVenda;
        produto.DataAlteracao = DateTimeOffset.UtcNow;
        await produtoRepository.AtualizarAsync(produto);
        await produtoRepository.SalvarAlteracoesAsync();
    }

    public async Task<ResultadoPaginadoDto<ProdutoResponseDto>> ObterTodasAsync(int pagina, int quantidadePorPagina)
    {
        var (produtos, total) = await produtoRepository.ObterTodasPaginadasAsync(pagina, quantidadePorPagina);
        var produtosResponse = produtos.Select(p => new ProdutoResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            Sku = p.Sku,
            Descricao = p.Descricao,
            PrecoCusto = p.PrecoCusto,
            PrecoVenda = p.PrecoVenda,
            CategoriaId = p.CategoriaId,
            Ativo = p.Ativo,
            CategoriaNome = p.Categoria?.Nome
        }).ToList();

        return new ResultadoPaginadoDto<ProdutoResponseDto>(produtosResponse, total, pagina, quantidadePorPagina);
    }
}
