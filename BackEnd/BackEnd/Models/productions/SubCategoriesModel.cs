using BackEnd.Models.others;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.productions
{
    [Table("SubCategories")]
    [Index(nameof(CategoryId), nameof(StatusId))]
    [Index(nameof(CategoryId), nameof(Name), IsUnique = true)]
    public class SubCategoriesModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        [Required]
        public string Slug { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [Required]
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [JsonIgnore]
        public virtual CategoryModel? Category { get; set; }

        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }

        public virtual ICollection<ProductModel>? Products { get; set; } = new List<ProductModel>();
    }
}
