using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YellowCarrot.Migrations.RecipeDb
{
    /// <inheritdoc />
    public partial class TryingSeedWithTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RecipeTag",
                columns: new[] { "RecipesRecipeId", "TagsName" },
                values: new object[] { 1, "4/5" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecipeTag",
                keyColumns: new[] { "RecipesRecipeId", "TagsName" },
                keyValues: new object[] { 1, "4/5" });
        }
    }
}
