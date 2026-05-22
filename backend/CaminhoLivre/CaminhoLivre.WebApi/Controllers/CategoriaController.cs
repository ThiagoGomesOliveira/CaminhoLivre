using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaminhoLivre.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoriaController : Controller
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService) => _categoriaService = categoriaService;

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarCategoriaDto categoriaDto)
    {
        var id = await _categoriaService.CriarAsync(categoriaDto);
        return CreatedAtAction(nameof(Criar), new { id, });
    }
}
