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
        Ativo = true;
        DataCadastro = DateTimeOffset.UtcNow;
    }

    public long Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
    public  DateTimeOffset DataCadastro { get; set; }
    public DateTimeOffset? DataAlteracao { get; set; }
    public string  Slug { get; set; }

    private readonly List<Produto> _produtos = [];
    public IReadOnlyCollection<Produto> Produtos => _produtos.AsReadOnly();
}
