using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ecommerceLite.Migrations
{
    public partial class modeloNovo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DespesasTotais",
                table: "Cesta");

            migrationBuilder.DropColumn(
                name: "MargemLucro",
                table: "Cesta");

            migrationBuilder.DropColumn(
                name: "TotalCompra",
                table: "Cesta");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecoVenda",
                table: "ItemCesta",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "adm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PedidoId = table.Column<int>(nullable: false),
                    DespesasTotais = table.Column<double>(nullable: false),
                    MargemLucro = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adm_Cesta_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Cesta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_adm_PedidoId",
                table: "adm",
                column: "PedidoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "adm");

            migrationBuilder.DropColumn(
                name: "PrecoVenda",
                table: "ItemCesta");

            migrationBuilder.AddColumn<double>(
                name: "DespesasTotais",
                table: "Cesta",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MargemLucro",
                table: "Cesta",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TotalCompra",
                table: "Cesta",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
