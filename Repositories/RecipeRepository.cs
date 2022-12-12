using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using YellowCarrot.Data;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories
{
    public class RecipeRepository
    {
        private readonly RecipeDbContext context;
        public RecipeRepository(RecipeDbContext context)
        {
            this.context = context;
        }

        public List<Recipe> GetAllRecipes()
        {
            return context.Recipes.Include(r => r.Ingredients).Include(r => r.Steps).Include(r => r.Tags).ToList();
        }
    }
}
