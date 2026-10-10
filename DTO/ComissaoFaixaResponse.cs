namespace APIregistroDeVenda.DTO;

public class ComissaoFaixaResponse
{
    public string Regra { get; set; } = string.Empty;

    public decimal Percentual { get; set; }

    public List<VendaComissaoResponse> Vendas { get; set; } = new();

    public decimal TotalComissao { get; set; }
}