using System.ComponentModel.DataAnnotations;

namespace CaminhoLivre.Modulo.Catalogo.Application.DTOs;

/// <summary>
/// Objeto de transferência de dados utilizado para a criação de um novo produto no catálogo.
/// </summary>
public record ProdutoDto
{
    /// <summary>
    /// Nome comercial e descritivo do produto.
    /// </summary>
    /// <remarks>Deve ser claro e sem abreviações confusas.</remarks>
    /// <example>Camiseta Polo Algodão Premium</example>
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(150, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 150 caracteres.")]
    public required string Nome { get; init; }
    /// <summary>
    /// Descrção do produto.
    /// </summary>
    /// <remarks>Deve ser claro e sem abreviações confusas.</remarks>
    /// <example>A Camiseta Básica Premium é a escolha perfeita para quem busca unir conforto, durabilidade e estilo no dia a dia. Confeccionada com o mais puro algodão brasileiro</example>
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(1000, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 1000 caracteres.")]
    public required string Descricao { get; init; }
    /// <summary>
    /// Código identificador único de estoque (Stock Keeping Unit).
    /// </summary>
    /// <remarks>Deve seguir o padrão hifenizado do ERP: [CATEGORIA]-[MODELO]-[VARIAÇÃO]. Não deve conter as letras 'I' ou 'O'.</remarks>
    /// <example>VES-CAM-PRM</example>
    [Required(ErrorMessage = "O SKU é obrigatório.")]
    [RegularExpression(@"^[A-Z0-9]{3}-[A-Z0-9]{3}-[A-Z0-9]{2,4}$", ErrorMessage = "O SKU fornecido não segue o padrão do ERP (Ex: VES-CAM-PRM).")]
    public required string Sku { get; init; }
    /// <summary>
    /// Preço de venda final praticado no e-commerce ou balcão.
    /// </summary>
    /// <remarks>Regra de negócio: Deve ser estritamente maior que o preço de custo.</remarks>
    /// <example>89.90</example>
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço de venda deve ser maior que zero.")]
    public  required decimal PrecoVenda { get; init; }
    /// <summary>
    /// Preço pago ao fornecedor pela aquisição do produto.
    /// </summary>
    /// <example>49.90</example>
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço de venda deve ser maior que zero.")]
    public required decimal PrecoCusto { get; init; }
    /// <summary>
    /// Identificador único Id da Categoria à qual o produto pertence.
    /// </summary>
    /// <remarks>A categoria informada deve existir no banco de dados e estar com o status Ativo.</remarks>
    /// <example>e3b0c442-98fc-4c14-951a-7b3d16cae503</example>
    [Required(ErrorMessage = "A categoria vinculada é obrigatória.")]
    public required long CategoriaId { get; init; }
}

