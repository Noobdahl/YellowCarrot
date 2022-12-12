using YellowCarrot.Data;

namespace YellowCarrot.Repositories
{
    public class UnitOfWork
    {
        private readonly RecipeDbContext rcontext;
        private readonly UserDbContext ucontext;
        private RecipeRepository _recipeRepo;
        private UserRepository _userRepo;
        public UnitOfWork(RecipeDbContext rcontext, UserDbContext ucontext)
        {
            this.rcontext = rcontext;
            this.ucontext = ucontext;
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
        public UserRepository userRepo
        {
            get
            {
                if (userRepo == null)
                {
                    _userRepo = new UserRepository(ucontext);
                }
                return _userRepo;
            }
        }
    }
}