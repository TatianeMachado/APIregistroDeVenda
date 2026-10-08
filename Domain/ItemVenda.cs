using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIregistroDeVenda.Domain;

public class ItemVenda
{
    [Key]
    public int ItemVendaId { get; set; }

    public int VendaId { get; set; }
    public Venda? Venda { get; set; }

    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }

    [Range(1, int.MaxValue,
      ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [Range(typeof(decimal), "0.01", "99999999.99",
    ErrorMessage = "O preço unitário deve estar entre 0,01 e 99.999.999,99.")]
    
    public decimal PrecoUnitario { get; set; }
}
