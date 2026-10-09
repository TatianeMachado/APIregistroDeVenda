using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class ComissaoCalcularRequest
{
    [Required]
    [MinLength(1, ErrorMessage = "Informe pelo menos uma venda.")]
    public List<VendaComissaoRequest> Vendas { get; set; } = new();
}