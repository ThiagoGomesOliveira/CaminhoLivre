using CaminhoLivre.Compartilhado.Entities;
using FluentValidation;

namespace CaminhoLivre.Compartilhado.Validators;

public class MunicipioValidator : AbstractValidator<Municipio>
{
    public MunicipioValidator()
    {
        RuleFor(m => m.Nome)
            .NotEmpty().WithMessage("O nome do município é obrigatório.");
        RuleFor(m => m.CodigoIbge)
            .GreaterThan(0).WithMessage("O código do IBGE deve ser um número positivo.");
        RuleFor(m => m.EstadoId)
            .GreaterThan(0).WithMessage("Necessário informar o id do estado");
    }
}
