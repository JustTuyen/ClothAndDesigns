using BackEnd.Models.productions.attributes;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.shopping
{
    [Table("InvoiceItems")]
    [Index(nameof(InvoiceId), nameof(VariationId), IsUnique = true)]
    public class InvoiceItemModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } = 0;
        [MaxLength(500)]
        public string? Note { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // Navigation properties
        [Required]
        public int VariationId { get; set; }
        [ForeignKey(nameof(VariationId))]
        [JsonIgnore]
        public virtual VariationModel? Variation { get; set; }

        [Required]
        public int InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        [JsonIgnore]
        public virtual InvoiceModel? Invoice { get; set; }
    }
}
