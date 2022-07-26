using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructura.Persistence.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientePlato_Platos_PlatoId",
                table: "IngredientePlato");

            migrationBuilder.RenameColumn(
                name: "Estados",
                table: "Mesas",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "PlatoId",
                table: "IngredientePlato",
                newName: "OrdensId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientePlato_PlatoId",
                table: "IngredientePlato",
                newName: "IX_IngredientePlato_OrdensId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientePlato_Platos_OrdensId",
                table: "IngredientePlato",
                column: "OrdensId",
                principalTable: "Platos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientePlato_Platos_OrdensId",
                table: "IngredientePlato");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "Mesas",
                newName: "Estados");

            migrationBuilder.RenameColumn(
                name: "OrdensId",
                table: "IngredientePlato",
                newName: "PlatoId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredientePlato_OrdensId",
                table: "IngredientePlato",
                newName: "IX_IngredientePlato_PlatoId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientePlato_Platos_PlatoId",
                table: "IngredientePlato",
                column: "PlatoId",
                principalTable: "Platos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
