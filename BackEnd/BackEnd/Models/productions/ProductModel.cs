using BackEnd.Models.others;
using BackEnd.Models.productions.attributes;
using BackEnd.Models.productions.tags;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.productions
{
    [Table("Products")]
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }
        [Required]
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;

        // Navigation properties
        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }

        [Required]
        public int SubCategoryId { get; set; }
        [ForeignKey(nameof(SubCategoryId))]
        [JsonIgnore]
        public virtual SubCategoriesMdel? SubCategory { get; set; }

        public int? DiscountId { get; set; }
        [ForeignKey(nameof(DiscountId))]
        [JsonIgnore]
        public virtual DiscountModel? Discount { get; set; }

        public virtual ICollection<VariationModel> Variations { get; set; } = new List<VariationModel>();
        public virtual ICollection<ProductTagModel> ProductTags { get; set; } = new List<ProductTagModel>();
        public virtual ICollection<ImageGallaryModel> ImageGallaries { get; set; } = new List<ImageGallaryModel>();

    }
}
