namespace CaminhoLivre.Compartilhado.Entities;

public class Municipio
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public long CodigoIbge { get; set; }
    public long EstadoId { get; set; }
    public Estado Estado { get; set; } = null!;
}
