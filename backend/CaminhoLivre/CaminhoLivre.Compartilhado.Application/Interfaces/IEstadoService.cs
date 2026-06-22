using CaminhoLivre.Compartilhado.Application.DTOs;

namespace CaminhoLivre.Compartilhado.Application.Interfaces;

public interface IEstadoService
{
    Task<long> AdicionarEstadoAsync(EstadoDto estadoDto);
    Task<List<EstadoDto>> ObterEstados();
}
