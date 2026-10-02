using BackEnd.Models.others;
using BackEnd.Models.users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace BackEnd.Models.productions
{
    [Table("Comments")]
    [Index(nameof(ProductId), nameof(StatusId))]
    [Index(nameof(ProductId), nameof(UserId), IsUnique = true)]
    public class CommentModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(1000)]
        public string Content { get; set; }
        [Required]
        [Range(0,5)]
        public int Rate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Foreign key
        [Required]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore]
        public virtual ProductModel? Product { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual UserModel? User { get; set; }

        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }

        public int? ImageId { get; set; }
        [ForeignKey(nameof(ImageId))]
        [JsonIgnore]
        public virtual ImageModel? Image { get; set; }
    }
}
