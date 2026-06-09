using CaminhoLivre.Compartilhado.Exceptions;
using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Services;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;
using Moq;

namespace CaminhoLivre.Modulo.Catalago.Tests.Services;

[TestClass]
public class ProdutoServiceTest
{
    private Mock<IProdutoRepository> _produtoRepositoryMock;
    private ProdutoService _produtoServiceMock;

    [TestInitialize]
    public void Init()
    {
        _produtoRepositoryMock = new Mock<IProdutoRepository>();
        _produtoServiceMock = new ProdutoService(_produtoRepositoryMock.Object);
    }

    [TestMethod]
    public async Task CriarAsync_QuandoDadosForemValidosESalvarComSucesso_DeveRetornarOIdDoProduto()
    {
        //Arrange 
        var produtoDto = new ProdutoDto
        {
            Nome = "Camiseta Polo Algodão Premium",
            Descricao = "A Camiseta Básica Premium é a escolha perfeita para quem busca unir conforto e durabilidade.",
            Sku = "VES-CAM-PM", 
            PrecoCusto = 49.90m,
            PrecoVenda = 89.90m, 
            CategoriaId = 1,
            Ativo = true
        };

        _produtoRepositoryMock
            .Setup(repo => repo.SalvarAlteracoesAsync())
            .ReturnsAsync(true);

        //Act
        var resultadoId = await _produtoServiceMock.CriarAsync(produtoDto);

        //Assert
        Assert.IsTrue(resultadoId >= 0);

        _produtoRepositoryMock.Verify(
            repo => repo.AdicionarAsync(It.IsAny<Produto>()),
            Times.Once
        );
       
        _produtoRepositoryMock.Verify(
            repo => repo.SalvarAlteracoesAsync(),
            Times.Once
        );
    }

    [TestMethod]
    public async Task CriarAsync_QuandoBancoFalharAoSalvar_DeveLanciarBusinessRuleException()
    {
        //Arrange 
        var produtoDto = new ProdutoDto
        {
            Nome = "Camiseta Polo Algodão Premium",
            Descricao = "A Camiseta Básica Premium é a escolha perfeita para quem busca unir conforto e durabilidade.",
            Sku = "VES-CAM-PM",
            PrecoCusto = 49.90m,
            PrecoVenda = 89.90m,
            CategoriaId = 1,
            Ativo = true
        };

        _produtoRepositoryMock
            .Setup(repo => repo.SalvarAlteracoesAsync())
            .ReturnsAsync(false);

        //Act Assert
        await Assert.ThrowsExceptionAsync<BusinessRuleException>(
            async () => await _produtoServiceMock.CriarAsync(produtoDto)
        );
    }

    [TestMethod]
    public async Task DesativarAsync_QuandoProdutoExistir_DeveChamarDesativarEAtualizarNoBanco()
    {
        //Arrange
        long produtoId = 42;

        var produtoMock = Produto.Criar("Produto Teste", "VES-CAM-PM", "Descrição", 10m, 20m, 1);

        _produtoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(produtoId))
            .ReturnsAsync(produtoMock);

        //Act
        await _produtoServiceMock.DesativarAsync(produtoId);

        //Assert
        _produtoRepositoryMock.Verify(
            repo => repo.AtualizarAsync(produtoMock),
            Times.Once
        );

        _produtoRepositoryMock.Verify(
            repo => repo.SalvarAlteracoesAsync(),
            Times.Once
        );
    }

    [TestMethod]
    public async Task DesativarAsync_QuandoProdutoNaoExistir_DeveLanciarNotFoundException()
    {
        //Arrange
        long idInexistente = 999;

        _produtoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(idInexistente))
            .ReturnsAsync((Produto)null);

        //Act Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(
            async () => await _produtoServiceMock.DesativarAsync(idInexistente)
        );
    }

    [TestMethod]
    public async Task AtivarAsync_QuandoProdutoExistir_DeveChamarAtivarEAtualizarNoBanco()
    {
        //Arrange 
        long produtoId = 100;

        var produtoMock = Produto.Criar("Produto Inativo", "VES-CAM-PM", "Descrição", 10m, 20m, 1);

        _produtoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(produtoId))
            .ReturnsAsync(produtoMock);

        //Act
        await _produtoServiceMock.AtivarAsync(produtoId);

        //Assert
        _produtoRepositoryMock.Verify(
            repo => repo.AtualizarAsync(produtoMock),
            Times.Once
        );

        _produtoRepositoryMock.Verify(
            repo => repo.SalvarAlteracoesAsync(),
            Times.Once
        );
    }

    [TestMethod]
    public async Task AtivarAsync_QuandoProdutoNaoExistir_DeveLanciarNotFoundException()
    {
        //Arrange 
        long idInexistente = 999;

        _produtoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(idInexistente))
            .ReturnsAsync((Produto)null);

        // Act Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(
            async () => await _produtoServiceMock.AtivarAsync(idInexistente)
        );
    }

    [TestMethod]
    public async Task AtualizarAsync_QuandoProdutoExistir_DeveAtualizarTodasAsPropriedadesECommitar()
    {
        //Arrange
        long produtoId = 10;

        var produtoOriginal = Produto.Criar("Nome Antigo", "VES-CAM-PM", "Desc Antiga", 10m, 20m, 1);

        var dtoNovo = new ProdutoDto
        {
            Nome = "Nome Atualizado",
            Sku = "VES-CAM-PM",
            Descricao = "Nova Descrição",
            PrecoCusto = 15.00m,
            PrecoVenda = 30.00m,
            CategoriaId = 2,
            Ativo = true
        };

        _produtoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(produtoId))
            .ReturnsAsync(produtoOriginal);

        //Act
        await _produtoServiceMock.AtualizarAsync(produtoId, dtoNovo);

        //Assert
        Assert.AreEqual(dtoNovo.Nome, produtoOriginal.Nome);
        Assert.AreEqual(dtoNovo.Sku, produtoOriginal.Sku);
        Assert.AreEqual(dtoNovo.Descricao, produtoOriginal.Descricao);
        Assert.AreEqual(dtoNovo.PrecoCusto, produtoOriginal.PrecoCusto);
        Assert.AreEqual(dtoNovo.PrecoVenda, produtoOriginal.PrecoVenda);
        Assert.AreEqual(dtoNovo.CategoriaId, produtoOriginal.CategoriaId);
        Assert.AreEqual(dtoNovo.Ativo, produtoOriginal.Ativo);

        _produtoRepositoryMock.Verify(
            repo => repo.AtualizarAsync(produtoOriginal),
            Times.Once
        );

        _produtoRepositoryMock.Verify(
            repo => repo.SalvarAlteracoesAsync(),
            Times.Once
        );
    }

    [TestMethod]
    public async Task UpdateAsync_QuandoProdutoNaoExistir_DeveLanciarNotFoundException()
    {
        //Arrange 
        long idInexistente = 999;
        var dto = new ProdutoDto
        {
            Nome = "Teste",
            Sku = "VES-TST-01",
            Descricao = "Desc",
            PrecoCusto = 10m,
            PrecoVenda = 20m,
            CategoriaId = 1
        };

        _produtoRepositoryMock
            .Setup(repo => repo.ObterPorIdAsync(idInexistente))
            .ReturnsAsync((Produto)null);

        //Act Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(
            async () => await _produtoServiceMock.AtualizarAsync(idInexistente, dto)
        );
    }

    [TestMethod]
    public async Task ObterTodasAsync_QuandoExistiremProdutos_DeveRetornarResultadoPaginadoEMapeado()
    {
        //Arrange
        int pagina = 1;
        int quantidadePorPagina = 10;
        int totalProdutosNoBanco = 15;
        var categoriaMock = Categoria.Criar("Logística", "Categoria de paletes");
        typeof(Categoria).GetProperty("Id")?.SetValue(categoriaMock, 5);

        
        var produtoMock = Produto.Criar(
            "Palete PBR",
            "VES-CAM-PM",
            "Palete de madeira",
            45m,
            90m,
            1
        );

        typeof(Produto).GetProperty("Id")?.SetValue(produtoMock, 1);
        typeof(Produto).GetProperty("Categoria")?.SetValue(produtoMock, categoriaMock);
        var listaProdutosMock = new List<Produto> { produtoMock };
        var retornoRepositorioMock = (listaProdutosMock, totalProdutosNoBanco);

        _produtoRepositoryMock
            .Setup(repo => repo.ObterTodasPaginadasAsync(pagina, quantidadePorPagina))
            .ReturnsAsync(retornoRepositorioMock);

        //Act
        var resultado = await _produtoServiceMock.ObterTodasAsync(pagina, quantidadePorPagina);

        //Assert
        Assert.IsNotNull(resultado);
        Assert.AreEqual(totalProdutosNoBanco, resultado.TotalItens);
        Assert.AreEqual(pagina, resultado.PaginaAtual);
        Assert.AreEqual(1, resultado.Itens.Count());

        
        var produtoMapeado = resultado.Itens.First();
        Assert.AreEqual(1, produtoMapeado.Id);
        Assert.AreEqual("Palete PBR", produtoMapeado.Nome);
        Assert.AreEqual("VES-CAM-PM", produtoMapeado.Sku);
        Assert.IsTrue(produtoMapeado.Ativo);

       
        Assert.IsNotNull(produtoMapeado.Categoria);
        Assert.AreEqual("Logística", produtoMapeado.Categoria.Nome);
        Assert.AreEqual("Categoria de paletes", produtoMapeado.Categoria.Descricao);

        Assert.AreEqual(1, produtoMapeado.Categoria.Id);

        _produtoRepositoryMock.Verify(
            repo => repo.ObterTodasPaginadasAsync(pagina, quantidadePorPagina),
            Times.Once
        );
    }
}
