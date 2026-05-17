namespace CaminhoLivre.Modulo.Catalogo.Entities;

public class Categoria
{
    protected Categoria()
    {

    }

    public Categoria(string nome)
    {
        if(string.IsNullOrEmpty(nome))
            throw new ArgumentException("O nome da categoria é obrigatório.", nameof(nome));

        Nome = nome;
        //Slug = GerarSlug(nome);
        Ativo = true;
        DataCadastro = DateTimeOffset.UtcNow;
    }

    private string? GerarSlug(string nome)
    {
        throw new NotImplementedException();
    }

    public long Id { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public bool Ativo { get; set; }
    public required DateTimeOffset DataCadastro { get; set; }
    public DateTimeOffset? DataAlteracao { get; set; }
    public required string  Slug { get; set; }

    private readonly List<Produto> _produtos = new();
    public IReadOnlyCollection<Produto> Produtos => _produtos.AsReadOnly();
}
