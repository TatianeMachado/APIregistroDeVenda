namespace APIregistroDeVenda.DTO;

public class VendaResponse
{
    public int VendaId { get; set; }

    public DateTime DataVenda { get; set; }

    public int VendedorId { get; set; }

    public string NomeVendedor { get; set; } = string.Empty;

    public List<ItemVendaResponse> ItensVenda { get; set; } = new();

    public decimal Total { get; set; }
}