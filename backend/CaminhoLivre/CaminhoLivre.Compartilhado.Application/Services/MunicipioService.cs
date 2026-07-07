using CaminhoLivre.Compartilhado.Application.DTOs;
using CaminhoLivre.Compartilhado.Application.Interfaces;
using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Compartilhado.Exceptions;
using CaminhoLivre.Compartilhado.Repositories;

namespace CaminhoLivre.Compartilhado.Application.Services;

public class MunicipioService(IMunicipioRepository municipioRepository) : IMunicipioService
{
    private readonly IMunicipioRepository _municipioRepository = municipioRepository;

    public async Task<long> AdicionarEstadoAsync(MunicipioDto municipioDto)
    {
        var municipio = Municipio.Criar(municipioDto.Nome, municipioDto.CodigoIbge, municipioDto.EstadoId);
        await _municipioRepository.AdicionarAsync(municipio);

        var sucesso = await _municipioRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new BusinessRuleException("Não foi possível salvar o município no banco de dados.");

        return municipio.Id;
    }

    public Task<List<MunicipioDto>> ObterMunicipios()
      => Task.Run(async () =>
      {
          var municipioDtoList = new List<MunicipioDto>();
          var municipios = await _municipioRepository.ObterTodosMunicipiosAsync();
          foreach (var municipio in municipios)
          {
              municipioDtoList.Add(new MunicipioDto
              {
                  Id = municipio.Id,
                  Nome = municipio.Nome,
                  CodigoIbge = municipio.CodigoIbge,
                  EstadoId = municipio.EstadoId
              });
          }
          return municipioDtoList;
      });
}
