using BackEnd.Models.productions;
using BackEnd.Models.productions.attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.others
{
    [Table("ImageGallary")]
    public class ImageGallaryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DisplayOrder { get; set; }
        [Required]
        public bool IsThumbnail { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        //
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public virtual ProductModel? Product { get; set; }

        [Required]
        public int ImageId { get; set; }
        [ForeignKey(nameof(ImageId))]
        [JsonIgnore]
        public virtual ImageModel? Image { get; set; }

        public int? VariationId { get; set; }
        [ForeignKey(nameof(VariationId))]
        [JsonIgnore]
        public virtual VariationModel? Variation { get; set; }

    }
}
