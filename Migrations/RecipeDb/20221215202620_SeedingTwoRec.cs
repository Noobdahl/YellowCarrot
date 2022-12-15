using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YellowCarrot.Migrations.RecipeDb
{
    /// <inheritdoc />
    public partial class SeedingTwoRec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 2, "Bolognese", "100g", 1 },
                    { 3, "Ketchup", "1 line", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1,
                column: "UserId",
                value: 4);

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "Name", "UserId" },
                values: new object[] { 2, "Lembas bread", 3 });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "StepId", "Description", "Order", "RecipeId" },
                values: new object[] { 2, "Draw a lightsaber with ketchup.", 2, 1 });

            migrationBuilder.InsertData(
                table: "Tags",
                column: "Name",
                value: "foodForTravels");

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 4, "Elven bread", "200g", 2 },
                    { 5, "Mallorn leaves", "3", 2 }
                });

            migrationBuilder.InsertData(
                table: "RecipeTag",
                columns: new[] { "RecipesRecipeId", "TagsName" },
                values: new object[] { 2, "foodForTravels" });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "StepId", "Description", "Order", "RecipeId" },
                values: new object[,]
                {
                    { 3, "Ask any elf for bread.", 1, 2 },
                    { 4, "Wrap bread in leaves.", 2, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "IngredientId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "RecipeTag",
                keyColumns: new[] { "RecipesRecipeId", "TagsName" },
                keyValues: new object[] { 2, "foodForTravels" });

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "StepId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "StepId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "StepId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Name",
                keyValue: "foodForTravels");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "RecipeId",
                keyValue: 1,
                column: "UserId",
                value: 3);
        }
    }
}
