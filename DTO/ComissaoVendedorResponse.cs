namespace APIregistroDeVenda.DTO;

public class ComissaoVendedorResponse
{
    public string Vendedor { get; set; } = string.Empty;

    public int QuantidadeVendas { get; set; }

    public decimal TotalVendas { get; set; }

    public decimal TotalComissao { get; set; }
}