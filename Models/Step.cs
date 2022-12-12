using System.ComponentModel.DataAnnotations;

namespace YellowCarrot.Models
{
    public class Step
    {
        [Key]
        public int StepId { get; set; }
        public int Order { get; set; }
        public string Description { get; set; } = null!;
        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}
