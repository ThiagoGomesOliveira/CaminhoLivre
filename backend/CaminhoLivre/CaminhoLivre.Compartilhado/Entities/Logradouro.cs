namespace CaminhoLivre.Compartilhado.Entities;

public class Logradouro
{
    public long Id { get; set; }
    public string Cep { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Rua { get; set; } = string.Empty;
    public long MunicipioId { get; set; }
    public Municipio Municipio { get; set; } = null!;

}
