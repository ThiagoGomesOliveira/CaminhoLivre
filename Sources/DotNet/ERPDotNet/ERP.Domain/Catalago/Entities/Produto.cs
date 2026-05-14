namespace ERP.Domain.Catalago.Entities;

public class Produto
{
    public long  Id { get;  set; }
    public required string Sku { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public string? CodigoBarras { get; set; }
    public required decimal PrecoVenda { get; set; }
    public required decimal PrecoCusto { get; set; }
    public decimal? PrecoPromocional { get; set; }
    public string? Moeda { get; set; }
    public  decimal? PesoBruto { get; set; }
    public required string Ncm { get; set; }
    public required long Origem { get; set; }
    public bool Ativo { get; set; }
    public required DateTimeOffset DataCadastro { get; set; }
    public DateTimeOffset? DataAlteracao { get; set; }
}
