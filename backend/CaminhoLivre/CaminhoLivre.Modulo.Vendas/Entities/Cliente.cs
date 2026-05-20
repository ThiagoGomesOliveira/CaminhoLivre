using CaminhoLivre.Modulo.Vendas.Enums;

namespace CaminhoLivre.Modulo.Vendas.Entities;

public class Cliente
{
    private readonly List<Endereco> _enderecos = [];
    public Cliente()
    {
        Ativo = true;
        DataCadastro = DateTimeOffset.UtcNow;
    }

    public  long Id { get; set; }
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public DateTimeOffset DataCadastro { get; set; }
    public required string CpfCnpj { get; set; }
    public required string Telefone { get; set; }
    public TipoCliente TipoCliente { get; set; }
    public IReadOnlyCollection<Endereco> Enderecos => _enderecos.AsReadOnly();  
    public bool Ativo { get; set; }
}
