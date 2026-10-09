using APIregistroDeVenda.Domain;
using Microsoft.EntityFrameworkCore;

namespace APIregistroDeVenda.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Produto>()
            .HasIndex(p => p.CodigoProduto)
            .IsUnique();

        modelBuilder.Entity<MovimentacaoEstoque>()
    .HasOne(m => m.Produto)
    .WithMany()
    .HasForeignKey(m => m.ProdutoId)
    .OnDelete(DeleteBehavior.Restrict);
    }

    public DbSet<Vendedor> Vendedores { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Venda> Vendas { get; set; }
    public DbSet<ItemVenda> ItensVenda { get; set; }
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }
}