using APIregistroDeVenda.DTO;
using APIregistroDeVenda.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIregistroDeVenda.Controller;

[ApiController]
[Route("api/movimentacoes-estoque")]
public class MovimentacoesEstoque: ControllerBase
{
    private readonly MovimentacaoEstoqueService _service;

    public MovimentacoesEstoque(
        MovimentacaoEstoqueService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(
        typeof(MovimentacaoEstoqueResponse),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Criar(
        [FromBody] MovimentacaoEstoqueCriarRequest request)
    {
        try
        {
            var resultado = await _service.CriarAsync(request);

            return StatusCode(
                StatusCodes.Status201Created,
                resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new
            {
                mensagem = ex.Message
            });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new
            {
                mensagem = ex.Message
            });
        }
        catch (OverflowException)
        {
            return BadRequest(new
            {
                mensagem = "O estoque resultante ultrapassa o limite numérico permitido."
            });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Atualizar(
    int id,
    [FromBody] MovimentacaoEstoqueAtualizarRequest request)
    {
        try
        {
            var resultado = await _service.AtualizarAsync(id, request);

            if (resultado is null)
                return NotFound(new { mensagem = "Movimentação não encontrada." });

            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { mensagem = ex.Message });
        }
        catch (OverflowException)
        {
            return BadRequest(new
            {
                mensagem = "O estoque resultante ultrapassa o limite numérico permitido."
            });
        }
    }
    [HttpGet]
    public async Task<ActionResult<EstoqueResponse>> Consultar()
    {
        var resultado = await _service.ConsultarEstoqueAsync();

        return Ok(resultado);
    }
    [HttpGet("historico")]
    public async Task<ActionResult<List<MovimentacaoEstoqueResponse>>> ConsultarHistorico()
    {
        return Ok(await _service.ConsultarAsync());
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> ConsultarPorId(
    int id)
    {
        var resultado = await _service.ConsultarPorIdAsync(id);

        if (resultado is null)
        {
            return NotFound(new
            {
                mensagem = "Movimentação não encontrada."
            });
        }

        return Ok(resultado);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            bool excluiu = await _service.ExcluirAsync(id);

            if (!excluiu)
            {
                return NotFound(new
                {
                    mensagem = "Movimentação não encontrada."
                });
            }

            return NoContent();
        }
        catch (OverflowException)
        {
            return BadRequest(new
            {
                mensagem = "O estoque resultante ultrapassa o limite numérico permitido."
            });
        }
    }
}