using APIregistroDeVenda.DTO;
using APIregistroDeVenda.Service;
using Microsoft.AspNetCore.Mvc;

namespace APIregistroDeVenda.Controller;

[ApiController]
[Route("api/juros")]
public class Juros : ControllerBase
{
    private readonly JurosService _service;

    public Juros(JurosService service)
    {
        _service = service;
    }

    [HttpPost("calcular")]
    public ActionResult<JurosResponse> Calcular(
        [FromBody] JurosCalcularRequest request)
    {
        var juros = _service.Calcular(
            request.Valor!.Value,
            request.DataVencimento!.Value);

        return Ok(new JurosResponse
        {
            ValorJuros = juros
        });
    }
    [HttpGet("calcular")]
    public ActionResult<JurosResponse> CalcularGet(
    [FromQuery] JurosCalcularRequest request)
    {
        var juros = _service.Calcular(
            request.Valor!.Value,
            request.DataVencimento!.Value);

        return Ok(new JurosResponse
        {
            ValorJuros = juros
        });
    }
}