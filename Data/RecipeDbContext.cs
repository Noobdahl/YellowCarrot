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

        }
    }
}