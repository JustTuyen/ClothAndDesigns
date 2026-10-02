using BackEnd.Models.users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.shopping
{
    [Table("Carts")]
    [Index(nameof(UserId), IsUnique = true)]
    public class CartModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        [JsonIgnore]
        public virtual UserModel? User { get; set; }

        public virtual ICollection<CartItemModel> CartItems { get; set; } = new List<CartItemModel>();

    }
}
