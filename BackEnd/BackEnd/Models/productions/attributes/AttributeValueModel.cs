using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace BackEnd.Models.productions.attributes
{
    [Table("AttributeValues")]
    [Index(nameof(AttributeId),nameof(Value), IsUnique = true)]

    public class AttributeValueModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        //Navigation properties
        [Required]
        public int AttributeId { get; set; }
        [ForeignKey(nameof(AttributeId))]
        [JsonIgnore]
        public virtual AttributeModel? Attribute { get; set; }

        public virtual ICollection<VariationValueModel> VariationValues { get; set; } = new List<VariationValueModel>();
    }
}
