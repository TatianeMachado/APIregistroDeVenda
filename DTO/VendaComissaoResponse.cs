namespace APIregistroDeVenda.DTO;

public class VendaComissaoResponse
{
    public int VendaComissaoId { get; set; }

    public string Vendedor { get; set; } = string.Empty;

    public decimal Valor { get; set; }

    public decimal Comissao { get; set; }
}