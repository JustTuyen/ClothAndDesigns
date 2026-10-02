using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.users
{
    [Table("Wards")]
    [Index(nameof(DistrictId), nameof(CreatedAt))]
    [Index(nameof(Name), IsUnique = true)]
    public class WardModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //address navigation property
        [Required]
        public int DistrictId { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [JsonIgnore]
        public virtual DistrictModel? District { get; set; }
        public virtual ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();

        
    }
}
