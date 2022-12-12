using YellowCarrot.Data;

namespace YellowCarrot.Repositories
{
    public class UnitOfWork
    {
        private readonly RecipeDbContext rcontext;
        private RecipeRepository _recipeRepo;
        public UnitOfWork(RecipeDbContext rcontext)
        {
            this.rcontext = rcontext;
        }
        public RecipeRepository recipeRepo
        {
            get
            {
                if (_recipeRepo == null)
                {
                    _recipeRepo = new RecipeRepository(rcontext);
                }
                return _recipeRepo;
            }
        }
    }
}