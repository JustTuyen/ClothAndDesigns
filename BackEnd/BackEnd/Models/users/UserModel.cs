using BackEnd.Models.others;
using BackEnd.Models.productions;
using BackEnd.Models.productions.tags;
using BackEnd.Models.shopping;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.users
{
    public enum UserRole { Customer, Admin, Staff }


    [Table("Users")]
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(PhoneNumber), IsUnique = true)]
    [Index(nameof(Role), nameof(StatusId))]
    public class UserModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public UserRole Role { get; set; }

        // Navigation property
        public virtual CartModel? Cart { get; set; }

        [Required]
        public int StatusId { get; set; }
        [ForeignKey(nameof(StatusId))]
        [JsonIgnore]
        public virtual StatusModel? Status { get; set; }

        public virtual ICollection<CommentModel> Comments { get; set; } = new List<CommentModel>();
        public virtual ICollection<FavoriteModel> Favorites { get; set; } = new List<FavoriteModel>();
        public virtual ICollection<AddressModel> Addresses { get; set; } = new List<AddressModel>();
        public virtual ICollection<OrderModel> Orders { get; set; } = new List<OrderModel>();
        public virtual ICollection<InvoiceModel> Invoices { get; set; } = new List<InvoiceModel>();
    }
}
