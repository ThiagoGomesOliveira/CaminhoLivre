using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Modulo.Catalogo.Application.Services;

public class ProdutoService : IProdutoService
{
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
        _produtoRepository = produtoRepository;
    }

    public async Task<long> CriarAsync(CriarProdutoDto dto)
    {
        var produto = new Produto
        {
            Sku = string.Empty,
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            PrecoVenda = 0,
            PrecoCusto = 0,
            CategoriaId = 0,
            Categoria = new Categoria("nome")
        };

        await _produtoRepository.AdicionarAsync(produto);
        var sucesso = await _produtoRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new Exception("Não foi possível salvar o produto no banco de dados.");

        return produto.Id;
    }
}
