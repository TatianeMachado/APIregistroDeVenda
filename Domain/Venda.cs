namespace APIregistroDeVenda.Domain;

public class Venda
{
    public int VendaId { get; set; }
    public DateTime DataVenda { get; set; }

    public int VendedorId { get; set; }
    public Vendedor? Vendedor { get; set; }

    public ICollection<ItemVenda> ItensVenda { get; set; } = new List<ItemVenda>();
}
