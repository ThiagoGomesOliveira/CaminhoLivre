using CaminhoLivre.Modulo.Vendas.Enums;

namespace CaminhoLivre.Modulo.Vendas.Entities;

public class Pedido
{
    public Pedido()
    {
        DataCriacao = DateTimeOffset.UtcNow;
    }

    public long  Id { get; set; }
    public long ClienteId { get; private set; }
    public StatusPedido StatusPedido { get; private set; }
    public decimal ValorTotal { get; set; }
    public DateTimeOffset DataCriacao { get; set; }
    public required string EntregaLogradouro { get; set; }
    public required string EntregaNumero { get; set; }
    public required string EntregaCep { get; set; }
    public required string EntregaCidade { get; set; }
    public required string EntregaUf { get; set; }
}
