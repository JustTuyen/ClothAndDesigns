using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.productions.tags
{
    [Table("Tags")]
    [Index(nameof(Name), IsUnique = true)]
    public class TagModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation property
        public virtual ICollection<ProductTagModel> ProductTags { get; set; } = new List<ProductTagModel>();
    }
}
