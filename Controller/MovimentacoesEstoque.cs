using APIregistroDeVenda.DTO;
using APIregistroDeVenda.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIregistroDeVenda.Controller;

[ApiController]
[Route("api/movimentacoes-estoque")]
public class MovimentacoesEstoque : ControllerBase
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

    [HttpPut("{id}")]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> Atualizar(
     [FromRoute] int id,
     [FromBody] MovimentacaoEstoqueAtualizarRequest request)
    {
        if (id <= 0)
        {
            return BadRequest(new
            {
                mensagem = "O ID deve ser um número inteiro maior que zero."
            });
        }

        try
        {
            var resultado = await _service.AtualizarAsync(id, request);

            if (resultado is null)
            {
                return NotFound(new
                {
                    mensagem = "Movimentação não encontrada."
                });
            }

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
    [HttpGet("{id}")]
    public async Task<ActionResult<MovimentacaoEstoqueResponse>> ConsultarPorId(
    [FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(new
            {
                mensagem = "O ID deve ser um número inteiro maior que zero."
            });
        }

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

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Excluir([FromRoute] int id)
    {
        if (id <= 0)
        {
            return BadRequest(new
            {
                mensagem = "O ID deve ser um número inteiro maior que zero."
            });
        }

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