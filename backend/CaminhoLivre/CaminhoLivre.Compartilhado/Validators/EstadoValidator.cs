using CaminhoLivre.Compartilhado.Entities;
using FluentValidation;

namespace CaminhoLivre.Compartilhado.Validators;

public class EstadoValidator  : AbstractValidator<Estado>
{
    public EstadoValidator()
    {
        RuleFor(e => e.Nome)
            .NotEmpty().WithMessage("O nome do estado é obrigatório.");

        RuleFor(e => e.Sigla)
            .NotEmpty().WithMessage("A sigla do estado é obrigatória.")
            .Length(2).WithMessage("A sigla deve conter apenas dois caracteres.");
    }
}
