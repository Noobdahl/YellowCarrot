using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Collections.Generic;
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


            modelBuilder.Entity<Recipe>().HasData(new Recipe()
            {
                RecipeId = 1,
                Name = "Darth Vader's Bolognese",
                UserId = 3,
            });
            modelBuilder.Entity<Ingredient>().HasData(new Ingredient()
            {
                IngredientId = 1,
                Name = "Spaghetti",
                Quantity = "250g",
                RecipeId = 1,
            });
            modelBuilder.Entity<Step>().HasData(new Step())


            //modelBuilder.Entity<Recipe>().HasData(new Recipe()
            //{
            //    RecipeId = 1,
            //    Name = "Darth Vader's Bolognese",
            //    UserId = 3,
            //    Tags = new List<Tag>() { new Tag() { Name = "spicyBreath"} },
            //    Steps = new List<Step>() 
            //    { 
            //        new Step() { 
            //            Order = 1, 
            //            RecipeId = 1, 
            //            Description = "Make normal spaghetti bolognese."
            //        },
            //        new Step() {
            //            Order = 2,
            //            RecipeId = 1,
            //            Description = "Scold yourself on boiling water."
            //        },
            //        new Step() {
            //            Order = 3,
            //            RecipeId = 1,
            //            Description = "Draw a string of ketchup as a lightsaber."
            //        },
            //        new Step() {
            //            Order = 4,
            //            RecipeId = 1,
            //            Description = "Serve without revealing your face."
            //        }
            //    },
            //    Ingredients = new List<Ingredient>()
            //    {
            //        new Ingredient()
            //        {
            //            IngredientId = 1,
            //            Name = "Spaghetti",
            //            Quantity = "500g",
            //            RecipeId = 1,
            //        },
            //        new Ingredient()
            //        {
            //            IngredientId = 2,
            //            Name = "Bolognese",
            //            Quantity = "500g",
            //            RecipeId = 1,
            //        },
            //        new Ingredient()
            //        {
            //            IngredientId = 3,
            //            Name = "Ketchup",
            //            Quantity = "1 string",
            //            RecipeId = 1,
            //        }
            //    }
            //});
        }
    }
}