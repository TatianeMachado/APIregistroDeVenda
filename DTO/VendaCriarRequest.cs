using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class VendaCriarRequest
{
    [Required(ErrorMessage = "O vendedor é obrigatório.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "Informe um identificador de vendedor válido.")]
    public int? VendedorId { get; set; }

    [Required(ErrorMessage = "Os itens da venda são obrigatórios.")]
    [MinLength(1,
        ErrorMessage = "A venda deve conter pelo menos um item.")]
    public List<ItemVendaCriarRequest> ItensVenda { get; set; } = new();
}