using BackEnd.Models.productions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.others
{
    [Table("Images")]
    [Index(nameof(PublicId), IsUnique = true)]
    public class ImageModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Height { get; set; }
        [Required]
        public int Width { get; set; }
        [Required]
        public long SizeBytes { get; set; }
        [Required]
        public string Type { get; set; } = "image/png";
        [Required]
        public string PublicId { get; set; } = string.Empty;
        [Required]
        public string URL { get; set; } = string.Empty;

        public string? AltText { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public virtual ICollection<ImageGallaryModel> ImageGallaries { get; set; } = new List<ImageGallaryModel>();
        public virtual CategoryModel? Category { get; set; }
        public virtual BannerModel? Banner { get; set; }
        public virtual DiscountModel? Discount { get; set; }
        public virtual CommentModel? Comment { get; set; }
    }
}
