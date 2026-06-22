using System.ComponentModel.DataAnnotations;
using System.Runtime.ConstrainedExecution;
using CaminhoLivre.Compartilhado.Validators;

namespace CaminhoLivre.Compartilhado.Entities;

public class Estado
{
    private Estado(){}

    private readonly List<Municipio> _municipios = [];
    public long Id { get; set; }
    public string Sigla { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public IReadOnlyCollection<Municipio> Municipios => _municipios.AsReadOnly();

    public static Estado Criar(string nome, string sigla)
    {
        var estado = new Estado
        {
            Nome = nome,
            Sigla = sigla
        };

        estado.Validar();
        return estado;
    }

    private void Validar()
    {
        var validator = new EstadoValidator();
        var result = validator.Validate(this);
        if (!result.IsValid)
        {
            var errorMessages = string.Join("; ", result.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errorMessages);
        }
    }
}
