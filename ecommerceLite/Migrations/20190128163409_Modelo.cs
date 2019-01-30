using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerceLite.Migrations
{
    public partial class Modelo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TotalCompra = table.Column<double>(nullable: false),
                    DespesasTotais = table.Column<double>(nullable: false),
                    MargemLucro = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cesta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCesta",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PedidoId = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCesta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemCesta_Cesta_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Cesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemCesta_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemCesta_PedidoId",
                table: "ItemCesta",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemCesta_ProdutoId",
                table: "ItemCesta",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemCesta");

            migrationBuilder.DropTable(
                name: "Cesta");
        }
    }
}
