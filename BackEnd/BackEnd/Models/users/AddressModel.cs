using BackEnd.Models.others;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.users
{
    [Table("Addresses")]
    [Index(nameof(UserId))]
    public class AddressModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        public string? Note { get; set; }
        public bool IsDefault { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual UserModel? User { get; set; }

        [Required]
        public int CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        [JsonIgnore]
        public virtual CityModel? City { get; set; }

        [Required]
        public int DistrictId { get; set; }
        [ForeignKey(nameof(DistrictId))]
        [JsonIgnore]
        public virtual DistrictModel? District { get; set; }

        [Required]
        public int WardId { get; set; }
        [ForeignKey(nameof(WardId))]
        [JsonIgnore]
        public virtual WardModel? Ward { get; set; }
    }
}
