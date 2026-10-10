using System.ComponentModel.DataAnnotations;
using APIregistroDeVenda.Domain;

namespace APIregistroDeVenda.DTO;

public class MovimentacaoEstoqueAtualizarRequest
{
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [EnumDataType(typeof(TipoMovimentacao))]
    public TipoMovimentacao? Tipo { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue,
        ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int? Quantidade { get; set; }
}