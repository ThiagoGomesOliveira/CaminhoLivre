using CaminhoLivre.Modulo.Catalogo.Entities;

namespace CaminhoLivre.Modulo.Catalago.Tests.Entites;

[TestClass]
public class CategoriaTest
{
    [TestMethod]
    public void CriarCategoria_ComDadosValidos_DeveInstanciarOk()
    {
        //Arrange 
        var nome = "Eletrodomestico";

        //Act
        var categoria = new Categoria(nome);

        //Assert
        Assert.IsNotNull(categoria);
    }
}
