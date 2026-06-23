using System.ComponentModel.DataAnnotations;
using CaminhoLivre.Compartilhado.Validators;

namespace CaminhoLivre.Compartilhado.Entities;

public class Municipio
{
    private Municipio(){}
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public long CodigoIbge { get; set; }
    public long EstadoId { get; set; }
    public Estado Estado { get; set; } = null!;

    public static Municipio Criar(string nome, long codigoIbge, long estadoId)
    {
        var municipio = new Municipio
        {
            Nome = nome,
            CodigoIbge = codigoIbge,
            EstadoId = estadoId
        };
        municipio.Validar();
        return municipio;
    }

    private void Validar()
    {
        var validator = new MunicipioValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errorMessages = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errorMessages);
        }
    }
}
