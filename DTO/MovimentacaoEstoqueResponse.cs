using APIregistroDeVenda.Domain;

namespace APIregistroDeVenda.DTO;

public class MovimentacaoEstoqueResponse
{
    public int MovimentacaoEstoqueId { get; set; }

    public string CodigoProduto { get; set; } = string.Empty;

    public TipoMovimentacao Tipo { get; set; }

    public string Descricao { get; set; } = string.Empty;
    public string DescricaoProduto { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public int EstoqueFinal { get; set; }

    public DateTime DataMovimentacao { get; set; }
}