using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIregistroDeVenda.Migrations
{
    /// <inheritdoc />
    public partial class PopularDados : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.InsertData(
                table: "Vendedores",
                columns: new[] { "VendedorId", "Nome" },
                values: new object[,]
                {
            { 1, "Ana Silva" },
            { 2, "Bruno Santos" },
            { 3, "Carla Oliveira" },
            { 4, "Diego Souza" },
            { 5, "Elisa Costa" },
            { 6, "Felipe Almeida" },
            { 7, "Gabriela Lima" },
            { 8, "Henrique Rocha" },
            { 9, "Isabela Martins" },
            { 10, "João Pereira" }
                });

            mb.InsertData(
                table: "Produtos",
                columns: new[]
                {
            "ProdutoId",
            "Nome",
            "CodigoProduto",
            "Descricao",
            "PrecoUnitario",
            "Estoque"
                },
                values: new object[,]
                {
            { 1, "Teclado USB", "PRD-000001",
                "Teclado padrão ABNT2 com fio", 100.00m, 48 },

            { 2, "Mouse USB", "PRD-000002",
                "Mouse óptico com fio", 50.00m, 79 },

            { 3, "Monitor 24 polegadas", "PRD-000003",
                "Monitor LED Full HD", 900.00m, 19 },

            { 4, "Headset", "PRD-000004",
                "Fone de ouvido com microfone", 150.00m, 28 },

            { 5, "Webcam", "PRD-000005",
                "Webcam com resolução Full HD", 200.00m, 24 },

            { 6, "Mousepad", "PRD-000006",
                "Mousepad de tecido com base emborrachada", 30.00m, 97 },

            { 7, "Cabo HDMI", "PRD-000007",
                "Cabo HDMI de dois metros", 40.00m, 58 },

            { 8, "SSD 500 GB", "PRD-000008",
                "Unidade SSD SATA de 500 GB", 300.00m, 14 },

            { 9, "Pendrive 32 GB", "PRD-000009",
                "Pendrive USB com capacidade de 32 GB", 45.00m, 46 },

            { 10, "Suporte para notebook", "PRD-000010",
                "Suporte ajustável para notebook", 80.00m, 34 }
                });

            // Data fixa para manter os dados de exemplo reproduzíveis.
            var dataBase = new DateTime(
                2026, 10, 8, 12, 0, 0, DateTimeKind.Utc);

            mb.InsertData(
                table: "Vendas",
                columns: new[] { "VendaId", "DataVenda", "VendedorId" },
                values: new object[,]
                {
            { 1, dataBase, 1 },
            { 2, dataBase.AddMinutes(30), 2 },
            { 3, dataBase.AddMinutes(60), 3 },
            { 4, dataBase.AddMinutes(90), 4 },
            { 5, dataBase.AddMinutes(120), 5 },
            { 6, dataBase.AddMinutes(150), 6 },
            { 7, dataBase.AddMinutes(180), 7 },
            { 8, dataBase.AddMinutes(210), 8 },
            { 9, dataBase.AddMinutes(240), 9 },
            { 10, dataBase.AddMinutes(270), 10 }
                });

            mb.InsertData(
                table: "ItensVenda",
                columns: new[]
                {
            "ItemVendaId",
            "VendaId",
            "ProdutoId",
            "Quantidade",
            "PrecoUnitario"
                },
                values: new object[,]
                {
            { 1, 1, 1, 2, 100.00m },
            { 2, 2, 2, 1, 50.00m },
            { 3, 3, 3, 1, 900.00m },
            { 4, 4, 4, 2, 150.00m },
            { 5, 5, 5, 1, 200.00m },
            { 6, 6, 6, 3, 30.00m },
            { 7, 7, 7, 2, 40.00m },
            { 8, 8, 8, 1, 300.00m },
            { 9, 9, 9, 4, 45.00m },
            { 10, 10, 10, 1, 80.00m }
                });
        }

        protected override void Down(MigrationBuilder mb)
        {
            // A ordem respeita os relacionamentos entre as tabelas.
            for (int id = 1; id <= 10; id++)
            {
                mb.DeleteData(
                    table: "ItensVenda",
                    keyColumn: "ItemVendaId",
                    keyValue: id);
            }

            for (int id = 1; id <= 10; id++)
            {
                mb.DeleteData(
                    table: "Vendas",
                    keyColumn: "VendaId",
                    keyValue: id);
            }

            for (int id = 1; id <= 10; id++)
            {
                mb.DeleteData(
                    table: "Produtos",
                    keyColumn: "ProdutoId",
                    keyValue: id);
            }

            for (int id = 1; id <= 10; id++)
            {
                mb.DeleteData(
                    table: "Vendedores",
                    keyColumn: "VendedorId",
                    keyValue: id);
            }
        }
    }
}
