using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using YellowCarrot.Data;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories
{
    public class TagRepository
    {
        private readonly RecipeDbContext context;
        public TagRepository(RecipeDbContext context)
        {
            this.context = context;
        }

        //Returns tag from dB by recieved tagName, returns null if not found
        public Tag? GetTagByName(string tagName)
        {
            return context.Tags.Where(t => t.Name == tagName).FirstOrDefault();
        }
        //This was supposed to be used when searching for tags, but i built searchfunction in the RecipeRepository
        public List<Tag> GetAllTags()
        {
            return context.Tags.ToList();
        }

        //Returns a single long string containing all tags on recipe with recieved id
        public string GetAllTagsFromRecipeById(int id)
        {
            string s = "";
            Recipe recipe = context.Recipes.Where(r => r.RecipeId == id).Include(r => r.Tags).FirstOrDefault();
            foreach (Tag tag in recipe.Tags)
            {
                s += $"#{tag.Name} ";
            }
            return s;
        }
    }
}
