using System.ComponentModel.DataAnnotations;
using CaminhoLivre.Modulo.Catalogo.Validators;

namespace CaminhoLivre.Modulo.Catalogo.Entities;
public class Categoria
{
    private Categoria(){}

    private readonly List<Produto> _produtos = [];
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
    public  DateTimeOffset DataCadastro { get; set; }
    public DateTimeOffset? DataAlteracao { get; set; }
    public string  Slug { get; set; }
    
    public IReadOnlyCollection<Produto> Produtos => _produtos.AsReadOnly();

    public static Categoria Criar(string nome, string descricao)
    {

        var categoria = new Categoria
        {
            Nome = nome,
            Descricao = descricao,
            Ativo = true,
            DataCadastro = DateTimeOffset.UtcNow
        };

        categoria.Validar();
        return categoria;
    }
    private void Validar()
    {
        var validator = new CategoriaValidator();
        var result = validator.Validate(this);

        if (!result.IsValid)
        {
            var errorMessages = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errorMessages);
        }
    }

    public void Ativar() 
    {
        Ativo = true;
        DataAlteracao = DateTimeOffset.UtcNow;
    }

    public void Desativar() 
    {
        Ativo = false;
        DataAlteracao = DateTimeOffset.UtcNow;
    }
}
