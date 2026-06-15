using System.ComponentModel.DataAnnotations;
using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Modulo.Compras.Validators;

namespace CaminhoLivre.Modulo.Compras.Entities;

public class Fornecedor
{
    private Fornecedor() { }

    public long Id { get; set; }
    public string NomeFantasia { get; set; }
    public string RazaoSocial { get; set; }
    public string CnpjCpf { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public long LogradouroId { get; set; }
    public Logradouro Logradouro { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public bool Ativo { get; set; }
    public DateTimeOffset DataCadastro { get; set; }

    public static Fornecedor Criar(string nomeFantasia, string razaoSocial, string cnpjCpf, string email, string telefone, long logradouroId, string numero, string complemento)
    {
        var fornecedor = new Fornecedor
        {
            NomeFantasia = nomeFantasia,
            RazaoSocial = razaoSocial,
            CnpjCpf = cnpjCpf,
            Email = email,
            Telefone = telefone,
            LogradouroId = logradouroId,
            Numero = numero,
            Complemento = complemento,
            Ativo = true,
            DataCadastro = DateTimeOffset.UtcNow
        };
        fornecedor.Validar();
        return fornecedor;
    }

    private void Validar()
    {
        var validator = new FornecedorValidator();
        var response = validator.Validate(this);
        if (!response.IsValid)
        {
            var errorMessages = string.Join("; ", response.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errorMessages);
        }
    }
}
