namespace CaminhoLivre.Modulo.Catalogo.Application.DTOs;
/// <summary>
/// Dados de retorno detalhados de uma categoria.
/// </summary>
public record CategoriaResponseDto
{
    /// <summary>Identificador único da categoria no banco.</summary>
    public long Id { get; init; }
    /// <summary>Nome comercial da categoria.</summary>
    public string Nome { get; init; }
    /// <summary>Nome comercial da categoria.</summary>
    public string Descricao { get; init; }
    /// <summary>Indica se a categoria está disponível para vinculação e exibição pública.</summary>
    public bool Ativo { get; init; }
    /// <summary>Data crição da categoria</summary>
    public DateTimeOffset DataCadastro { get; init; }
    /// <summary>Data alteração da categoria</summary>
    public DateTimeOffset? DataAlteracao { get; init; }
    /// <summary>Slug da categoria</summary>
    public string Slug { get; init; }
}
