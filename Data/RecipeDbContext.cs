using Microsoft.EntityFrameworkCore;
using YellowCarrot.Models;

namespace YellowCarrot.Data
{
    public class RecipeDbContext : DbContext
    {
        public RecipeDbContext()
        {

        }
        public RecipeDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=YellowCarrotRecipeDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().HasMany(u => u.Ingredients).WithOne(r => r.Recipe).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Recipe>().HasMany(u => u.Steps).WithOne(r => r.Recipe).OnDelete(DeleteBehavior.Cascade);

            //Darth Vader recipe
            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 1,
                Name = "Darth Vader's Bolognese",
                UserId = 4,
                picUrl = "https://images.immediate.co.uk/production/volatile/sites/30/2020/08/the-best-spaghetti-bolognese-7e83155.jpg?quality=90&webp=true&resize=375,341"
            });
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    IngredientId = 1,
                    Name = "Spaghetti",
                    Quantity = "250g",
                    RecipeId = 1,
                },
                new Ingredient()
                {
                    IngredientId = 2,
                    Name = "Bolognese",
                    Quantity = "100g",
                    RecipeId = 1,
                },
                new Ingredient()
                {
                    IngredientId = 3,
                    Name = "Ketchup",
                    Quantity = "1 line",
                    RecipeId = 1,
                }
                );
            modelBuilder.Entity<Step>().HasData(new Step()
            {
                StepId = 1,
                Order = 1,
                Description = "Use the force.",
                RecipeId = 1,
            },
            new Step()
            {
                StepId = 2,
                Order = 2,
                Description = "Draw a lightsaber with ketchup.",
                RecipeId = 1,
            }
            );
            modelBuilder.Entity<Tag>().HasData(new Tag()
            {
                Name = "4/5"
            });
            modelBuilder.Entity("RecipeTag").HasData(
                new
                {
                    TagsName = "4/5",
                    RecipesRecipeId = 1,
                });




            //Recipe 2
            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 2,
                Name = "Lembas bread",
                UserId = 3,
                picUrl = "http://4.bp.blogspot.com/_cd6_MFUGTUE/SU6yFC_7eHI/AAAAAAAAAII/dgbwkYf3cZs/w1200-h630-p-k-no-nu/_DSC6981.jpg"
            });
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    IngredientId = 4,
                    Name = "Elven bread",
                    Quantity = "200g",
                    RecipeId = 2,
                },
                new Ingredient()
                {
                    IngredientId = 5,
                    Name = "Mallorn leaves",
                    Quantity = "3",
                    RecipeId = 2,
                }
                );
            modelBuilder.Entity<Step>().HasData(new Step()
            {
                StepId = 3,
                Order = 1,
                Description = "Ask any elf for bread.",
                RecipeId = 2,
            },
            new Step()
            {
                StepId = 4,
                Order = 2,
                Description = "Wrap bread in leaves.",
                RecipeId = 2,
            }
            );
            modelBuilder.Entity<Tag>().HasData(new Tag()
            {
                Name = "foodForTravels"
            });
            modelBuilder.Entity("RecipeTag").HasData(
                new
                {
                    TagsName = "foodForTravels",
                    RecipesRecipeId = 2,
                });




            //Recipe 3
            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 3,
                Name = "Gin & Tonic med äpple",
                UserId = 5,
                picUrl = "https://i.ibb.co/fHtXgjx/image.png"
            });
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient()
                {
                    IngredientId = 6,
                    Name = "Hellström Gin",
                    Quantity = "5cl",
                    RecipeId = 3,
                },
                new Ingredient()
                {
                    IngredientId = 7,
                    Name = "Neutral Tonic (ekobryggeriet original)",
                    Quantity = "10cl",
                    RecipeId = 3,
                },
                new Ingredient()
                {
                    IngredientId = 8,
                    Name = "Is",
                    Quantity = "Mycket",
                    RecipeId = 3,
                },
                new Ingredient()
                {
                    IngredientId = 9,
                    Name = "Timjan",
                    Quantity = "Ett par kvistar",
                    RecipeId = 3,
                },
                new Ingredient()
                {
                    IngredientId = 10,
                    Name = "Äpple",
                    Quantity = "En klyfta",
                    RecipeId = 3,
                }
                );
            modelBuilder.Entity<Step>().HasData(new Step()
            {
                StepId = 5,
                Order = 1,
                Description = "Ta ett kallt glas.",
                RecipeId = 3,
            },
            new Step()
            {
                StepId = 6,
                Order = 2,
                Description = "Fyll upp glaset med mycket is.",
                RecipeId = 3,
            },
            new Step()
            {
                StepId = 7,
                Order = 3,
                Description = "Tillsätt gin.",
                RecipeId = 3,
            },
            new Step()
            {
                StepId = 8,
                Order = 4,
                Description = "Tillsätt tonic.",
                RecipeId = 3,
            },
            new Step()
            {
                StepId = 9,
                Order = 5,
                Description = "Sätt i ett par kvistar timjan.",
                RecipeId = 3,
            },
            new Step()
            {
                StepId = 10,
                Order = 6,
                Description = "Lägg i en klyfta äpple.",
                RecipeId = 3,
            }
            );
            modelBuilder.Entity<Tag>().HasData(
                new Tag()
                {
                    Name = "gin"
                },
            new Tag()
            {
                Name = "neutralTonic"
            });
            modelBuilder.Entity("RecipeTag").HasData(
                new
                {
                    TagsName = "gin",
                    RecipesRecipeId = 3,
                },
                new
                {
                    TagsName = "neutralTonic",
                    RecipesRecipeId = 3,
                });
        }
    }
}