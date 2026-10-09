using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class ItemVendaCriarRequest
{
    [Required(ErrorMessage = "O produto é obrigatório.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "Informe um identificador de produto válido.")]
    public int? ProdutoId { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int? Quantidade { get; set; }
}