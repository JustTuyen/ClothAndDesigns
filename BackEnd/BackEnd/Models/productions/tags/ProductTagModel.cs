using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.productions.tags
{
    [Table("ProductTags")]
    public class ProductTagModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Foreign key 
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public virtual ProductModel? Product { get; set; }
        [Required]
        public int TagId { get; set; }
        [ForeignKey(nameof(TagId))]
        [JsonIgnore]
        public virtual TagModel? Tag { get; set; }

    }
}
