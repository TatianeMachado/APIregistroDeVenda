using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class JurosCalcularRequest
{
    [Required(ErrorMessage = "O valor é obrigatório.")]
    public decimal? Valor { get; set; }

    [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
    public DateOnly? DataVencimento { get; set; }
}