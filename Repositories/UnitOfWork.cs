using YellowCarrot.Data;

namespace YellowCarrot.Repositories
{
    public class UnitOfWork
    {
        private readonly RecipeDbContext context;
        private RecipeRepository _recipeRepo;
        private IngredientRepository _ingredientRepo;
        private StepRepository _stepRepo;
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
        public IngredientRepository IngredientRepo
        {
            get
            {
                if (_ingredientRepo == null)
                {
                    _ingredientRepo = new IngredientRepository(context);
                }
                return _ingredientRepo;
            }
        }
        public StepRepository StepRepo
        {
            get
            {
                if (_stepRepo == null)
                {
                    _stepRepo = new StepRepository(context);
                }
                return _stepRepo;
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