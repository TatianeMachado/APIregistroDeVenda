namespace APIregistroDeVenda.DTO;

public class ProdutoEstoqueResponse
{
    public string CodigoProduto { get; set; } = string.Empty;

    public string DescricaoProduto { get; set; } = string.Empty;

    public int Estoque { get; set; }
}