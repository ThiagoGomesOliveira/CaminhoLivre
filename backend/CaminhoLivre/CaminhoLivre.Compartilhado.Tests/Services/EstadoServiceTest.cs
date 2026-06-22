using CaminhoLivre.Compartilhado.Application.DTOs;
using CaminhoLivre.Compartilhado.Application.Services;
using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Compartilhado.Exceptions;
using CaminhoLivre.Compartilhado.Repositories;
using Moq;

namespace CaminhoLivre.Compartilhado.Tests.Services;

[TestClass]
public class EstadoServiceTest
{
    private Mock<IEstadoRepository> _estadoRepositoryMock;
    private EstadoService _estadoServiceFake;

    [TestInitialize]
    public void Init()
    {
        _estadoRepositoryMock = new Mock<IEstadoRepository>();
        _estadoServiceFake = new EstadoService(_estadoRepositoryMock.Object);
    }

    [TestMethod]
    public async Task CriarAsync_QuandoDadosForemValidosESalvarComSucesso_DeveRetornarOIdDoEstado()
    {
        //Arrange 
        var estadoDto = new EstadoDto
        {
            Nome = "Estado Teste",
            Sigla = "ET",
            Id = 1
        };

        _estadoRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync())
            .ReturnsAsync(true);

        //Act
        var resultadoId = await _estadoServiceFake.AdicionarEstadoAsync(estadoDto);

        //Assert
       Assert.IsNotNull(resultadoId);
    }

    [TestMethod]
    public async Task CriarAsync_QuandoBancoFalharAoSalvar_DeveLanciarBusinessRuleException()
    {
        //Arrange 
        var estadoDto = new EstadoDto
        {
            Nome = "Estado Teste",
            Sigla = "ET",
        };
        _estadoRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync())
            .ReturnsAsync(false);

        //Act & Assert
        await Assert.ThrowsExceptionAsync<BusinessRuleException>(async () =>
            await _estadoServiceFake.AdicionarEstadoAsync(estadoDto));
    }

    [TestMethod]
    public async Task ObterEstados_DeveRetornarListaDeEstadoDto()
    {
        //Arrange
        var estados = new List<Estado>
        {
            Estado.Criar("Estado 1", "E1"),
            Estado.Criar("Estado 2", "E2")
        };
        _estadoRepositoryMock.Setup(repo => repo.ObterTodosEstados())
            .ReturnsAsync(estados);

        //Act
        var resultado = await _estadoServiceFake.ObterEstados();

        //Assert
        Assert.IsNotNull(resultado);
        Assert.AreEqual(2, resultado.Count);
        Assert.AreEqual("Estado 1", resultado[0].Nome);
        Assert.AreEqual("E1", resultado[0].Sigla);
        Assert.AreEqual("Estado 2", resultado[1].Nome);
        Assert.AreEqual("E2", resultado[1].Sigla);
    }
}
