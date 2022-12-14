using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YellowCarrot.Migrations.RecipeDb
{
    /// <inheritdoc />
    public partial class InitialWithSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    picUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Steps",
                columns: table => new
                {
                    StepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steps", x => x.StepId);
                    table.ForeignKey(
                        name: "FK_Steps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeTag",
                columns: table => new
                {
                    RecipesRecipeId = table.Column<int>(type: "int", nullable: false),
                    TagsName = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeTag", x => new { x.RecipesRecipeId, x.TagsName });
                    table.ForeignKey(
                        name: "FK_RecipeTag_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeTag_Tags_TagsName",
                        column: x => x.TagsName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "RecipeId", "Name", "UserId", "picUrl" },
                values: new object[,]
                {
                    { 1, "Darth Vader's Bolognese", 4, "https://images.immediate.co.uk/production/volatile/sites/30/2020/08/the-best-spaghetti-bolognese-7e83155.jpg?quality=90&webp=true&resize=375,341" },
                    { 2, "Lembas bread", 3, "http://4.bp.blogspot.com/_cd6_MFUGTUE/SU6yFC_7eHI/AAAAAAAAAII/dgbwkYf3cZs/w1200-h630-p-k-no-nu/_DSC6981.jpg" },
                    { 3, "Gin & Tonic med äpple", 5, "https://i.ibb.co/fHtXgjx/image.png" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                column: "Name",
                values: new object[]
                {
                    "4/5",
                    "foodForTravels",
                    "gin",
                    "neutralTonic"
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "IngredientId", "Name", "Quantity", "RecipeId" },
                values: new object[,]
                {
                    { 1, "Spaghetti", "250g", 1 },
                    { 2, "Bolognese", "100g", 1 },
                    { 3, "Ketchup", "1 line", 1 },
                    { 4, "Elven bread", "200g", 2 },
                    { 5, "Mallorn leaves", "3", 2 },
                    { 6, "Hellström Gin", "5cl", 3 },
                    { 7, "Neutral Tonic (ekobryggeriet original)", "10cl", 3 },
                    { 8, "Is", "Mycket", 3 },
                    { 9, "Timjan", "Ett par kvistar", 3 },
                    { 10, "Äpple", "Ett klyfta", 3 }
                });

            migrationBuilder.InsertData(
                table: "RecipeTag",
                columns: new[] { "RecipesRecipeId", "TagsName" },
                values: new object[,]
                {
                    { 1, "4/5" },
                    { 2, "foodForTravels" },
                    { 3, "gin" },
                    { 3, "neutralTonic" }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "StepId", "Description", "Order", "RecipeId" },
                values: new object[,]
                {
                    { 1, "Use the force.", 1, 1 },
                    { 2, "Draw a lightsaber with ketchup.", 2, 1 },
                    { 3, "Ask any elf for bread.", 1, 2 },
                    { 4, "Wrap bread in leaves.", 2, 2 },
                    { 5, "Ta ett kallt glas.", 1, 3 },
                    { 6, "Fyll upp glaset med mycket is.", 2, 3 },
                    { 7, "Tillsätt gin.", 3, 3 },
                    { 8, "Tillsätt tonic.", 4, 3 },
                    { 9, "Sätt i ett par kvistar timjan.", 5, 3 },
                    { 10, "Lägg i en klyfta äpple.", 6, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RecipeId",
                table: "Ingredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeTag_TagsName",
                table: "RecipeTag",
                column: "TagsName");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_RecipeId",
                table: "Steps",
                column: "RecipeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "RecipeTag");

            migrationBuilder.DropTable(
                name: "Steps");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
