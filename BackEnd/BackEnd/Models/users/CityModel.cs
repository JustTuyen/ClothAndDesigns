using BackEnd.Models.shopping;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.users
{
    [Table("Cities")]
    [Index(nameof(Name), IsUnique = true)]
    public class CityModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Code { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //address navigation property
        public virtual ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();

        public virtual ICollection<DistrictModel> Districts { get; set; } = new List<DistrictModel>();
    }
}
