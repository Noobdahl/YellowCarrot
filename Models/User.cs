using EntityFrameworkCore.EncryptColumn.Attribute;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrot.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        [EncryptColumn]
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public List<Recipe> Recipes { get; set; } = new();
    }
}
