using System.ComponentModel.DataAnnotations;

namespace CaminhoLivre.Compartilhado.Application.DTOs;

/// <summary>
/// Objeto de transferência de dados utilizado para munícipio.
/// </summary>
public class MunicipioDto
{
    /// <summary>
    /// Id do municipio.
    /// </summary>
    public long Id { get; init; }
    /// <summary>
    /// Nome do estado.
    /// </summary>
    /// <remarks>Deve ser claro e sem abreviações confusas.</remarks>
    /// <example>Blumenau</example>
    [Required(ErrorMessage = "O nome do municipio é obrigatório.")]
    public string Nome { get; init; } = string.Empty;
    /// <summary>
    ///Código do IBGE
    ///</summary>
    [Required(ErrorMessage = "O código do IBGE é obritaório.")]
    public long CodigoIbge { get; init; }
    /// <summary>
    /// Id do estado.
    /// </summary>
    public long EstadoId { get; init; }
}
