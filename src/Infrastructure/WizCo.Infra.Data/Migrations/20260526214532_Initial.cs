using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WizCo.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    ClienteNome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    ValorTotal = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItensPedido",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PedidoId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProdutoNome = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItensPedido", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItensPedido_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "ClienteNome", "DataCriacao", "Status", "ValorTotal" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "João Silva", new DateTime(2026, 5, 6, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3182), "Pago", 4500m },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Maria Oliveira", new DateTime(2026, 5, 8, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3204), "Novo", 7999m },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Carlos Souza", new DateTime(2026, 5, 9, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3211), "Cancelado", 3200m },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Fernanda Lima", new DateTime(2026, 5, 11, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3216), "Pago", 4200m },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Ricardo Mendes", new DateTime(2026, 5, 12, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3221), "Novo", 1100m },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Patrícia Gomes", new DateTime(2026, 5, 13, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3226), "Pago", 1800m },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Lucas Ferreira", new DateTime(2026, 5, 14, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3231), "Cancelado", 350m },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Juliana Costa", new DateTime(2026, 5, 15, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3236), "Novo", 3200m },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "André Martins", new DateTime(2026, 5, 16, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3240), "Pago", 1400m },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Camila Rocha", new DateTime(2026, 5, 17, 21, 45, 29, 156, DateTimeKind.Utc).AddTicks(3248), "Novo", 897m }
                });

            migrationBuilder.InsertData(
                table: "ItensPedido",
                columns: new[] { "Id", "PedidoId", "PrecoUnitario", "ProdutoNome", "Quantidade" },
                values: new object[,]
                {
                    { new Guid("21111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111111"), 4500m, "Notebook Dell Inspiron", 1 },
                    { new Guid("22222222-1111-1111-1111-111111111111"), new Guid("22222222-2222-2222-2222-222222222222"), 7999m, "iPhone 15 Pro", 1 },
                    { new Guid("23333333-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333"), 3200m, "Smart TV Samsung 55", 1 },
                    { new Guid("24444444-1111-1111-1111-111111111111"), new Guid("44444444-4444-4444-4444-444444444444"), 4200m, "PlayStation 5", 1 },
                    { new Guid("25555555-1111-1111-1111-111111111111"), new Guid("55555555-5555-5555-5555-555555555555"), 550m, "Mouse Logitech MX Master 3", 2 },
                    { new Guid("26666666-1111-1111-1111-111111111111"), new Guid("66666666-6666-6666-6666-666666666666"), 1800m, "Monitor LG Ultrawide", 1 },
                    { new Guid("27777777-1111-1111-1111-111111111111"), new Guid("77777777-7777-7777-7777-777777777777"), 350m, "Teclado Mecânico Redragon", 1 },
                    { new Guid("28888888-1111-1111-1111-111111111111"), new Guid("88888888-8888-8888-8888-888888888888"), 3200m, "Apple Watch Series 9", 1 },
                    { new Guid("29999999-1111-1111-1111-111111111111"), new Guid("99999999-9999-9999-9999-999999999999"), 1400m, "Cadeira Gamer ThunderX3", 1 },
                    { new Guid("2aaaaaaa-1111-1111-1111-111111111111"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), 299m, "Echo Dot Alexa", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItensPedido_PedidoId",
                table: "ItensPedido",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItensPedido");

            migrationBuilder.DropTable(
                name: "Pedidos");
        }
    }
}
