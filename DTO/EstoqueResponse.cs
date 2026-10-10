namespace APIregistroDeVenda.DTO;

public class EstoqueResponse
{
    public List<ProdutoEstoqueResponse> Estoque { get; set; } = new();
}