using System.Text.RegularExpressions;
using CaminhoLivre.Modulo.Catalogo.Entities;
using FluentValidation;

namespace CaminhoLivre.Modulo.Catalogo.Validators;

public class ProdutoValidator : AbstractValidator<Produto>
{
    // Regex para validar o padrão: 3 letras/números, hífen, 3 letras/números, hífen, 2 a 4 letras/números
    // Exemplo válido: VES-CAM-PRM
    private static readonly Regex SkuRegex = new(@"^[A-Z0-9]{3}-[A-Z0-9]{3}-[A-Z0-9]{2,4}$", RegexOptions.Compiled);

    public ProdutoValidator()
    {

        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome do produto é obrigatório.");
        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("A descrição do produto é obrigatória.");
        RuleFor(p => p.PrecoCusto)
            .GreaterThan(0).WithMessage("O preço de custo do produto deve ser maior que zero.");
        RuleFor(p => p.PrecoVenda)
            .GreaterThan(0).WithMessage("O preço de venda do produto deve ser maior que zero.");
        RuleFor(p => p.CategoriaId)
            .GreaterThan(0).WithMessage("A categoria do produto é obrigatória.");
        RuleFor(p => p.Sku)
            .NotEmpty()
                .WithMessage("O SKU é obrigatório.")
            .MaximumLength(15)
                .WithMessage("O SKU deve ter no máximo 15 caracteres.")
            .Must(BeAValidSkuPattern)
                .WithMessage("O SKU informado está fora do padrão do ERP (Ex: VES-CAM-PRM). Use apenas maiúsculas, números e hifens.")
            .Must(NotContainConfusingCharacters)
                .WithMessage("Por questões de legibilidade operacional, o SKU não deve conter as letras 'O' ou 'I'.");
    }

    private bool BeAValidSkuPattern(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku)) 
            return false;

        return SkuRegex.IsMatch(sku);
    }

    private bool NotContainConfusingCharacters(string sku)
    {
        if (string.IsNullOrWhiteSpace(sku)) 
            return true;

        // Valida a Lei de Ouro: evitar 'O' e 'I' para não confundir com '0' e '1'
        return !sku.Contains('O') && !sku.Contains('I');
    }
}
