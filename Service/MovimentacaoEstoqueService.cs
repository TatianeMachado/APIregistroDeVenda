using System.Data;
using APIregistroDeVenda.Context;
using APIregistroDeVenda.Domain;
using APIregistroDeVenda.DTO;
using Microsoft.EntityFrameworkCore;

namespace APIregistroDeVenda.Service;

public class MovimentacaoEstoqueService
{
    private readonly AppDbContext _context;

    public MovimentacaoEstoqueService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<MovimentacaoEstoqueResponse> CriarAsync(
        MovimentacaoEstoqueCriarRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.CodigoProduto))
            throw new ArgumentException("Informe o código do produto.");

        if (request.Quantidade is null || request.Quantidade <= 0)
            throw new ArgumentException(
                "A quantidade deve ser maior que zero.");

        if (request.Tipo != TipoMovimentacao.Entrada &&
            request.Tipo != TipoMovimentacao.Saida)
        {
            throw new ArgumentException(
                "Informe entrada ou saída.");
        }

        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable);

        var produto = await _context.Produtos
            .SingleOrDefaultAsync(
                p => p.CodigoProduto == request.CodigoProduto);

        if (produto is null)
            throw new KeyNotFoundException("Produto não encontrado.");

        int quantidade = request.Quantidade.Value;
        TipoMovimentacao tipo = request.Tipo.Value;

        // checked impede ultrapassar a capacidade numérica do int.
        int estoqueFinal = tipo == TipoMovimentacao.Entrada
            ? checked(produto.Estoque + quantidade)
            : checked(produto.Estoque - quantidade);

        produto.Estoque = estoqueFinal;

        var movimentacao = new MovimentacaoEstoque
        {
            ProdutoId = produto.ProdutoId,
            Tipo = tipo,
            Descricao = tipo == TipoMovimentacao.Entrada
                ? "Entrada de mercadoria"
                : "Saída de mercadoria",
            Quantidade = quantidade,
            EstoqueFinal = estoqueFinal,
            DataMovimentacao = DateTime.UtcNow
        };

        _context.MovimentacoesEstoque.Add(movimentacao);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new MovimentacaoEstoqueResponse
        {
            MovimentacaoEstoqueId =
                movimentacao.MovimentacaoEstoqueId,
            CodigoProduto = produto.CodigoProduto!,
            Tipo = movimentacao.Tipo,
            Descricao = movimentacao.Descricao,
            Quantidade = movimentacao.Quantidade,
            EstoqueFinal = movimentacao.EstoqueFinal,
            DataMovimentacao = movimentacao.DataMovimentacao
        };
    }
}