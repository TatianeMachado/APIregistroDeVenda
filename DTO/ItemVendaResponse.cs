namespace APIregistroDeVenda.DTO;

public class ItemVendaResponse
{
    public int ItemVendaId { get; set; }

    public int ProdutoId { get; set; }

    public string CodigoProduto { get; set; } = string.Empty;

    public string NomeProduto { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    public decimal Subtotal { get; set; }
}