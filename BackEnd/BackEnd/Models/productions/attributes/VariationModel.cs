using BackEnd.Models.others;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.productions.attributes
{
    [Table("Variations")]
    public class VariationModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int InStock { get; set; }
        [Required]
        public string Sku { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AddPrice { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public virtual ProductModel? Product { get; set; }

        public virtual ICollection<VariationValueModel> VariationValues { get; set; } = new List<VariationValueModel>();
        public virtual ICollection<ImageGallaryModel> ImageGallaries { get; set; } = new List<ImageGallaryModel>();
    }
}
