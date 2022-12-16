using YellowCarrot.Data;

namespace YellowCarrot.Repositories
{
    public class UnitOfWork
    {
        private readonly RecipeDbContext context;
        private RecipeRepository _recipeRepo;
        private TagRepository _tagRepo;
        public UnitOfWork(RecipeDbContext rcontext)
        {
            this.context = rcontext;
        }
        public RecipeRepository RecipeRepo
        {
            get
            {
                if (_recipeRepo == null)
                {
                    _recipeRepo = new RecipeRepository(context);
                }
                return _recipeRepo;
            }
        }
        public TagRepository TagRepo
        {
            get
            {
                if (_tagRepo == null)
                {
                    _tagRepo = new TagRepository(context);
                }
                return _tagRepo;
            }
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}