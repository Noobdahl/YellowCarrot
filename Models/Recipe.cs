using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrot.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public string? picUrl { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public List<Step> Steps { get; set; } = new();
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}
