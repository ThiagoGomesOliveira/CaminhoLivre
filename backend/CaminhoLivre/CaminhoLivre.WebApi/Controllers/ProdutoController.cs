using CaminhoLivre.Modulo.Catalogo.Application.DTOs;
using CaminhoLivre.Modulo.Catalogo.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CaminhoLivre.WebApi.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Tags("Catálogo - Gerenciamento de produtos")]
public class ProdutoController(IProdutoService produtoService) : Controller
{
    /// <summary>
    /// Cadastra um novo produto no catálogo gerencial do ERP.
    /// </summary>
    /// <remarks>
    /// ### 📋 Diretrizes e Regras de Negócio:
    /// 
    /// 1. **Padrão de SKU (Leis de Ouro):**
    ///    * Deve seguir rigorosamente o formato `[CATEGORIA]-[MODELO]-[VARIAÇÃO]` (Ex: `VES-CAM-PRM`).
    ///    * Apenas letras maiúsculas, números e hifens são aceitos.
    ///    * **Legibilidade:** É proibido o uso das letras **O** e **I** para evitar confusão com os números **0** e **1**.
    /// 
    /// 2. **Ciclo de Vida:**
    ///    * Todo produto é criado com o status **Ativo** por padrão, ficando imediatamente disponível para o estoque.
    /// </remarks>
    /// <param name="produtoDto">Objeto contendo os dados cadastrais do produto.</param>
    /// <returns>Retorna o identificador único numérico (ID) do produto recém-criado.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)] 
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]   
    public async Task<IActionResult> Criar([FromBody] ProdutoDto produtoDto)
    {
        var id = await produtoService.CriarAsync(produtoDto);

        return CreatedAtAction(nameof(Criar), new { id, });
    }

    /// <summary>
    /// Ativa o produto no catálogo, tornando-a disponível para uso.
    /// </summary>
    /// <param name="id">Identificador numérico (long) do produto.</param>
    /// <example>15</example>
    [HttpPatch("{id:long}/ativar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Ativar(long id)
    {
        // Proteção simples: IDs de banco de dados sequenciais começam em 1
        if (id <= 0)
            return BadRequest("ID inválido.");

        await produtoService.AtivarAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Desativa um produto no catalogo e impacta a visibilidade do mesmo.
    /// </summary>
    /// <param name="id">Identificador numérico (long) do produto a ser desativada.</param>
    /// <example>15</example>
    [HttpPatch("{id:long}/desativar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Desativar(long id)
    {
        if (id <= 0)
            return BadRequest("ID inválido.");

        await produtoService.DesativarAsync(id);
        return NoContent();
    }

    /// <summary>
    /// Atualiza os dados cadastrais de um produto existente existente.
    /// </summary>
    /// <param name="id">Identificador numérico (long) do produto. </param>
    /// <param name="dto">Novos dados para atualização.</param>
    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Atualizar(long id, [FromBody] ProdutoDto dto)
    {
        if (id <= 0)
            return BadRequest("ID inválido.");

        await produtoService.AtualizarAsync(id, dto);
        return NoContent();
    }

    /// <summary>
    /// Retorna uma lista paginada de todos os produtos cadastrado.
    /// </summary>
    /// <remarks>
    /// Utilize este endpoint para alimentar tabelas e grids gerenciais. 
    /// Os resultados são ordenados alfabeticamente pelo nome.
    /// </remarks>
    /// <param name="pagina">Número da página desejada (Inicia em 1).</param>
    /// <param name="quantidadePorPagina">Quantidade de registros por página (Padrão: 10, Máximo: 50).</param>
    [HttpGet]
    [ProducesResponseType(typeof(ResultadoPaginadoDto<ProdutoResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodas([FromQuery] int pagina = 1, [FromQuery] int quantidadePorPagina = 10)
    {
        // Proteção simples para evitar abusos de paginação pesada
        if (quantidadePorPagina > 50)
            quantidadePorPagina = 50;
        if (pagina < 1)
            pagina = 1;

        var resultado = await produtoService.ObterTodasAsync(pagina, quantidadePorPagina);

        return Ok(resultado);
    }
}