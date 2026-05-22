using CaminhoLivre.Modulo.Catalogo.Entities;

namespace CaminhoLivre.Modulo.Catalago.Tests.Entites;

[TestClass]
public class ProdutoTest
{

    [TestMethod]
    public void CriarProduto_Valido_DeveCriarComSucesso()
    {
        // Arrange
        var nome = "Produto Teste";
        var sku = "PRODUTO-TESTE";
        var descricao = "Descrição do produto teste";
        var precoCusto = 10.0m;
        var precoVenda = 20.0m;

        // Act
        var produto = new Produto(nome, sku, descricao, precoCusto, precoVenda)
        {
            Ncm = "123",
            Categoria = Categoria.Criar("teste", "descricao"),
            CategoriaId = 1,
            Sku = sku,
            Nome = nome,
            Descricao = descricao,
            PrecoVenda = precoVenda,
            PrecoCusto = precoCusto
        };

        // Assert
        Assert.IsNotNull(produto);
    }
}
