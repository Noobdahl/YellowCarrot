using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YellowCarrot.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        [ForeignKey(nameof(Recipe))]
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
        //public List<Recipe> Recipes { get; set; } = new();
    }
}
