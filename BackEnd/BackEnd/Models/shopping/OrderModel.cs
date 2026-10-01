using BackEnd.Models.others;
using BackEnd.Models.users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.shopping
{
    [Table("Orders")]
    public class OrderModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ShippingFee { get; set; }
        [Required]
        public string InvoiceCode { get; set; }
        public string? Note { get; set; }
        public bool IsPaid { get; set; } = false;
        public string? AddressSnapshot{ get; set; }
        public string? MethodSnapshot { get; set; }
        public string? FullFillmentType { get; set; }
        public string OrderNumber { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        [Required]
        public int AddressId { get; set; }
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
