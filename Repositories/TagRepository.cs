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
        public void CreateNewTag(Tag nTag)
        {
            context.Tags.Add(nTag);
        }
        public Tag? GetTagByName(string tagName)
        {
            return context.Tags.Where(t => t.Name == tagName).FirstOrDefault();
        }

        public List<Tag> GetAllTags()
        {
            return context.Tags.ToList();
        }
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
