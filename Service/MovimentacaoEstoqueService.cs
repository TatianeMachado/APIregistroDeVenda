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
            DescricaoProduto = produto.Descricao!,
            Tipo = movimentacao.Tipo,
            Descricao = movimentacao.Descricao,
            Quantidade = movimentacao.Quantidade,
            EstoqueFinal = movimentacao.EstoqueFinal,
            DataMovimentacao = movimentacao.DataMovimentacao
        };
    }
    public async Task<MovimentacaoEstoqueResponse?> AtualizarAsync(
       int id,
       MovimentacaoEstoqueAtualizarRequest request)
    {
        if (request.Quantidade is null || request.Quantidade <= 0)
        {
            throw new ArgumentException(
                "A quantidade deve ser maior que zero.");
        }

        if (request.Tipo != TipoMovimentacao.Entrada &&
            request.Tipo != TipoMovimentacao.Saida)
        {
            throw new ArgumentException("Informe entrada ou saída.");
        }

        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable);

        var movimentacao = await _context.MovimentacoesEstoque
            .SingleOrDefaultAsync(m => m.MovimentacaoEstoqueId == id);

        if (movimentacao is null)
        {
            return null;
        }

        var produto = await _context.Produtos
            .SingleAsync(p => p.ProdutoId == movimentacao.ProdutoId);

        var posteriores = await _context.MovimentacoesEstoque
            .Where(m =>
                m.ProdutoId == movimentacao.ProdutoId &&
                m.MovimentacaoEstoqueId > movimentacao.MovimentacaoEstoqueId)
            .OrderBy(m => m.MovimentacaoEstoqueId)
            .ToListAsync();

        long efeitoAnterior =
            movimentacao.Tipo == TipoMovimentacao.Entrada
                ? movimentacao.Quantidade
                : -(long)movimentacao.Quantidade;

        TipoMovimentacao tipoNovo = request.Tipo.Value;
        int quantidadeNova = request.Quantidade.Value;

        long efeitoNovo =
            tipoNovo == TipoMovimentacao.Entrada
                ? quantidadeNova
                : -(long)quantidadeNova;

        long diferenca = efeitoNovo - efeitoAnterior;

        movimentacao.Tipo = tipoNovo;
        movimentacao.Quantidade = quantidadeNova;
        movimentacao.Descricao =
            tipoNovo == TipoMovimentacao.Entrada
                ? "Entrada de mercadoria"
                : "Saída de mercadoria";

        movimentacao.EstoqueFinal = checked(
            (int)((long)movimentacao.EstoqueFinal + diferenca));

        foreach (var posterior in posteriores)
        {
            posterior.EstoqueFinal = checked(
                (int)((long)posterior.EstoqueFinal + diferenca));
        }

        produto.Estoque = checked(
            (int)((long)produto.Estoque + diferenca));

        // A data original permanece igual.
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new MovimentacaoEstoqueResponse
        {
            MovimentacaoEstoqueId = movimentacao.MovimentacaoEstoqueId,
            CodigoProduto = produto.CodigoProduto!,
            Tipo = movimentacao.Tipo,
            Descricao = movimentacao.Descricao,
            DescricaoProduto = produto.Descricao!,
            Quantidade = movimentacao.Quantidade,
            EstoqueFinal = movimentacao.EstoqueFinal,
            DataMovimentacao = movimentacao.DataMovimentacao
        };
    }

    public async Task<List<MovimentacaoEstoqueResponse>> ConsultarAsync()
    {
        return await _context.MovimentacoesEstoque
            .AsNoTracking()
            .OrderBy(m => m.MovimentacaoEstoqueId)
            .Select(m => new MovimentacaoEstoqueResponse
            {
                MovimentacaoEstoqueId = m.MovimentacaoEstoqueId,
                CodigoProduto = m.Produto!.CodigoProduto!,
                DescricaoProduto = m.Produto!.Descricao!,
                Tipo = m.Tipo,
                Descricao = m.Descricao,
                Quantidade = m.Quantidade,
                EstoqueFinal = m.EstoqueFinal,
                DataMovimentacao = m.DataMovimentacao
            })
            .ToListAsync();
    }

    public async Task<MovimentacaoEstoqueResponse?> ConsultarPorIdAsync(int id)
    {
        return await _context.MovimentacoesEstoque
            .AsNoTracking()
            .Where(m => m.MovimentacaoEstoqueId == id)
            .Select(m => new MovimentacaoEstoqueResponse
            {
                MovimentacaoEstoqueId = m.MovimentacaoEstoqueId,
                CodigoProduto = m.Produto!.CodigoProduto!,
                DescricaoProduto = m.Produto!.Descricao!,
                Tipo = m.Tipo,
                Descricao = m.Descricao,
                Quantidade = m.Quantidade,
                EstoqueFinal = m.EstoqueFinal,
                DataMovimentacao = m.DataMovimentacao
            })
            .SingleOrDefaultAsync();
    }

    //EXATAMENTE O DESAFIO

    public async Task<EstoqueResponse> ConsultarEstoqueAsync()
    {
        var produtos = await _context.Produtos
            .AsNoTracking()
            .OrderBy(p => p.ProdutoId)
            .Select(p => new ProdutoEstoqueResponse
            {
                CodigoProduto = p.CodigoProduto!,
                DescricaoProduto = p.Descricao!,
                Estoque = p.Estoque
            })
            .ToListAsync();

        return new EstoqueResponse
        {
            Estoque = produtos
        };
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        await using var transaction =
            await _context.Database.BeginTransactionAsync(
                IsolationLevel.Serializable);

        var movimentacao = await _context.MovimentacoesEstoque
            .SingleOrDefaultAsync(m => m.MovimentacaoEstoqueId == id);

        if (movimentacao is null)
        {
            return false;
        }

        var produto = await _context.Produtos
            .SingleAsync(p => p.ProdutoId == movimentacao.ProdutoId);

        var posteriores = await _context.MovimentacoesEstoque
            .Where(m =>
                m.ProdutoId == movimentacao.ProdutoId &&
                m.MovimentacaoEstoqueId > movimentacao.MovimentacaoEstoqueId)
            .ToListAsync();

        long efeito =
            movimentacao.Tipo == TipoMovimentacao.Entrada
                ? movimentacao.Quantidade
                : -(long)movimentacao.Quantidade;

        produto.Estoque = checked(
            (int)((long)produto.Estoque - efeito));

        foreach (var posterior in posteriores)
        {
            posterior.EstoqueFinal = checked(
                (int)((long)posterior.EstoqueFinal - efeito));
        }

        _context.MovimentacoesEstoque.Remove(movimentacao);

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return true;
    }
}