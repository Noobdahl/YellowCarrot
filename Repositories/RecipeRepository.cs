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
        //Get all recipes from dB, R in crud
        public List<Recipe> GetAllRecipes()
        {
            return context.Recipes.Include(r => r.Ingredients).Include(r => r.Steps).Include(r => r.Tags).ToList();
        }

        //Creates a new recipe in database, C in crud
        public void CreateNewRecipe(Recipe recipe)
        {
            context.Recipes.Add(recipe);
        }

        //Deletes a recipe from database, D in crud
        public void DeleteRecipe(Recipe sRecipe)
        {
            context.Recipes.Remove(sRecipe);
        }

        //Returns a list of recipes from dB from the search keyword
        public List<Recipe> GetSearchResult(string keyWord)
        {
            //Gets all recipes that contains the keyword in their name
            List<Recipe> result = context.Recipes.Where(r => r.Name.Contains(keyWord)).Include(r => r.Ingredients).Include(r => r.Steps).Include(r => r.Tags).ToList();

            //Returns tag if theres any that matches with keyword
            Tag? tag = context.Tags.Where(t => t.Name == keyWord).FirstOrDefault();
            //If tag is found
            if (tag != null)
            {
                //Gets all recipes with the matched tag
                List<Recipe> list = context.Recipes.Where(r => r.Tags.Contains(tag)).ToList();
                //Loops through recipes found by tag
                foreach (Recipe recipe in list)
                {
                    //If recipe isn't already fetched by the name above (row 38)
                    if (!result.Contains(recipe))
                    {
                        //Add it to the list we are returning
                        result.Add(recipe);
                    }
                }
            }
            return result;
        }

        //Updates the recipe recieved to dB
        public void UpdateRecipe(Recipe recipe)
        {
            context.Recipes.Update(recipe);
        }

        //Returns recipe matched by recieved id, else returns null
        public Recipe? GetRecipeById(int id)
        {
            return context.Recipes.Where(r => r.RecipeId == id).Include(r => r.Ingredients).Include(r => r.Steps).Include(r => r.Tags).FirstOrDefault();
        }
    }
}