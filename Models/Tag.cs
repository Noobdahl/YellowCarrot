using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrot.Models
{
    public class Tag
    {
        [Key]
        public string Name { get; set; } = null!;
        public List<Recipe> Recipes { get; set; } = new();
    }
}
