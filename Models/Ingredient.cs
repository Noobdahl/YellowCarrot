using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrot.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public int Quantity { get; set; }
        public List<Recipe> Recipes { get; set; } = new();
    }
}
