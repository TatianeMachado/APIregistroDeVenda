using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class VendaComissaoRequest
{
    [Required(ErrorMessage = "O vendedor é obrigatório.")]
    public string? Vendedor { get; set; }

    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(typeof(decimal), "0.01", "99999999.99",
        ErrorMessage = "O valor da venda deve ser positivo.")]
    public decimal? Valor { get; set; }
}