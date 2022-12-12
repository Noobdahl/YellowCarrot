using YellowCarrot.Data;
using YellowCarrot.Models;

namespace YellowCarrot.Repositories
{
    public class IngredientRepository
    {
        private readonly RecipeDbContext context;
        public IngredientRepository(RecipeDbContext context)
        {
            this.context = context;
        }
        public void CreateNewIngredient(Ingredient nIngredient)
        {
            context.Ingredients.Add(nIngredient);
        }
    }
}
