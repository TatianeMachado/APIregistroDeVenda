using System.ComponentModel.DataAnnotations;
using APIregistroDeVenda.Domain;

namespace APIregistroDeVenda.DTO;

public class MovimentacaoEstoqueCriarRequest
{
    [Required(ErrorMessage = "O código do produto é obrigatório.")]
    [StringLength(50)]
    public string? CodigoProduto { get; set; }

    [Required(ErrorMessage = "O tipo da movimentação é obrigatório.")]
    [EnumDataType(typeof(TipoMovimentacao))]
    public TipoMovimentacao? Tipo { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int? Quantidade { get; set; }
}