using CaminhoLivre.Compartilhado.Application.DTOs;
using CaminhoLivre.Compartilhado.Application.Services;
using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Compartilhado.Repositories;
using Moq;

namespace CaminhoLivre.Compartilhado.Tests.Services;

[TestClass]
public class MunicipioServiceTest
{
    private  Mock<IMunicipioRepository> _municipioRepositoryMock;

    [TestInitialize]
    public void Init()
    {
        _municipioRepositoryMock = new Mock<IMunicipioRepository>();
    }

    [TestMethod]
    public async Task AdicionarMunicipioAsync_DeveAdicionarMunicipio()
    {
        // Arrange
        var municipioRepositoryMock = new Mock<IMunicipioRepository>();
        var municipioService = new MunicipioService(municipioRepositoryMock.Object);
        var municipioDto = new MunicipioDto { Nome = "São Paulo", CodigoIbge = 3550308, EstadoId = 1 };

        municipioRepositoryMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Municipio>())).Returns(Task.CompletedTask);
        municipioRepositoryMock.Setup(repo => repo.SalvarAlteracoesAsync()).ReturnsAsync(true);

        // Act
        var result = await municipioService.AdicionarEstadoAsync(municipioDto);

        // Assert
        municipioRepositoryMock.Verify(repo => repo.AdicionarAsync(It.IsAny<Municipio>()), Times.Once);
        municipioRepositoryMock.Verify(repo => repo.SalvarAlteracoesAsync(), Times.Once);
    }

    [TestMethod]
    public async Task ObterMunicipios_DeveRetornarListaDeMunicipioDto()
    {
        //Arrange
        var municipioRepositoryMock = new Mock<IMunicipioRepository>();
        var municipioService = new MunicipioService(municipioRepositoryMock.Object);
        var municipios = new List<Municipio> 
        {
            Municipio.Criar("Municipio 1", 123, 1),
            Municipio.Criar("Municipio 2", 123, 1)
        };
        _municipioRepositoryMock.Setup(repo => repo.ObterTodosMunicipiosAsync())
            .ReturnsAsync(municipios);

        //Act
        var resultado = await municipioService.ObterMunicipios();

        //Assert
        Assert.IsNotNull(resultado);
    }
}
