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
}
