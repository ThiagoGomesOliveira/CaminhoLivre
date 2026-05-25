namespace CaminhoLivre.Modulo.Catalogo.Application.DTOs;

/// <summary>
/// Objeto de transferência de dados que representa a resposta detalhada de um produto do catálogo.
/// </summary>
public class ProdutoResponseDto
{
    /// <summary>
    /// Identificador único numérico do produto no banco de dados.
    /// </summary>
    /// <example>1024</example>
    public long Id { get; init; }

    /// <summary>
    /// Nome comercial do produto.
    /// </summary>
    /// <example>Camiseta Polo Algodão Premium</example>
    public  string Nome { get; init; }

    /// <summary>
    /// Descrção do produto.
    /// </summary>
    /// <remarks>Deve ser claro e sem abreviações confusas.</remarks>
    /// <example>A Camiseta Básica Premium é a escolha perfeita para quem busca unir conforto, durabilidade e estilo no dia a dia. Confeccionada com o mais puro algodão brasileiro</example>
    public string Descricao { get; init; }

    /// <summary>
    /// Código identificador único de estoque (Stock Keeping Unit) formatado.
    /// </summary>
    /// <example>VES-CAM-PRM</example>
    public  string Sku { get; init; }

    /// <summary>
    /// Preço de custo pago ao fornecedor.
    /// </summary>
    /// <example>49.90</example>
    public decimal PrecoCusto { get; init; }

    /// <summary>
    /// Preço de venda praticado no ERP.
    /// </summary>
    /// <example>89.90</example>
    public decimal PrecoVenda { get; init; }

    /// <summary>
    /// Saldo atual disponível no estoque físico.
    /// </summary>
    /// <example>150</example>
    public int QuantidadeEstoque { get; init; }

    /// <summary>
    /// Indica se o produto está ativo e visível para operações comerciais.
    /// </summary>
    /// <example>true</example>
    public bool Ativo { get; init; }

    /// <summary>
    /// Identificador numérico da categoria vinculada.
    /// </summary>
    /// <example>15</example>
    public long CategoriaId { get; init; }

    /// <summary>
    /// Nome descritivo da categoria para exibição imediata no front-end.
    /// </summary>
    /// <example>Vestuário</example>
    public required string CategoriaNome { get; init; }
}