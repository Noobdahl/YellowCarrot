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
        //Recieves new ingredient, and adds it to dB - C in crud
        public void CreateNewIngredient(Ingredient nIngredient)
        {
            context.Ingredients.Add(nIngredient);
        }
    }
}
