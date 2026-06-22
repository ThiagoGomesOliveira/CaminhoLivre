using System.ComponentModel.DataAnnotations;

namespace CaminhoLivre.Compartilhado.Application.DTOs;

/// <summary>
/// Objeto de transferência de dados utilizado para a criação do estado.
/// </summary>
public class EstadoDto
{
    /// <summary>
    /// Id do estado.
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Nome do estado.
    /// </summary>
    /// <remarks>Deve ser claro e sem abreviações confusas.</remarks>
    /// <example>Santa Catarina</example>
    [Required(ErrorMessage = "O nome do estado é obrigatório.")]
    public required string Nome { get; init; }
    /// <summary>
    /// Sigla do estado.
    /// </summary>
    /// <exampleSC</example>
    [Required(ErrorMessage = "Sigla do estado é obrigatório.")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "A sigla deve conter apenas dois caracteres")]
    public required string Sigla { get; init; }
}
