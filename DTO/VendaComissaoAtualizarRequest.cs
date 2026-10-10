using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class VendaComissaoAtualizarRequest
{
    [Required(ErrorMessage = "O vendedor é obrigatório.")]
    public string? Vendedor { get; set; }

    [Required(ErrorMessage = "O valor é obrigatório.")]
    public decimal? Valor { get; set; }
}