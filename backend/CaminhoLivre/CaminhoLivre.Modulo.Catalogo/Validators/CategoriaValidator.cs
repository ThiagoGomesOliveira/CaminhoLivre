using CaminhoLivre.Modulo.Catalogo.Entities;
using FluentValidation;

namespace CaminhoLivre.Modulo.Catalogo.Validators;
public class CategoriaValidator : AbstractValidator<Categoria>
{
    public CategoriaValidator() 
    {
        RuleFor(c => c.Nome)
              .NotEmpty().WithMessage("O nome da categoria é obrigatório.");

    } 
}
