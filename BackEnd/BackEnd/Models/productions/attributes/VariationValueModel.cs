using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BackEnd.Models.productions.attributes
{
    [Table("VariationValues")]
    [Index(nameof(VariationId), nameof(AttributeValueId), IsUnique = true)]
    public class VariationValueModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int AttributeId { get; set; }
        [ForeignKey(nameof(AttributeId))]
        [JsonIgnore]
        public virtual AttributeModel? Attribute { get; set; }

        [Required]
        public int VariationId { get; set; }
        [ForeignKey(nameof(VariationId))]
        [JsonIgnore]
        public virtual VariationModel? Variation { get; set; }

        [Required]
        public int AttributeValueId { get; set; }
        [ForeignKey(nameof(AttributeValueId))]
        [JsonIgnore]
        public virtual AttributeValueModel? AttributeValue { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
