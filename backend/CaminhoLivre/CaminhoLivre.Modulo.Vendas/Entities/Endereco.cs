using CaminhoLivre.Modulo.Vendas.Enums;

namespace CaminhoLivre.Modulo.Vendas.Entities;

public class Endereco
{
    public long Id { get; set; }
    public required long ClienteId { get; set; }
    public required string Logradouro { get; set; }
    public required string Numero { get; set; }
    public string? Complemento { get; set; }
    public required string  Bairro { get; set; }
    public required string Cidade { get; set; }
    public required string UF { get; set; }
    public required string Cep { get; set; }
    public bool EhPrincipal { get; set; }
    public TipoEndereco TipoEndereco { get; set; }
    public DateTimeOffset DataCriacao { get; set; }
    public required Cliente Cliente { get; set; }

}
