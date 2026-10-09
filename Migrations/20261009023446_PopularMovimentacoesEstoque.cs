using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIregistroDeVenda.Migrations
{
    /// <inheritdoc />
    public partial class PopularMovimentacoesEstoque : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            var dataBase = new DateTime(
                2026, 10, 9, 12, 0, 0, DateTimeKind.Utc);

            // Tipo: 1 = Entrada; 2 = Saída.
            mb.InsertData(
                table: "MovimentacoesEstoque",
                columns: new[]
                {
            "MovimentacaoEstoqueId",
            "ProdutoId",
            "Tipo",
            "Descricao",
            "Quantidade",
            "EstoqueFinal",
            "DataMovimentacao"
                },
                values: new object[,]
                {
            { 1, 1, 1, "Entrada de mercadoria", 10, 58, dataBase },
            { 2, 2, 2, "Saída de mercadoria", 5, 74, dataBase.AddMinutes(10) },
            { 3, 3, 1, "Entrada de mercadoria", 3, 22, dataBase.AddMinutes(20) },
            { 4, 4, 2, "Saída de mercadoria", 2, 26, dataBase.AddMinutes(30) },
            { 5, 5, 1, "Entrada de mercadoria", 6, 30, dataBase.AddMinutes(40) },
            { 6, 6, 2, "Saída de mercadoria", 7, 90, dataBase.AddMinutes(50) },
            { 7, 7, 1, "Entrada de mercadoria", 12, 70, dataBase.AddMinutes(60) },
            { 8, 8, 2, "Saída de mercadoria", 4, 10, dataBase.AddMinutes(70) },
            { 9, 9, 1, "Entrada de mercadoria", 9, 55, dataBase.AddMinutes(80) },
            { 10, 10, 2, "Saída de mercadoria", 8, 26, dataBase.AddMinutes(90) }
                });

            int[] estoquesFinais = { 58, 74, 22, 26, 30, 90, 70, 10, 55, 26 };

            for (int i = 0; i < estoquesFinais.Length; i++)
            {
                mb.UpdateData(
                    table: "Produtos",
                    keyColumn: "ProdutoId",
                    keyValue: i + 1,
                    column: "Estoque",
                    value: estoquesFinais[i]);
            }
        }

        protected override void Down(MigrationBuilder mb)
        {
            for (int id = 1; id <= 10; id++)
            {
                mb.DeleteData(
                    table: "MovimentacoesEstoque",
                    keyColumn: "MovimentacaoEstoqueId",
                    keyValue: id);
            }

            int[] estoquesAnteriores = { 48, 79, 19, 28, 24, 97, 58, 14, 46, 34 };

            for (int i = 0; i < estoquesAnteriores.Length; i++)
            {
                mb.UpdateData(
                    table: "Produtos",
                    keyColumn: "ProdutoId",
                    keyValue: i + 1,
                    column: "Estoque",
                    value: estoquesAnteriores[i]);
            }
        }

    }
}
