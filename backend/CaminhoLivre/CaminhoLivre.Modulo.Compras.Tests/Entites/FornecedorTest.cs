using CaminhoLivre.Modulo.Compras.Entities;

namespace CaminhoLivre.Modulo.Compras.Tests.Entites
{
    [TestClass]
    public class FornecedorTest
    {
        [TestMethod]
        public void CriarForcedor_ComDadosValidos_DeveInstanciarOk()
        {
            //Arrange
            var nome = "Fornecedor Teste";
            var telefone = "0888";
            var email = "email@teste";

            //Act
            var fornecedor = new Fornecedor(nome, email, telefone);

            //Assert
            Assert.IsNotNull(fornecedor);
        }
    }
}
