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
    }
}
