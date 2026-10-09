using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.Domain;

public class MovimentacaoEstoque
{
    public int MovimentacaoEstoqueId { get; set; }

    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    public TipoMovimentacao Tipo { get; set; }

    [Required]
    [StringLength(300)]
    public string Descricao { get; set; } = string.Empty;

    [Range(1, int.MaxValue,
     ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [Range(0, int.MaxValue,
        ErrorMessage = "O estoque final não pode ser negativo.")]
    public int EstoqueFinal { get; set; }

    public DateTime DataMovimentacao { get; set; }
}