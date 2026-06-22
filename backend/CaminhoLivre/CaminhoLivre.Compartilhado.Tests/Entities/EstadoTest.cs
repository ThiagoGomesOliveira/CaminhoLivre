using System.ComponentModel.DataAnnotations;
using CaminhoLivre.Compartilhado.Entities;

namespace CaminhoLivre.Compartilhado.Tests.Entities;

[TestClass]
public class EstadoTest
{
    [TestMethod]
    public void CriarEstado_ComDadosValidos_DeveInstanciarOk()
    {
        //Arrange 
        var nome = "São Paulo";
        var sigla = "SP";

        //Act
        var estado = Estado.Criar(nome, sigla);

        //Assert
        Assert.IsNotNull(estado);
    }

    [TestMethod]
    public void CriarEstado_ComDadosInvalidos_DeveLancarExcecao()
    {
        //Arrange 
        var nome = "";
        var sigla = "SP";

        //Act & Assert
        Assert.ThrowsException<ValidationException>(() => Estado.Criar(nome, sigla));
    }
}
