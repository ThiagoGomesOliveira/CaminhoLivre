using CaminhoLivre.Compartilhado.Application.DTOs;

namespace CaminhoLivre.Compartilhado.Application.Interfaces;

public interface IMunicipioService
{
    Task<long> AdicionarEstadoAsync(MunicipioDto municipioDto);
    Task<List<MunicipioDto>> ObterMunicipios();
}
