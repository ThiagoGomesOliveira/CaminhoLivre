namespace CaminhoLivre.Compartilhado.Entities;

public class Estado
{
    private readonly List<Municipio> _municipios = [];
    public long Id { get; set; }
    public string Sigla { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public IReadOnlyCollection<Municipio> Municipios => _municipios.AsReadOnly();
}
