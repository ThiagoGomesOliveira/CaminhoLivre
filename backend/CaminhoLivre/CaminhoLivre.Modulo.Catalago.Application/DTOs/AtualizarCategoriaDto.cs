using System.ComponentModel.DataAnnotations;

namespace CaminhoLivre.Modulo.Catalogo.Application.DTOs;

public record AtualizarCategoriaDto
{
    /// <summary>
    /// Nome comercial e descritivo da categoria.
    /// </summary>
    /// <remarks>Deve ser claro e sem abreviações confusas.</remarks>
    /// <example>Vestuário</example>
    [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public required string Nome { get; init; }

    /// <summary>
    /// Uma leve descrição do que se trata a categoria
    /// </summary>
    /// <remarks>Deve ser claro e sem abreviações confusas.</remarks>
    /// <example> Formal, casual, festa, moda praia ou esportiva</example>
    [Required(ErrorMessage = "A descrição da categoria é obrigatório.")]
    [StringLength(500, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Descricao { get; init; }

    /// <summary>
    /// Indica se a categoria está ativa.
    /// </summary>
    /// <example>true</example>
    public bool Ativo { get; init; }

}
