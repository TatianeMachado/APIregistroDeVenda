using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIregistroDeVenda.Domain;

public class VendaComissao
{
    public int VendaComissaoId { get; set; }

    [Required]
    public string Vendedor { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Valor { get; set; }
}