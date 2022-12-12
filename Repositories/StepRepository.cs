using YellowCarrot.Data;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories
{
    public class StepRepository
    {
        private readonly RecipeDbContext context;
        public StepRepository(RecipeDbContext context)
        {
            this.context = context;
        }

        public void CreateNewStep(Step nStep)
        {
            context.Steps.Add(nStep);
        }
    }
}
