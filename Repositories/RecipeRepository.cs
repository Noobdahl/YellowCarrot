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
        public void CreateNewRecipe(Recipe recipe)
        {
            context.Recipes.Add(recipe);
        }

        public void DeleteRecipe(Recipe sRecipe)
        {
            context.Recipes.Remove(sRecipe);
        }

        public List<Recipe> GetSearchResult(string keyWord)
        {
            List<Recipe> result = context.Recipes.Where(r => r.Name.Contains(keyWord)).Include(r => r.Ingredients).Include(r => r.Steps).Include(r => r.Tags).ToList();

            Tag? tag = context.Tags.Where(t => t.Name == keyWord).FirstOrDefault();
            if (tag != null)
            {
                context.Recipes.Where(r => r.Tags.Contains(tag)).ToList().ForEach(r => result.Add(r));
            }
            return result;
        }

        public void UpdateRecipe(Recipe recipe)
        {
            context.Recipes.Update(recipe);
        }

        public Recipe? GetRecipeById(int id)
        {
            return context.Recipes.Where(r => r.RecipeId == id).Include(r => r.Ingredients).Include(r => r.Steps).Include(r => r.Tags).FirstOrDefault();
        }
    }
}