using BackEnd.Models.others;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.productions
{
    [Table("Categories")]
    [Index(nameof(StatusId), nameof(CreatedAt))]
    [Index(nameof(Name), IsUnique =true)]
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //
        public int? ImageId { get; set; }
        [ForeignKey(nameof(ImageId))]
        [JsonIgnore]
        public virtual ImageModel? Image { get; set; }

        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }

        public virtual ICollection<SubCategoriesModel> SubCategories { get; set; } = new List<SubCategoriesModel>();
    }
}
