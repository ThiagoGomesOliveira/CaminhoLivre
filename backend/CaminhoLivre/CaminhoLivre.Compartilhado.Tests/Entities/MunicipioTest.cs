using System.ComponentModel.DataAnnotations;
using CaminhoLivre.Compartilhado.Entities;

namespace CaminhoLivre.Compartilhado.Tests.Entities;

[TestClass]
public class MunicipioTest
{

    [TestMethod]
    public void CriarMunicipio_Valido_DeveCriarMunicipio()
    {
        // Arrange
        var nome = "Blumenau";
        var codigoIbge = 4202404;
        var estadoId = 1;

        // Act
        var municipio = Municipio.Criar(nome, codigoIbge, estadoId);

        // Assert
        Assert.AreEqual(nome, municipio.Nome);
        Assert.AreEqual(codigoIbge, municipio.CodigoIbge);
        Assert.AreEqual(estadoId, municipio.EstadoId);
    }

    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void CriarMunicipio_NomeVazio_DeveLancarExcecao()
    {
        // Arrange
        var nome = "";
        var codigoIbge = 4202404;
        var estadoId = 1;

        // Act Assert
        Municipio.Criar(nome, codigoIbge, estadoId);
    }
}
