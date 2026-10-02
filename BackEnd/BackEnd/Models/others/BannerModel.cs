using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.others
{
    [Table("Banners")]
    [Index(nameof(StatusId), nameof(CreatedAt))]
    public class BannerModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Note { get; set; }
        [Required]
        public int DisplayOrder { get; set; }

        [Required]
        public bool IsThumbnail { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        [Required]
        public int ImageId { get; set; }
        [ForeignKey(nameof(ImageId))]
        [JsonIgnore]
        public virtual ImageModel? Image { get; set; }

        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }
    }
}
