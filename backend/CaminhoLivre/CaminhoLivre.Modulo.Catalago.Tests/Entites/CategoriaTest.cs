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
        var descricao = "Categoria para eletrodomesticos";

        //Act
        var categoria = Categoria.Criar(nome, descricao);

        //Assert
        Assert.IsNotNull(categoria);
    }
}
