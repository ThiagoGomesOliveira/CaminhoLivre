namespace CaminhoLivre.Modulo.Catalogo.Entities;

public class Produto
{
    public Produto()
    {
        
    }

    public Produto(string nome, string sku, string descricao, decimal precoCusto, decimal precoVenda)
    {
        if(string.IsNullOrEmpty(nome))
            throw new ArgumentException("O nome do produto é obrigatório.", nameof(nome));

        if (string.IsNullOrEmpty(sku))
            throw new ArgumentException("O SKU do produto é obrigatório.", nameof(sku));

        if (precoCusto <= 0)
            throw new ArgumentException("O preço de custo do produto deve ser maior que zero.", nameof(precoCusto));

        if(precoVenda <= 0)
            throw new ArgumentException("O preço de venda do produto deve ser maior que zero.", nameof(precoVenda));

        if (string.IsNullOrEmpty(descricao))
            throw new ArgumentException("Necessário a descrição do produto",nameof(descricao));

        Ativo = true;
        Sku = sku;
        Nome = nome;
        PrecoCusto = precoCusto;
        PrecoVenda = precoVenda;
        Descricao = descricao;
        DataCadastro = DateTimeOffset.UtcNow;
    }

    public long Id { get; set; }
    public required string Sku { get; set; }
    public required string Nome { get; set; }
    public required string Descricao { get; set; }
    public string CodigoBarras { get; set; }
    public required decimal PrecoVenda { get; set; }
    public required decimal PrecoCusto { get; set; }
    public decimal? PrecoPromocional { get; set; }
    public string Moeda { get; set; }
    public decimal PesoBruto { get; set; }
    public  string Ncm { get; set; }
    public  long Origem { get; set; }
    public bool Ativo { get; set; }
    public  DateTimeOffset DataCadastro { get; set; }
    public DateTimeOffset? DataAlteracao { get; set; }
    public required long CategoriaId { get; set; }
    public required Categoria Categoria { get; set; }
}
