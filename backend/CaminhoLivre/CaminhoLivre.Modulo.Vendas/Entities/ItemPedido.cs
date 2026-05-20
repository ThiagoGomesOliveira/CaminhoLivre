using CaminhoLivre.Modulo.Catalogo.Entities;

namespace CaminhoLivre.Modulo.Vendas.Entities;

public class ItemPedido
{
    public long Id { get; set; }
    public long PedidoId { get; set; }
    public long ProdutoId { get; set; }
    public long Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal ValorSubtotal { get; set; }
    public required Produto Produto { get; set; }
    public required Pedido Pedido { get; set; }
}
