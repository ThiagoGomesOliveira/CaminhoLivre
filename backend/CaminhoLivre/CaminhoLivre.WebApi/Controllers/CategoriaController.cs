using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaminhoLivre.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Tags("Catálogo - Gerenciamento de Categorias")]
public class CategoriaController(ICategoriaService categoriaService) : Controller
{

    /// <summary>
    /// Cria uma nova categoria para agrupamento de produtos.
    /// </summary>
    /// <remarks>
    /// ### Regras de Negócio:
    /// * O **Nome** da categoria deve ser único no sistema para evitar duplicidade operacional.
    /// * Toda categoria nasce com o status `Ativo = true` por padrão.
    /// </remarks>
    /// <param name="dto">Dados para criação da categoria.</param>
    /// <returns>Retorna o ID da categoria criada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Criar([FromBody] CategoriaDto categoriaDto)
    {
        var id = await categoriaService.CriarAsync(categoriaDto);
        return CreatedAtAction(nameof(Criar), new { id, });
    }

    /// <summary>
    /// Retorna uma lista paginada de todas as categorias cadastradas.
    /// </summary>
    /// <remarks>
    /// Utilize este endpoint para alimentar tabelas e grids gerenciais. 
    /// Os resultados são ordenados alfabeticamente pelo nome.
    /// </remarks>
    /// <param name="pagina">Número da página desejada (Inicia em 1).</param>
    /// <param name="quantidadePorPagina">Quantidade de registros por página (Padrão: 10, Máximo: 50).</param>
    [HttpGet]
    [ProducesResponseType(typeof(ResultadoPaginadoDto<CategoriaResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodas([FromQuery] int pagina = 1, [FromQuery] int quantidadePorPagina = 10)
    {
        // Proteção simples para evitar abusos de paginação pesada
        if (quantidadePorPagina > 50)
            quantidadePorPagina = 50;

        if (pagina < 1)
            pagina = 1;

        var resultado = await categoriaService.ObterTodasAsync(pagina, quantidadePorPagina);

        return Ok(resultado);
    }

    /// <summary>
    /// Ativa uma categoria no catálogo, tornando-a disponível para uso.
    /// </summary>
    /// <param name="id">Identificador numérico (long) da categoria.</param>
    /// <example>15</example>
    [HttpPatch("{id:long}/ativar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Ativar(long id)
    {
        // Proteção simples: IDs de banco de dados sequenciais começam em 1
        if (id <= 0)
            return BadRequest("ID inválido.");

        await categoriaService.AtivarAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Desativa uma categoria e impacta a visibilidade dos produtos vinculados.
    /// </summary>
    /// <param name="id">Identificador numérico (long) da categoria a ser desativada.</param>
    /// <example>15</example>
    [HttpPatch("{id:long}/desativar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desativar(long id)
    {
        if (id <= 0)
            return BadRequest("ID inválido.");

        await categoriaService.DesativarAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Atualiza os dados cadastrais (Nome e Descrição) de uma categoria existente.
    /// </summary>
    /// <param name="id">Identificador numérico (long) da categoria.</param>
    /// <param name="dto">Novos dados para atualização.</param>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)] 
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]   
    [ProducesResponseType(StatusCodes.Status404NotFound)]  
    public async Task<IActionResult> Atualizar(long id, [FromBody] AtualizarCategoriaDto dto)
    {
        if (id <= 0) return BadRequest("ID inválido.");

        // Executa a atualização na Service. Se o nome for duplicado, a service joga um ConflictException
        await categoriaService.AtualizarAsync(id, dto);

        return NoContent();
    }
}
