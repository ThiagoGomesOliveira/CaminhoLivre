using System.Threading.Tasks;
using CaminhoLivre.Compartilhado.Application.DTOs;
using CaminhoLivre.Compartilhado.Application.Interfaces;
using CaminhoLivre.Compartilhado.Entities;
using CaminhoLivre.Compartilhado.Exceptions;
using CaminhoLivre.Compartilhado.Repositories;

namespace CaminhoLivre.Compartilhado.Application.Services;

public class EstadoService(IEstadoRepository estadoRepository) : IEstadoService
{
    private readonly IEstadoRepository _estadoRepository = estadoRepository;

    public async Task<long> AdicionarEstadoAsync(EstadoDto estadoDto)
    {
        var estado = Estado.Criar(estadoDto.Nome, estadoDto.Sigla);
        await _estadoRepository.AdicionarAsync(estado);

        var sucesso = await _estadoRepository.SalvarAlteracoesAsync();
        if (!sucesso)
            throw new BusinessRuleException("Não foi possível salvar a categoria no banco de dados.");

        return estado.Id;
    }

    public async Task<List<EstadoDto>> ObterEstados()
    {
        var estadoDtoList = new List<EstadoDto>();
        var estados = await _estadoRepository.ObterTodosEstados();
        foreach (var estado in estados)
        {
            estadoDtoList.Add(new EstadoDto
            {
                Id = estado.Id,
                Nome = estado.Nome,
                Sigla = estado.Sigla
            });
        }

        return estadoDtoList;
    }
}
