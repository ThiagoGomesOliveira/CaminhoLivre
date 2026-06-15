using System.ComponentModel.DataAnnotations;
using CaminhoLivre.Modulo.Compras.Entities;

namespace CaminhoLivre.Modulo.Compras.Tests.Entites;

[TestClass]
public class FornecedorTest
{
    [TestMethod]
    public void CriarFornecedor_ComDadosValidos_DeveInstanciarOk()
    {
        //Arrange 
        var nomeFantasia = "EMPRESA TESTE ";
        var razaoSocial = "Empresa teste ltda";
        var cnpjCpf = "12345678000199";
        var email = "email@teste.com.br";
        var telefone = "9999999999";
        var logradouroId = 1;
        var numero = "10";
        var complemento = "Casa";

        //Act
        var fornecedorFake = Fornecedor.Criar(nomeFantasia, razaoSocial, cnpjCpf, email, telefone, logradouroId, numero, complemento);

        //Assert
        Assert.IsNotNull(fornecedorFake);
    }

    [TestMethod]
    public void CriarFornecedor_ComDadosInvalidos_DeveLancarExcecao()
    {
        //Arrange 
        var nomeFantasia = "";
        var razaoSocial = "";
        var cnpjCpf = "";
        var email = "emailinvalido";
        var telefone = "telefoneinvalido";
        var logradouroId = 0;
        var numero = "";
        var complemento = "";

        //Act & Assert
        Assert.ThrowsException<ValidationException>(() =>
            Fornecedor.Criar(nomeFantasia, razaoSocial, cnpjCpf, email, telefone, logradouroId, numero, complemento));
    }

    [TestMethod]
    public void CriarFornecedor_ComTelefoneInvalido_DeveLancarExcecao()
    {
        //Arrange 
        var nomeFantasia = "EMPRESA TESTE ";
        var razaoSocial = "Empresa teste ltda";
        var cnpjCpf = "12345678000199";
        var email = "teste@email.com";
        var telefone ="aaaaa";
        var logradouroId = 1;
        var numero = "10";
        var complemento = "Casa";

        //Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Fornecedor.Criar(nomeFantasia, razaoSocial, cnpjCpf, email, telefone, logradouroId, numero, complemento);
        });

        Assert.IsTrue(excecao.Message.Contains("O telefone do fornecedor deve conter apenas números e pode incluir um código de país opcional."));
    }

    [TestMethod]
    public void CriarFornecedor_ComEmailInvalido_DeveLancarExcecao() 
    {
        //Arrange 
        var nomeFantasia = "EMPRESA TESTE ";
        var razaoSocial = "Empresa teste ltda";
        var cnpjCpf = "12345678000199";
        var email = "emailtestecombr";
        var telefone = "9999999999";
        var logradouroId = 1;
        var numero = "10";
        var complemento = "Casa";


        //Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Fornecedor.Criar(nomeFantasia, razaoSocial, cnpjCpf, email, telefone, logradouroId, numero, complemento);
        });

        Assert.IsTrue(excecao.Message.Contains("O email do fornecedor deve ser um endereço de email válido."));
    }

    [TestMethod]
    public void CriarFornecedor_ComCnpjCpfInvalido_DeveLancarExcecao()
    {
        //Arrange 
        var nomeFantasia = "EMPRESA TESTE ";
        var razaoSocial = "Empresa teste ltda";
        var cnpjCpf = string.Empty;
        var email = "emailtestecombr";
        var telefone = "9999999999";
        var logradouroId = 1;
        var numero = "10";
        var complemento = "Casa";


        //Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Fornecedor.Criar(nomeFantasia, razaoSocial, cnpjCpf, email, telefone, logradouroId, numero, complemento);
        });

        Assert.IsTrue(excecao.Message.Contains("O CNPJ/CPF do fornecedor é obrigatório."));
    }

    [TestMethod]
    public void CriarFornecedor_RazaoSocialInvalido_DeveLancarExcecao()
    {
        //Arrange 
        var nomeFantasia = "EMPRESA TESTE ";
        var razaoSocial = string.Empty;
        var cnpjCpf = "010101010101";
        var email = "emailtestecombr";
        var telefone = "9999999999";
        var logradouroId = 1;
        var numero = "10";
        var complemento = "Casa";


        //Act & Assert
        var excecao = Assert.ThrowsException<ValidationException>(() =>
        {
            var produto = Fornecedor.Criar(nomeFantasia, razaoSocial, cnpjCpf, email, telefone, logradouroId, numero, complemento);
        });

        Assert.IsTrue(excecao.Message.Contains("Razão social fornecedor é obrigatório."));
    }
}