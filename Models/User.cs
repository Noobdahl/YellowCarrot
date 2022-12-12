using EntityFrameworkCore.EncryptColumn.Attribute;
using System.ComponentModel.DataAnnotations;

namespace YellowCarrot.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [EncryptColumn]
        [MaxLength(250)]
        public string Password { get; set; } = null!;
        public bool IsAdmin { get; set; }
    }
}
