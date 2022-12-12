using YellowCarrot.Data;

namespace YellowCarrot.Repositories
{
    public class RecipeRepository
    {
        private readonly RecipeDbContext context;
        public RecipeRepository(RecipeDbContext context)
        {
            this.context = context;
        }
        //CRUD
    }
}
