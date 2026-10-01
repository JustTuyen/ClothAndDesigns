using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.users
{
    [Table("Districts")]
    public class DistrictModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        //address navigation property
        [Required]
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        [JsonIgnore]
        public virtual AddressModel? Address { get; set; }

        [Required]
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        [JsonIgnore]
        public virtual CityModel? City { get; set; }

        public virtual ICollection<WardModel> Wards { get; set; } = new List<WardModel>();
        public virtual ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();
    }
}
