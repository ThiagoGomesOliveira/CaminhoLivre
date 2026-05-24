using System.ComponentModel.DataAnnotations;
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
        var sku = "VES-CAM-PRM";
        var descricao = "Descrição do produto teste";
        var precoCusto = 10.0m;
        var precoVenda = 20.0m;
        var categoriaId = 1;

        // Act
        var produto = Produto.Criar(nome, sku, descricao, precoCusto, precoVenda, categoriaId);

        // Assert
        Assert.IsNotNull(produto);
    }

    [TestMethod]
    public void Criar_ComSkuInvalido_DeveDispararDomainValidationException()
    {
        // Arrange 
        var nome = "Produto Teste";
        var descricao = "Descrição do produto teste";
        var precoCusto = 10.0m;
        var precoVenda = 20.0m;
        var categoriaId = 1;
        var skuInvalido = "SKU-INVALIDO-LONGO-E-MINUSCULO";

        // Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Produto.Criar(nome, skuInvalido, descricao, precoCusto, precoVenda, categoriaId);
        });

        // Assert adicional (Opcional: validar a mensagem de erro de dentro da exceção)
        Assert.IsTrue(excecao.Message.Contains("padrão do ERP"));
    }

    [TestMethod]
    public void Criar_PrecoCustoInvalido_DeveDispararDomainValidationException()
    {
        // Arrange 
        var nome = "Produto Teste";
        var descricao = "Descrição do produto teste";
        var precoCusto = 0;
        var precoVenda = 20.0m;
        var categoriaId = 1;
        var skuInvalido = "VES-CAM-PRM";

        // Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Produto.Criar(nome, skuInvalido, descricao, precoCusto, precoVenda, categoriaId);
        });

        // Assert adicional (Opcional: validar a mensagem de erro de dentro da exceção)
        Assert.IsTrue(excecao.Message.Contains("O preço de custo do produto deve ser maior que zero."));
    }

    [TestMethod]
    public void Criar_PrecoVendaInvalido_DeveDispararDomainValidationException()
    {
        // Arrange 
        var nome = "Produto Teste";
        var descricao = "Descrição do produto teste";
        var precoCusto = 20.0m;
        var precoVenda = 0;
        var categoriaId = 1;
        var skuInvalido = "VES-CAM-PRM";

        // Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Produto.Criar(nome, skuInvalido, descricao, precoCusto, precoVenda, categoriaId);
        });

        // Assert adicional (Opcional: validar a mensagem de erro de dentro da exceção)
        Assert.IsTrue(excecao.Message.Contains("O preço de venda do produto deve ser maior que zero."));
    }

    [TestMethod]
    public void Criar_CategoriaInvalida_DeveDispararDomainValidationException()
    {
        // Arrange 
        var nome = "Produto Teste";
        var descricao = "Descrição do produto teste";
        var precoCusto = 20.0m;
        var precoVenda = 2;
        var categoriaId = 0;
        var skuInvalido = "VES-CAM-PRM";

        // Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Produto.Criar(nome, skuInvalido, descricao, precoCusto, precoVenda, categoriaId);
        });

        // Assert adicional (Opcional: validar a mensagem de erro de dentro da exceção)
        Assert.IsTrue(excecao.Message.Contains("A categoria do produto é obrigatória."));
    }

    [TestMethod]
    public void Criar_ComSkuInvalidoComZero_DeveDispararDomainValidationException()
    {
        // Arrange 
        var nome = "Produto Teste";
        var descricao = "Descrição do produto teste";
        var precoCusto = 10.0m;
        var precoVenda = 20.0m;
        var categoriaId = 1;
        var skuInvalido = "VES-CAM-PRI";

        // Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Produto.Criar(nome, skuInvalido, descricao, precoCusto, precoVenda, categoriaId);
        });

        // Assert adicional (Opcional: validar a mensagem de erro de dentro da exceção)
        Assert.IsTrue(excecao.Message.Contains("Por questões de legibilidade operacional, o SKU não deve conter as letras 'O' ou 'I'."));
    }

    [TestMethod]
    public void Criar_ComSkuInvalidoForaDoPadrao_DeveDispararDomainValidationException()
    {
        // Arrange 
        var nome = "Produto Teste";
        var descricao = "Descrição do produto teste";
        var precoCusto = 10.0m;
        var precoVenda = 20.0m;
        var categoriaId = 1;
        var skuInvalido = "VE";

        // Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Produto.Criar(nome, skuInvalido, descricao, precoCusto, precoVenda, categoriaId);
        });

        // Assert adicional (Opcional: validar a mensagem de erro de dentro da exceção)
        Assert.IsTrue(excecao.Message.Contains("O SKU informado está fora do padrão do ERP (Ex: VES-CAM-PRM). Use apenas maiúsculas, números e hifens."));
    }

}
