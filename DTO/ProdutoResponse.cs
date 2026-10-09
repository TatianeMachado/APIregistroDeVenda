namespace APIregistroDeVenda.DTO;

public class ProdutoResponse
{
    public int ProdutoId { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string CodigoProduto { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public decimal PrecoUnitario { get; set; }

    public int Estoque { get; set; }
}