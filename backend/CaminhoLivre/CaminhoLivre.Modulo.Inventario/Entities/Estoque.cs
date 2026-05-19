using CaminhoLivre.Modulo.Catalogo.Entities;

namespace CaminhoLivre.Modulo.Inventario.Entities;

public class Estoque
{
    public  long Id { get; set; }
    public required long ProdutoId { get; set; }
    public long QtdDisponivel { get; set; }
    public long QtdReservada { get; set; }
    public required Produto Produto { get; set; }
}
