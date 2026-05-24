using System.ComponentModel.DataAnnotations;
using CaminhoLivre.Modulo.Catalogo.Validators;

namespace CaminhoLivre.Modulo.Catalogo.Entities;

public class Produto
{
    private Produto(){}
    public long Id { get; set; }
    public string Sku { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public string CodigoBarras { get; set; }
    public decimal PrecoVenda { get; set; }
    public decimal PrecoCusto { get; set; }
    public decimal? PrecoPromocional { get; set; }
    public string Moeda { get; set; }
    public decimal PesoBruto { get; set; }
    public string Ncm { get; set; }
    public long Origem { get; set; }
    public bool Ativo { get; set; }
    public DateTimeOffset DataCadastro { get; set; }
    public DateTimeOffset? DataAlteracao { get; set; }
    public long CategoriaId { get; set; }
    public Categoria Categoria { get; set; }


    public static Produto Criar(string nome, string sku, string descricao, decimal precoCusto, decimal precoVenda, long categoriaId)
    {

        var produto = new Produto
        {
            Ativo = true,
            Sku = sku,
            Nome = nome,
            PrecoCusto = precoCusto,
            PrecoVenda = precoVenda,
            Descricao = descricao,
            DataCadastro = DateTimeOffset.UtcNow,
            CategoriaId = categoriaId
        };

        produto.Validar();
        return produto;
    }

    private void Validar() 
    {
        var validator = new ProdutoValidator();
        var response = validator.Validate(this);

        if (!response.IsValid)
        {
            var errorMessages = string.Join("; ", response.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errorMessages);
        }
    }

    public void Ativar() 
    {
        Ativo = false;
    }

    public void Desativar()
    {
        Ativo = false;
    }
}
