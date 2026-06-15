using CaminhoLivre.Modulo.Compras.Entities;
using FluentValidation;

namespace CaminhoLivre.Modulo.Compras.Validators;

public class FornecedorValidator : AbstractValidator<Fornecedor>
{
    public FornecedorValidator()
    {
        RuleFor(f => f.NomeFantasia)
            .NotEmpty()
                .WithMessage("O nome fantasia do fornecedor é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O nome fantasia do fornecedor deve ter no máximo 100 caracteres.");
        RuleFor(f => f.Email)
            .NotEmpty()
                .WithMessage("O email do fornecedor é obrigatório.")
            .EmailAddress()
                .WithMessage("O email do fornecedor deve ser um endereço de email válido.");
        RuleFor(f => f.Telefone)
            .NotEmpty()
                .WithMessage("O telefone do fornecedor é obrigatório.")
            .Matches(@"^\+?\d{10,15}$")
                .WithMessage("O telefone do fornecedor deve conter apenas números e pode incluir um código de país opcional.");
        RuleFor(f => f.LogradouroId)
           .GreaterThan(0)
               .WithMessage("O logradouro do fornecedor é obrigatória.\"");
        RuleFor(f => f.CnpjCpf)
            .NotEmpty()
                .WithMessage("O CNPJ/CPF do fornecedor é obrigatório.");
        RuleFor(f => f.RazaoSocial)
            .NotEmpty()
               .WithMessage("Razão social fornecedor é obrigatório.");
    }
}
