using APIregistroDeVenda.DTO;
using APIregistroDeVenda.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIregistroDeVenda.Controller;

[ApiController]
[Route("api/comissoes")]
public class Comissoes : ControllerBase
{
    private readonly ComissaoService _service;

    public Comissoes(ComissaoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(
        [FromBody] ComissaoCalcularRequest request)
    {
        int quantidade = await _service.CadastrarAsync(request);

        return StatusCode(StatusCodes.Status201Created, new
        {
            mensagem = "Vendas cadastradas com sucesso.",
            quantidadeVendas = quantidade
        });
    }

    [HttpGet]
    public async Task<ActionResult<VendasResponse>> Consultar()
    {
        var resultado = await _service.ListarVendasAsync();

        return Ok(resultado);
    }

    [HttpGet("faixas")]
    public async Task<ActionResult<List<ComissaoFaixaResponse>>> ConsultarPorFaixa()
    {
        var resultado = await _service.ConsultarPorFaixaAsync();

        return Ok(resultado);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<VendaComissaoResponse>> ConsultarPorId(
    int id)
    {
        var resultado = await _service.ConsultarPorIdAsync(id);

        if (resultado is null)
        {
            return NotFound(new
            {
                mensagem = "Venda não encontrada."
            });
        }

        return Ok(resultado);
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult<VendaComissaoResponse>> Atualizar(
    int id,
    [FromBody] VendaComissaoAtualizarRequest request)
    {
        var resultado = await _service.AtualizarAsync(id, request);

        if (resultado is null)
        {
            return NotFound(new
            {
                mensagem = "Venda não encontrada."
            });
        }

        return Ok(resultado);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        bool excluiu = await _service.ExcluirAsync(id);

        if (!excluiu)
        {
            return NotFound(new
            {
                mensagem = "Venda não encontrada."
            });
        }

        return NoContent();
    }
}
