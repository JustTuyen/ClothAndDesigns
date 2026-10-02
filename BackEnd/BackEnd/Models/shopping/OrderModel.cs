using BackEnd.Models.others;
using BackEnd.Models.users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.shopping
{
    [Table("Orders")]
    [Index(nameof(OrderCode), IsUnique = true)]
    [Index(nameof(UserId), nameof(CreatedAt))]
    [Index(nameof(StatusId), nameof(CreatedAt))]
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ShippingFee { get; set; } = 0;
        [Required]
        public string OrderCode { get; set; }
        public string? Note { get; set; }
        public bool IsPaid { get; set; } = false;
        [Required]
        public string AddressSnapshot{ get; set; }
        [Required]
        public string MethodSnapshot { get; set; }
        [Required]
        public string FullFillmentType { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        public int? AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        [JsonIgnore]
        public virtual AddressModel? Address { get; set; }

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

        [Required]
        public int PaymentMethodId { get; set; }
        [ForeignKey(nameof(PaymentMethodId))]
        [JsonIgnore]
        public virtual PaymentMethodModel? PaymentMethod { get; set; }

        public virtual ICollection<OrderItemModel>? OrderItems { get; set; } = new List<OrderItemModel>();
    }
}
