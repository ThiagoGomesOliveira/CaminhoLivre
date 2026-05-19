namespace CaminhoLivre.Modulo.Compras.Entities;

public class Fornecedor
{
    public Fornecedor()
    {
        
    }

    public Fornecedor(string nome,  string email, string telefone)
    {

        if (string.IsNullOrEmpty(nome))
            throw new ArgumentException("O nome do fornecedor é obrigatório.", nameof(nome));

        if(string.IsNullOrEmpty(email))
            throw new ArgumentException("O email do fornecedor é obrigatório.", nameof(email));

        if (string.IsNullOrEmpty(telefone))
            throw new ArgumentException("O telefone do fornecedor é obrigatório.", nameof(telefone));

        Nome = nome;
        Ativo = true;
        DataCadastro = DateTimeOffset.UtcNow;
    }

    public long Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public bool Ativo { get; set; }
    public DateTimeOffset DataCadastro { get; set; }
}
