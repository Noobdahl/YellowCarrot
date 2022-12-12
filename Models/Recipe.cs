﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrot.Models
{
    public class Recipe
    {
        [Key]
        public int RecipeId { get; set; }
        public string Name { get; set; } = null!;
        public int UserId { get; set; }
        public User Owner { get; set; }
        public List<Tag> Tags { get; set; } = new();
        public List<Step> Steps { get; set; } = new();
        public List<Ingredient> Ingredients { get; set; } = new();
    }
}
