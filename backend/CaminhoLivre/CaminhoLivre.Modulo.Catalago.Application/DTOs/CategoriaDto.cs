using System.ComponentModel.DataAnnotations;

namespace CaminhoLivre.Modulo.Catalogo.Application.DTOs;
/// <summary>
/// Objeto de transferência de dados utilizado para a criação da categoria no catálogo.
/// </summary>
public record CategoriaDto 
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
    public required string Descricao { get; init; }
}

