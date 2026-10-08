using System.ComponentModel.DataAnnotations;

namespace APIregistroDeVenda.Domain;

public class Vendedor
{
    [Key]
    public int VendedorId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Nome { get; set; }

    public ICollection<Venda> Vendas { get; set; } = new List<Venda>();
}
