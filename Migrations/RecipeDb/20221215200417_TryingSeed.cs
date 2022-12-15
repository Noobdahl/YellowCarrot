using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations.RecipeDb
{
    /// <inheritdoc />
    public partial class TryingSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "Name", "UserId" },
                values: new object[] { 1, "Darth Vader's Bolognese", 3 });

            migrationBuilder.InsertData(
                table: "Tags",
                column: "Name",
                value: "4/5");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Name", "Quantity", "RecipeId" },
                values: new object[] { 1, "Spaghetti", "250g", 1 });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "StepId", "Description", "Order", "RecipeId" },
                values: new object[] { 1, "Use the force.", 1, 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "StepId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "4/5");

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1);
        }
    }
}
