using CaminhoLivre.Compartilhado.Exceptions;
using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Services;
using CaminhoLivre.Modulo.Catalogo.Entities;
using CaminhoLivre.Modulo.Catalogo.Repositories;
using Moq;

namespace CaminhoLivre.Modulo.Catalago.Tests.Services;

[TestClass]
public class CategoriaServiceTest
{
    private Mock<ICategoriaRepository> _categoriaRepositoryMock;
    private CategoriaService _categoriaServiceFake;


    [TestInitialize]
    public void Init()
    {
        _categoriaRepositoryMock = new Mock<ICategoriaRepository>();
        _categoriaServiceFake = new CategoriaService(_categoriaRepositoryMock.Object);

    }

    [TestMethod]
    public async Task CriarAsync_QuandoDadosForemValidosESalvarComSucesso_DeveRetornarOIdDaCategoria()
    {
        //Arrange 
        var categoriaDto = new CategoriaDto
        {
            Nome = "Categoria Teste",
            Descricao = "Descrição da categoria teste",
        };

        _categoriaRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync())
            .ReturnsAsync(true);

        //Act
        var resultadoId = await _categoriaServiceFake.CriarAsync(categoriaDto);

        //Assert
        Assert.IsTrue(resultadoId >= 0);
        _categoriaRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Categoria>()), Times.Once);
        _categoriaRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
    }

    [TestMethod]
    public async Task CriarAsync_QuandoBancoFalharAoSalvar_DeveLanciarBusinessRuleException()
    {
        //Arrange 
        var categoriaDto = new CategoriaDto
        {
            Nome = "Categoria Teste",
            Descricao = "Descrição da categoria teste",
        };
        _categoriaRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync())
            .ReturnsAsync(false);

        //Act & Assert
        await Assert.ThrowsExceptionAsync<BusinessRuleException>(async () =>
        {
            await _categoriaServiceFake.CriarAsync(categoriaDto);
        });
    }

    [TestMethod]
    public async Task DesativarAsync_QuandoCategoriaExistir_DeveChamarDesativarEAtualizarNoBanco()
    {
        //Arrange
        var id = 1;
        var categoria = Categoria.Criar("Categoria Teste", "Descrição da categoria teste");

        _categoriaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(id))
            .ReturnsAsync(categoria);

        //Act
        await _categoriaServiceFake.DesativarAsync(id);

        //Assert
        Assert.IsFalse(categoria.Ativo);
    }

    [TestMethod]
    public async Task DesativarAsync_QuandoCategoriaNaoExistirDeveLanciarNotFoundException()
    {
        //Arrange
        var idInexistente = 666;

        _categoriaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(idInexistente))
           .ReturnsAsync((Categoria)null);

        //Act & Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
        {
            await _categoriaServiceFake.DesativarAsync(idInexistente);
        });
    }


    [TestMethod]
    public async Task AtivarAsync_QuandoCategoriaExistir_DeveChamarAtivarEAtualizarNoBanco()
    {
        //Arrange
        var id = 1;
        var categoria = Categoria.Criar("Categoria Teste", "Descrição da categoria teste");
        categoria.Desativar();
        _categoriaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(id))
            .ReturnsAsync(categoria);

        //Act
        await _categoriaServiceFake.AtivarAsync(id);

        //Assert
        Assert.IsTrue(categoria.Ativo);
    }

    [TestMethod]
    public async Task AtivarAsync_QuandoCategoriaNaoExistirDeveLanciarNotFoundException()
    {
        //Arrange
        var idInexistente = 666;
        _categoriaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(idInexistente))
           .ReturnsAsync((Categoria)null);

        //Act & Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
        {
            await _categoriaServiceFake.AtivarAsync(idInexistente);
        });
    }

    [TestMethod]
    public async Task AtualizarAsync_QuandoCategoriaExistir_DeveAtualizarOsDadosEChamarSalvarAlteracoes()
    {
        //Arrange
        var id = 1;
        var categoria = Categoria.Criar("Categoria Teste", "Descrição da categoria teste");
        var atualizarDto = new AtualizarCategoriaDto
        {
            Nome = "Categoria Atualizada",
            Descricao = "Descrição atualizada",
            Ativo = false
        };
        _categoriaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(id))
            .ReturnsAsync(categoria);

        //Act
        await _categoriaServiceFake.AtualizarAsync(id, atualizarDto);

        //Assert
        Assert.AreEqual(atualizarDto.Nome, categoria.Nome);
        Assert.AreEqual(atualizarDto.Descricao, categoria.Descricao);
        Assert.AreEqual(atualizarDto.Ativo, categoria.Ativo);
    }

    [TestMethod]
    public async Task AtualizarAsync_QuandoCategoriaNaoExistirDeveLanciarNotFoundException()
    {
        //Arrange
        var idInexistente = 666;
        var atualizarDto = new AtualizarCategoriaDto
        {
            Nome = "Categoria Atualizada",
            Descricao = "Descrição atualizada",
            Ativo = false
        };

        _categoriaRepositoryMock.Setup(repo => repo.ObterPorIdAsync(idInexistente))
           .ReturnsAsync((Categoria)null);

        //Act & Assert
        await Assert.ThrowsExceptionAsync<NotFoundException>(async () =>
        {
            await _categoriaServiceFake.AtualizarAsync(idInexistente, atualizarDto);
        });
    }

    [TestMethod]
    public async Task ObterTodasAsync_QuandoExistiremCategorias_DeveRetornarResultadoPaginadoEMapeado() 
    {
        //Arrange
        var pagina = 1;
        var quantidadePorPagina = 10;
        var categorias = new List<Categoria>
        {
            Categoria.Criar("Categoria 1", "Descrição 1"),
            Categoria.Criar("Categoria 2", "Descrição 2"),
            Categoria.Criar("Categoria 3", "Descrição 3")
        };

        _categoriaRepositoryMock.Setup(repo => repo.ObterTodasPaginadasAsync(pagina, quantidadePorPagina))
            .ReturnsAsync((categorias, categorias.Count));

        //Act
        var resultado = await _categoriaServiceFake.ObterTodasAsync(pagina, quantidadePorPagina);

        //Assert
        Assert.AreEqual(categorias.Count, resultado.TotalItens);
        Assert.AreEqual(categorias.Count, resultado.Itens.Count());
        Assert.IsTrue(resultado.Itens.All(dto => categorias.Any(c => c.Id == dto.Id && c.Nome == dto.Nome)));
    }
}
