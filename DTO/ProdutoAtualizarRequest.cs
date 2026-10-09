using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.DTO;

public class ProdutoAtualizarRequest
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100)]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(300)]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "O preço unitário é obrigatório.")]
    [Range(typeof(decimal), "0.01", "99999999.99",
        ErrorMessage = "O preço unitário deve estar entre 0,01 e 99.999.999,99.")]
    public decimal? PrecoUnitario { get; set; }

    [Range(0, int.MaxValue,
        ErrorMessage = "O estoque não pode ser negativo.")]
    public int? Estoque { get; set; }
}