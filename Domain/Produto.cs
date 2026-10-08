using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIregistroDeVenda.Domain;

public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(50)]
    public string? CodigoProduto { get; set; }

    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }

    
    [Column(TypeName = "decimal(10,2)")]
    [Range(typeof(decimal), "0.01", "99999999.99",
    ErrorMessage = "O preço unitário deve estar entre 0,01 e 99.999.999,99.")]
    public decimal PrecoUnitario { get; set; }

    [Range(0, int.MaxValue,
    ErrorMessage = "O estoque não pode ser negativo.")]
    public int Estoque { get; set; }

    public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();
}
