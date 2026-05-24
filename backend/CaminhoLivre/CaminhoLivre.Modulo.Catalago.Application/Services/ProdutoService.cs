using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;

namespace CaminhoLivre.Modulo.Catalogo.Application.Services;

public class ProdutoService(IProdutoRepository produtoRepository) : IProdutoService
{
    public async Task<long> CriarAsync(ProdutoDto dto)
    {

        var produto = Produto.Criar(dto.Nome, dto.Sku, dto.Descricao, dto.PrecoCusto, dto.PrecoVenda,dto.CategoriaId);
        await produtoRepository.AdicionarAsync(produto);
        var sucesso = await produtoRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new Exception("Não foi possível salvar o produto no banco de dados.");

        return produto.Id;
    }
}
